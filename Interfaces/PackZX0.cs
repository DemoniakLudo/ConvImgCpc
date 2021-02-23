using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class PackZX0 : Form {
		const int MAX_OFFSET_ZX0 = 32640;
		const int MAX_SCALE = 50;
		const int QTY_BLOCKS = 10000;

		Block ghost_root;
		Block[] dead_array;
		int dead_array_size = 0;
		byte[] output_data;
		int output_index, bit_index, bit_mask;
		bool backtrack;

		public PackZX0() {
			InitializeComponent();
		}

		private Block allocate(int bits, int index, int offset, int length, Block chain) {
			Block ptr;
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
					dead_array = new Block[QTY_BLOCKS];
					for (int i = 0; i < QTY_BLOCKS; i++)
						dead_array[i] = new Block();

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

		private void assign(ref Block ptr, Block chain) {
			chain.references++;
			if (ptr != null) {
				if (--ptr.references == 0) {
					ptr.ghost_chain = ghost_root;
					ghost_root = ptr;
				}
			}
			ptr = chain;
		}

		private int elias_gamma_bits(int value) {
			int bits = 1;
			while (value > 1) {
				bits += 2;
				value >>= 1;
			}
			return bits;
		}

		private void write_bit(int value) {
			if (backtrack) {
				if (value != 0)
					output_data[output_index - 1] |= 1;

				backtrack = false;
			}
			else {
				if (bit_mask == 0) {
					bit_mask = 128;
					bit_index = output_index;
					output_data[output_index++] = 0;
				}
				if (value != 0)
					output_data[bit_index] |= (byte)bit_mask;

				bit_mask >>= 1;
			}
		}

		private void write_interlaced_elias_gamma(int value) {
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

		public byte[] Pack(byte[] input_data, int input_size, ref int output_size) {
			int bits, length;
			int max_offset = input_size - 1 > MAX_OFFSET_ZX0 ? MAX_OFFSET_ZX0 : input_size - 1 < 1 ? 1 : input_size - 1;
			int dots = 0;

			Block[] last_literal = new Block[max_offset + 1];
			Block[] last_match = new Block[max_offset + 1];
			Block[] tabOptimal = new Block[input_size + 1];
			int[] match_length = new int[max_offset + 1];
			int[] best_length = new int[input_size + 1];
			best_length[2] = 2;
			assign(ref last_match[1], allocate(-1, -1, 1, 0, null));
			for (int index = 0; index < input_size; index++) {
				int best_length_size = 2;
				max_offset = index > MAX_OFFSET_ZX0 ? MAX_OFFSET_ZX0 : index < 1 ? 1 : index;
				for (int offset = 1; offset <= max_offset; offset++) {
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
									int bits2 = tabOptimal[index - best_length_size].bits + elias_gamma_bits(best_length_size - 1);
									if (bits2 <= bits) {
										best_length[best_length_size] = best_length_size;
										bits = bits2;
									}
									else
										best_length[best_length_size] = best_length[best_length_size - 1];
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
				if (index * MAX_SCALE / input_size > dots) {
					progressBar1.Value = 100 * ++dots / MAX_SCALE;
					Application.DoEvents();
				}
			}
			Block prev, next = null, optimal = tabOptimal[input_size - 1];
			output_size = (optimal.bits + 18 + 7) / 8;
			output_data = new byte[output_size];
			while (optimal != null) {
				prev = optimal.chain;
				optimal.chain = next;
				next = optimal;
				optimal = prev;
			}
			output_index = 0;
			bit_mask = 0;
			int last_offset = 1;
			int input_index = 0;
			bool first = true;
			for (optimal = next.chain; optimal != null; optimal = optimal.chain) {
				if (optimal.offset == 0) {
					if (first)
						first = false;
					else
						write_bit(0);

					write_interlaced_elias_gamma(optimal.length);
					for (int i = 0; i < optimal.length; i++)
						output_data[output_index++] = input_data[input_index++];
				}
				else
					if (optimal.offset == last_offset) {
					write_bit(0);
					write_interlaced_elias_gamma(optimal.length);
					input_index += optimal.length;
				}
				else {
					write_bit(1);
					write_interlaced_elias_gamma((optimal.offset - 1) / 128 + 1);
					output_data[output_index++] = (byte)((255 - ((optimal.offset - 1) % 128)) << 1);
					backtrack = true;
					write_interlaced_elias_gamma(optimal.length - 1);
					input_index += optimal.length;
					last_offset = optimal.offset;
				}
			}
			write_bit(1);
			write_interlaced_elias_gamma(256);
			return output_data;
		}
	}

	public class Block {
		public Block chain, ghost_chain;
		public int bits, index, offset, length, references;
	}
}
