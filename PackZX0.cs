namespace ConvImgCpc {
	public class block_t {
		public block_t chain;
		public block_t ghost_chain;
		public int bits;
		public int index;
		public int offset;
		public int length;
		public int references;
	}

	class PackZX0 {
		const int INITIAL_OFFSET = 1;
		const int MAX_OFFSET_ZX0 = 32640;
		const int MAX_SCALE = 50;
		const int QTY_BLOCKS = 10000;

		byte[] output_data;
		int output_index;
		int input_index;
		int bit_index;
		int bit_mask;
		int backtrack;

		block_t ghost_root;
		block_t[] dead_array;
		int dead_array_size = 0;

		block_t allocate(int bits, int index, int offset, int length, block_t chain) {
			block_t ptr = new block_t();
			if (ghost_root != null) {
				ptr = ghost_root;
				ghost_root = ptr.ghost_chain;
				if (ptr.chain != null) {
					if (--ptr.chain.references == 0) {
						ptr.chain.ghost_chain = ghost_root;
						ghost_root = ptr.chain;
					}
				}
			}
			else {
				if (dead_array_size == 0) {
					dead_array = new block_t[QTY_BLOCKS];
					for (int i = 0; i < QTY_BLOCKS; i++)
						dead_array[i] = new block_t();

					dead_array_size = QTY_BLOCKS;
				}
				ptr = dead_array[--dead_array_size];
			}
			ptr.bits = bits;
			ptr.index = index;
			ptr.offset = offset;
			ptr.length = length;
			if (chain != null)
				chain.references++;

			ptr.chain = chain;
			ptr.references = 0;
			return ptr;
		}

		void assign(ref block_t ptr, block_t chain) {
			chain.references++;
			if (ptr != null) {
				if (--ptr.references == 0) {
					ptr.ghost_chain = ghost_root;
					ghost_root = ptr;
				}
			}
			ptr = chain;
		}

		int offset_ceiling(int index) {
			return index > MAX_OFFSET_ZX0 ? MAX_OFFSET_ZX0 : index < INITIAL_OFFSET ? INITIAL_OFFSET : index;
		}

		int elias_gamma_bits(int value) {
			int bits = 1;
			while (value > 1) {
				bits += 2;
				value >>= 1;
			}
			return bits;
		}

		void read_bytes(int n) {
			input_index += n;
		}

		void write_byte(byte value) {
			output_data[output_index++] = value;
		}

		void write_bit(int value) {
			if (backtrack != 0) {
				if (value != 0)
					output_data[output_index - 1] |= 1;

				backtrack = 0;
			}
			else {
				if (bit_mask == 0) {
					bit_mask = 128;
					bit_index = output_index;
					write_byte(0);
				}
				if (value != 0)
					output_data[bit_index] |= (byte)bit_mask;

				bit_mask >>= 1;
			}
		}

		void write_interlaced_elias_gamma(int value) {
			int i;
			for (i = 2; i <= value; i <<= 1)
				;
			i >>= 1;
			while ((i >>= 1) > 0) {
				write_bit(0);
				write_bit(value & i);
			}
			write_bit(1);
		}

		public byte[] compress(byte[] input_data, int input_size, ref int output_size) {
			int last_offset = INITIAL_OFFSET;
			int first = 1;
			int i;
			int best_length_size;
			int bits;
			int index;
			int offset;
			int length;
			int bits2;
			int max_offset = offset_ceiling(input_size - 1);

			block_t[] last_literal = new block_t[max_offset + 1];
			block_t[] last_match = new block_t[max_offset + 1];
			block_t[] tabOptimal = new block_t[input_size + 1];
			int[] match_length = new int[max_offset + 1];
			int[] best_length = new int[input_size + 1];
			best_length[2] = 2;
			assign(ref last_match[INITIAL_OFFSET], allocate(-1, -1, INITIAL_OFFSET, 0, null));
			for (index = 0; index < input_size; index++) {
				best_length_size = 2;
				max_offset = offset_ceiling(index);
				for (offset = 1; offset <= max_offset; offset++) {
					if (index != 0 && index >= offset && input_data[index] == input_data[index - offset]) {
						if (last_literal[offset] != null) {
							length = index - last_literal[offset].index;
							bits = last_literal[offset].bits + 1 + elias_gamma_bits(length);
							assign(ref last_match[offset], allocate(bits, index, offset, length, last_literal[offset]));
							if (tabOptimal[index] == null || tabOptimal[index].bits > bits)
								assign(ref tabOptimal[index], last_match[offset]);
						}
						if (++match_length[offset] > 1) {
							if (best_length_size < match_length[offset]) {
								bits = tabOptimal[index - best_length[best_length_size]].bits + elias_gamma_bits(best_length[best_length_size] - 1);
								do {
									best_length_size++;
									bits2 = tabOptimal[index - best_length_size].bits + elias_gamma_bits(best_length_size - 1);
									if (bits2 <= bits) {
										best_length[best_length_size] = best_length_size;
										bits = bits2;
									}
									else {
										best_length[best_length_size] = best_length[best_length_size - 1];
									}
								}
								while (best_length_size < match_length[offset]);
							}
							length = best_length[match_length[offset]];
							bits = tabOptimal[index - length].bits + 8 + elias_gamma_bits((offset - 1) / 128 + 1) + elias_gamma_bits(length - 1);
							if (last_match[offset] == null || last_match[offset].index != index || last_match[offset].bits > bits) {
								assign(ref last_match[offset], allocate(bits, index, offset, length, tabOptimal[index - length]));
								if (tabOptimal[index] == null || tabOptimal[index].bits > bits)
									assign(ref tabOptimal[index], last_match[offset]);
							}
						}
					}
					else {
						match_length[offset] = 0;
						if (last_match[offset] != null) {
							length = index - last_match[offset].index;
							bits = last_match[offset].bits + 1 + elias_gamma_bits(length) + length * 8;
							assign(ref last_literal[offset], allocate(bits, index, 0, length, last_match[offset]));
							if (tabOptimal[index] == null || tabOptimal[index].bits > bits)
								assign(ref tabOptimal[index], last_literal[offset]);
						}
					}
				}
			}
			block_t optimal = tabOptimal[input_size - 1];
			output_size = (optimal.bits + 18 + 7) / 8;
			output_data = new byte[output_size];
			block_t prev, next = null;
			while (optimal != null) {
				prev = optimal.chain;
				optimal.chain = next;
				next = optimal;
				optimal = prev;
			}
			input_index = 0;
			output_index = 0;
			bit_mask = 0;
			for (optimal = next.chain; optimal != null; optimal = optimal.chain) {
				if (optimal.offset == 0) {
					if (first != 0)
						first = 0;
					else
						write_bit(0);

					write_interlaced_elias_gamma(optimal.length);
					for (i = 0; i < optimal.length; i++) {
						write_byte(input_data[input_index]);
						read_bytes(1);
					}
				}
				else
					if (optimal.offset == last_offset) {
					write_bit(0);
					write_interlaced_elias_gamma(optimal.length);
					read_bytes(optimal.length);
				}
				else {
					write_bit(1);
					write_interlaced_elias_gamma((optimal.offset - 1) / 128 + 1);
					write_byte((byte)((255 - ((optimal.offset - 1) % 128)) << 1));
					backtrack = 1;
					write_interlaced_elias_gamma(optimal.length - 1);
					read_bytes(optimal.length);
					last_offset = optimal.offset;
				}
			}
			write_bit(1);
			write_interlaced_elias_gamma(256);
			return output_data;
		}
		/*
		int main(int argc, char* argv[]) {
			char* output_name;
			byte* input_data;
			FILE* ifp;
			FILE* ofp;
			int input_size;
			int output_size;
			int partial_counter;
			int total_counter;
			int delta;

			printf("ZX0 v1.5: Optimal data compressor by Einar Saukas\n");
			if (argc == 2) {
				output_name = (char*)malloc(strlen(argv[1]) + 5);
				strcpy(output_name, argv[1]);
				strcat(output_name, ".zx0");
			}
			else if (argc == 3) {
				output_name = argv[2];
			}
			else {
				fprintf(stderr, "Usage: %s input [output.zx0]\n", argv[0]);
				exit(1);
			}
			ifp = fopen(argv[1], "rb");
			if (!ifp) {
				fprintf(stderr, "Error: Cannot access input file %s\n", argv[1]);
				exit(1);
			}
			fseek(ifp, 0L, SEEK_END);
			input_size = ftell(ifp);
			fseek(ifp, 0L, SEEK_SET);
			if (!input_size) {
				fprintf(stderr, "Error: Empty input file %s\n", argv[1]);
				exit(1);
			}
			input_data = (byte*)malloc(input_size);
			if (!input_data) {
				fprintf(stderr, "Error: Insufficient memory\n");
				exit(1);
			}
			total_counter = 0;
			do {
				partial_counter = fread(input_data + total_counter, sizeof(char), input_size - total_counter, ifp);
				total_counter += partial_counter;
			} while (partial_counter > 0);

			if (total_counter != input_size) {
				fprintf(stderr, "Error: Cannot read input file %s\n", argv[1]);
				exit(1);
			}
			fclose(ifp);
			ofp = fopen(output_name, "wb");
			if (!ofp) {
				fprintf(stderr, "Error: Cannot create output file %s\n", output_name);
				exit(1);
			}
			output_data = compress(input_data, input_size, &output_size, &delta);
			if (fwrite(output_data, sizeof(char), output_size, ofp) != output_size) {
				fprintf(stderr, "Error: Cannot write output file %s\n", output_name);
				exit(1);
			}
			fclose(ofp);
			printf("File compressed from %d to %d bytes! (delta %d)\n", input_size, output_size, delta);
			return 0;
		}
		*/
	}
}
