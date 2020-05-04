[assembly: System.CLSCompliant(true)]

namespace ConvImgCpc {
	public static class PackDepack {
		private const int SEEKBACK = 0x1000;
		private const int MAXSTRING = 256;

		private static int[] matches = new int[MAXSTRING];
		private static int[,] matchtable = new int[MAXSTRING, SEEKBACK];

		static public int Depack(byte[] bufIn, int startIn, byte[] bufOut) {
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

		static public int Pack(byte[] bufIn, int lengthIn, byte[] bufOut) {
			byte[] codebuffer = new byte[24];
			byte bits = 0;
			int count = 0, bitcount = 0, codecount = 0, lengthOut = 0;
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
	}
}
