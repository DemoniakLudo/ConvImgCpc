using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class PackModule : Form {
		const int MAX_OFFSET_ZX0 = 32640;
		const int MAX_SCALE = 50;
		const int QTY_BLOCKS = 10000;

		Block ghost_root;
		Block[] dead_array;
		int dead_array_size = 0;
		int output_index, bit_index, bit_mask;
		bool backtrack;

		public PackModule() {
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

		private void write_bit(int value, byte[] output_data) {
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

		private void write_interlaced_elias_gamma(int value, byte[] output_data) {
			int i;
			for (i = 2; i <= value; i <<= 1)
				;
			i >>= 1;
			while ((i >>= 1) > 0) {
				write_bit(0, output_data);
				write_bit(value & i, output_data);
			}
			write_bit(1, output_data);
		}

		private int PackZX0(byte[] input_data, int input_size, byte[] output_data, int output_size) {
			Show();
			int bits, length;
			int max_offset = input_size - 1 > MAX_OFFSET_ZX0 ? MAX_OFFSET_ZX0 : input_size - 1 < 1 ? 1 : input_size - 1;
			int dots = 0;

			Block[] last_literal = new Block[max_offset + 1];
			Block[] last_match = new Block[max_offset + 1];
			Block[] tabOptimal = new Block[input_size + 1];
			int[] match_length = new int[max_offset + 1];
			int[] best_length = new int[input_size + 2];
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
						write_bit(0, output_data);

					write_interlaced_elias_gamma(optimal.length, output_data);
					for (int i = 0; i < optimal.length; i++)
						output_data[output_index++] = input_data[input_index++];
				}
				else
					if (optimal.offset == last_offset) {
						write_bit(0, output_data);
						write_interlaced_elias_gamma(optimal.length, output_data);
						input_index += optimal.length;
					}
					else {
						write_bit(1, output_data);
						write_interlaced_elias_gamma((optimal.offset - 1) / 128 + 1, output_data);
						output_data[output_index++] = (byte)((255 - ((optimal.offset - 1) % 128)) << 1);
						backtrack = true;
						write_interlaced_elias_gamma(optimal.length - 1, output_data);
						input_index += optimal.length;
						last_offset = optimal.offset;
					}
			}
			write_bit(1, output_data);
			write_interlaced_elias_gamma(256, output_data);
			Hide();
			return output_size;
		}

		private const int SEEKBACK = 0x1000;
		private const int MAXSTRING = 256;

		private int[] matches = new int[MAXSTRING];
		private int[,] matchtable = new int[MAXSTRING, SEEKBACK];

		private int Depack(byte[] bufIn, int startIn, byte[] bufOut) {
			byte a, DepackBits = 0;
			int bit, inBytes = startIn, longueur, delta, outBytes = 0;

			while (true) {
				bit = DepackBits & 1;
				DepackBits >>= 1;
				if (DepackBits == 0) {
					DepackBits = bufIn[inBytes++];
					bit = DepackBits & 1;
					DepackBits >>= 1;
					DepackBits |= 0x80;
				}
				if (bit == 0)
					bufOut[outBytes++] = bufIn[inBytes++];
				else {
					if (bufIn[inBytes] == 0)
						break; /* EOF */

					a = bufIn[inBytes];
					if ((a & 0x80) != 0) {
						longueur = 3 + ((bufIn[inBytes] >> 4) & 7);
						delta = (bufIn[inBytes++] & 15) << 8;
						delta |= bufIn[inBytes++];
						delta++;
					}
					else
						if ((a & 0x40) != 0) {
							longueur = 2;
							delta = bufIn[inBytes++] & 0x3f;
							delta++;
						}
						else
							if ((a & 0x20) != 0) {
								longueur = 2 + (bufIn[inBytes++] & 31);
								delta = bufIn[inBytes++];
								delta++;
							}
							else
								if ((a & 0x10) != 0) {
									delta = (bufIn[inBytes++] & 15) << 8;
									delta |= bufIn[inBytes++];
									longueur = bufIn[inBytes++] + 1;
									delta++;
								}
								else {
									if (bufIn[inBytes] == 15) {
										longueur = delta = bufIn[inBytes + 1] + 1;
										inBytes += 2;
									}
									else {
										if (bufIn[inBytes] > 1)
											longueur = delta = bufIn[inBytes];
										else
											longueur = delta = 256;

										inBytes++;
									}
								}
					for (; longueur-- > 0; ) {
						bufOut[outBytes] = bufOut[outBytes - delta];
						outBytes++;
					}
				}
			}
			return (outBytes);
		}

		private int Pack(byte[] bufIn, int lengthIn, byte[] bufOut, int lengthOut) {
			byte[] codebuffer = new byte[24];
			byte bits = 0;
			int count = 0, bitcount = 0, codecount = 0;
			int matchtablestart = 0, matchtableend = 0, oldmatchtablestart;
			int start, end, max, b, c, d;

			for (int i = 0; i < matches.Length; i++)
				matches[i] = 0;

			while (true) {
				for (c = matchtableend; c < count; c++) {
					b = bufIn[c];
					matchtable[b, matches[b]] = c;
					matches[b]++;
				}
				matchtableend = count;

				if (count >= 2) {
					int stlen = 0;
					int stpos = 0;
					int stlen2 = 0;
					int bb = bufIn[count];
					for (c = matches[bb] - 1; c >= 0; c--) {
						start = matchtable[bb, c];
						end = start + MAXSTRING;
						if (end > count)
							end = count;

						max = end - start;
						if (max >= stlen) {
							for (d = 1; d < max; d++)
								if (start + d >= lengthIn || count + d >= lengthIn || bufIn[start + d] != bufIn[count + d])
									break;

							if ((d >= 2) && (d > stlen)) {
								stlen = d;
								stpos = count - start;
							}
							if ((d == stlen) && (count - start < stpos))
								stpos = count - start;
						}

						if ((stlen == MAXSTRING) && (stpos == stlen))
							break;
					}
					if (count + 1 < lengthIn) {
						bb = bufIn[count + 1];
						for (c = matches[bb] - 1; c >= 0; c--) {
							start = matchtable[bb, c];
							end = start + MAXSTRING;
							if (end > count + 1)
								end = count + 1;

							max = end - start;
							if (max >= stlen2) {
								for (d = 1; d < max; d++)
									if (start + d >= lengthIn || count + d + 1 >= lengthIn || bufIn[start + d] != bufIn[count + d + 1])
										break;

								if ((d >= 2) && (d >= stlen2))
									stlen2 = d;
							}
							if (stlen2 == MAXSTRING)
								break;
						}
						if (stlen2 - 1 > stlen)
							stlen = 0;
					}

					if (stlen > 1) {
						if ((stlen == 2) && (stpos >= MAXSTRING)) {
							codebuffer[codecount++] = bufIn[count++];
							bitcount++;
						}
						else {
							if (stpos == stlen) {
								if (stlen == MAXSTRING)
									codebuffer[codecount++] = 0x1;
								else {
									if (stlen <= 14)
										codebuffer[codecount++] = (byte)stlen;
									else {
										codebuffer[codecount++] = 0x0F;
										codebuffer[codecount++] = (byte)(stlen - 1);
									}
								}
							}
							else {
								if ((stlen == 2) && (stpos < 65))
									codebuffer[codecount++] = (byte)(0x40 + stpos - 1);
								else {
									if ((stlen <= 33) && (stpos < 257)) {
										codebuffer[codecount++] = (byte)(0x20 + stlen - 2);
										codebuffer[codecount++] = (byte)(stpos - 1);
									}
									else {
										if ((stlen >= 3) && (stlen <= 10)) {
											codebuffer[codecount++] = (byte)(0x80 + ((stlen - 3) << 4) + ((stpos - 1) >> 8));
											codebuffer[codecount++] = (byte)(stpos - 1);
										}
										else {
											codebuffer[codecount++] = (byte)(0x10 + ((stpos - 1) >> 8));
											codebuffer[codecount++] = (byte)(stpos - 1);
											codebuffer[codecount++] = (byte)(stlen - 1);
										}
									}
								}
							}
							bits = (byte)(bits | (1 << bitcount));
							bitcount++;
							count += stlen;
						}
					}
					else {
						codebuffer[codecount++] = bufIn[count++];
						bitcount++;
					}
				}
				else {
					codebuffer[codecount++] = bufIn[count++];
					bitcount++;
				}
				if (bitcount == 8) {
					bufOut[lengthOut++] = bits;
					System.Array.Copy(codebuffer, 0, bufOut, lengthOut, codecount);
					lengthOut += codecount;
					bitcount = codecount = 0;
					bits = 0;
				}
				if (count >= lengthIn)
					break;

				oldmatchtablestart = matchtablestart;
				matchtablestart = count - SEEKBACK;
				if (matchtablestart < 0)
					matchtablestart = 0;

				for (c = oldmatchtablestart; c < matchtablestart; c++) {
					b = bufIn[c];
					for (d = 0; d < matches[b]; d++)
						if (matchtable[b, d] >= matchtablestart) {
							System.Buffer.BlockCopy(matchtable, ((b * SEEKBACK) + d) * sizeof(int), matchtable, b * SEEKBACK * sizeof(int), (matches[b] - d) * sizeof(int));
							break;
						}

					matches[b] -= d;
				}
			}
			codebuffer[codecount++] = 0;
			bufOut[lengthOut++] = (byte)(bits | (1 << bitcount));
			System.Array.Copy(codebuffer, 0, bufOut, lengthOut, codecount);
			return (lengthOut + codecount);
		}

		public int Depack(byte[] bufIn, int startIn, byte[] bufOut, Main.PackMethode methode) {
			//if (methode == Main.PackMethode.Standard)
			return Depack(bufIn, startIn, bufOut);
		}

		public int Pack(byte[] bufIn, int lengthIn, byte[] bufOut, int lengthOut, Main.PackMethode methode) {
			if (methode == Main.PackMethode.Standard)
				return Pack(bufIn, lengthIn, bufOut, lengthOut);

			return PackZX0(bufIn, lengthIn, bufOut, lengthOut);
		}
	}

	public class Block {
		public Block chain, ghost_chain;
		public int bits, index, offset, length, references;
	}
}
