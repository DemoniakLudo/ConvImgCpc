using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageCpc : Form {
		private Rendu fenetreRendu;
		private UndoRedo undo = new UndoRedo();
		private int offsetX = 0, offsetY = 0;
		private int zoom = 1, oldZoom = 1;
		private bool setZoomRect = false, unZoom = false;
		private int zoomRectx, zoomRecty, zoomRectw, zoomRecth;
		private int drawCol = 1, undrawCol = 0;
		private int penWidth = 1;
		private int posx = 0, posy = 0, sizex = 0, sizey = 0;
		private int memoMouseX = 0, memoMouseY = 0;
		private bool movePos = false, moveSize = false;
		private bool doDraw = false;
		private bool setCopyRect = false;
		private int copyRectx, copyRecty, copyRectw, copyRecth;
		private enum EditTool { Draw, Zoom, Copy };
		private EditTool editToolMode = EditTool.Draw;
		private DirectBitmap imgMotif;
		private DirectBitmap imgCopy;

		private void ToolModeDraw(MouseEventArgs e) {
			int yReel = e != null ? (offsetY + (e.Y / zoom)) & 0xFFE : 0;
			int mode = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (yReel & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
			int Tx = (4 >> mode);
			drawColor.BackColor = Color.FromArgb(bitmapCpc.GetColorPal(drawCol % (modeVirtuel == 6 ? 16 : 1 << Tx)).GetColorArgb);
			undrawColor.BackColor = Color.FromArgb(bitmapCpc.GetColorPal(undrawCol % (modeVirtuel == 6 ? 16 : 1 << Tx)).GetColorArgb);
			drawColor.Width = undrawColor.Width = 35 * Tx;
			drawColor.Refresh();
			undrawColor.Refresh();
			if (e == null || e.Button == MouseButtons.None) {
				if (doDraw) {
					doDraw = false;
					undo.EndUndoRedo();
				}
			}
			else {
				int pen = e.Button == MouseButtons.Left ? drawCol : undrawCol;
				for (int y = 0; y < penWidth * 2; y += 2) {
					mode = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (yReel & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
					Tx = (4 >> mode);
					int nbCol = modeVirtuel == 6 ? 16 : 1 << Tx;
					int realColor = GetPalCPC(Palette[pen % nbCol]);
					int yStart = zoom * (yReel - offsetY);
					for (int x = 0; x < penWidth * Tx; x += Tx) {
						int xReel = (x + offsetX + (e.X / zoom)) & -Tx;
						if (xReel >= 0 && yReel >= 0 && xReel < TailleX && yReel < TailleY) {
							undo.MemoUndoRedo(xReel, yReel, bmpLock.GetPixel(xReel, yReel), realColor, doDraw);
							doDraw = true;
							bmpLock.SetHorLineDouble(xReel, yReel, Tx, realColor);
							if (zoom != 1)
								for (int yz = yStart; yz < Math.Min(tmpLock.Height, yStart + (zoom << 1)); yz += 2)
									tmpLock.SetHorLineDouble(zoom * (xReel - offsetX), yz, zoom * Tx, realColor);
						}
					}
					yReel += 2;
				}
				Render();
			}
		}

		private void DoZoom() {
			vScrollBar.Visible = hScrollBar.Visible = zoom > 1;
			hScrollBar.Maximum = hScrollBar.LargeChange + TailleX - (TailleX / zoom);
			vScrollBar.Maximum = vScrollBar.LargeChange + TailleY - (TailleY / zoom);
			hScrollBar.Value = Math.Max(0, Math.Min(Math.Min(zoomRectx, zoomRectx + zoomRectw), TailleX - ((imgOrigine.Width + zoom) / zoom)));
			vScrollBar.Value = Math.Max(0, Math.Min(Math.Min(zoomRecty, zoomRecty + zoomRecth), TailleY - ((imgOrigine.Height + zoom) / zoom)));
			offsetX = (hScrollBar.Value >> 3) << 3;
			offsetY = (vScrollBar.Value >> 1) << 1;
			lblZoom.Text = zoom.ToString() + ":1";
			oldZoom = zoom;
			Render(true);
		}

		private void ToolModeZoom(MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				if (zoom == 1) {
					if (!setZoomRect) {
						setZoomRect = true;
						zoomRectx = e.X;
						zoomRecty = e.Y;
						zoomRecth = zoomRectw = 0;
					}
					else {
						Graphics g = Graphics.FromImage(pictureBox.Image);
						if (zoomRecth != 0 || zoomRectw != 0) {
							XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, zoomRectx, zoomRecty, zoomRectx + zoomRectw, zoomRecty + zoomRecth);
						}
						zoomRectw = e.X - zoomRectx;
						zoomRecth = e.Y - zoomRecty;
						XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, zoomRectx, zoomRecty, zoomRectx + zoomRectw, zoomRecty + zoomRecth);
						pictureBox.Refresh();
					}
				}
			}
			else
				if (e.Button == MouseButtons.Right) {
					if (!unZoom) {
						unZoom = true;
						if (zoom >= 2)
							zoom >>= 1;

						DoZoom();
					}
				}
				else {
					unZoom = false;
					if (setZoomRect) {
						setZoomRect = false;
						if (zoomRectw != 0 && zoomRecth != 0) {
							Graphics g = Graphics.FromImage(pictureBox.Image);
							XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, zoomRectx, zoomRecty, zoomRectx + zoomRectw, zoomRecty + zoomRecth);
							zoom = Math.Max(1, Math.Min(Math.Abs(768 / zoomRectw), Math.Abs(544 / zoomRecth)) & 0x7E);
							DoZoom();
						}
					}
				}
		}

		private void DrawCopy(MouseEventArgs e) {
			if (imgMotif != null && imgCopy != null) {
				imgCopy.CopyBits(bmpLock);
				int yReel = ((e.Y / zoom) & 0xFFE) * zoom;
				int mode = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (yReel & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				int Tx = (4 >> mode);
				int xReel = ((e.X / zoom) & -Tx) * zoom;
				if (xReel < bitmapCpc.NbCol << 3 && yReel < bitmapCpc.NbLig << 1) {
					Graphics g = Graphics.FromImage(pictureBox.Image);
					g.DrawImage(imgMotif.Bitmap, xReel, yReel);
					pictureBox.Refresh();
				}
			}
		}

		private void ToolModeCopy(MouseEventArgs e) {
			int yReel = ((e.Y / zoom) & 0xFFE) * zoom;
			int mode = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (yReel & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
			int Tx = (4 >> mode);
			int xReel = ((e.X / zoom) & -Tx) * zoom;
			if (e.Button == MouseButtons.Left) {
				if (imgMotif != null) {
					for (int y = 0; y < imgMotif.Height; y += 2) {
						mode = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? ((y + yReel) & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
						Tx = (4 >> mode);
						for (int x = 0; x < imgMotif.Width; x += Tx) {
							if (x + xReel < bitmapCpc.NbCol << 3 && y + yReel < bitmapCpc.NbLig << 1) {
								int c = imgMotif.GetPixel(x, y);
								undo.MemoUndoRedo(x + xReel, y + yReel, bmpLock.GetPixel(x + xReel, y + yReel), c, doDraw);
								bmpLock.SetHorLineDouble(x + xReel, y + yReel, Tx, c);
								doDraw = true;
							}
						}
					}
					imgCopy.CopyBits(bmpLock);
					Render();
				}
				else {
					if (!setCopyRect) {
						setCopyRect = true;
						copyRectx = xReel;
						copyRecty = yReel;
						copyRecth = copyRectw = 0;
					}
					else {
						Graphics g = Graphics.FromImage(pictureBox.Image);
						if (copyRecth != 0 || copyRectw != 0) {
							XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, copyRectx, copyRecty, copyRectx + copyRectw, copyRecty + copyRecth);
						}
						copyRectw = xReel - copyRectx;
						copyRecth = yReel - copyRecty;
						XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, copyRectx, copyRecty, copyRectx + copyRectw, copyRecty + copyRecth);
						pictureBox.Refresh();
					}
				}
			}
			else {
				if (setCopyRect) {
					setCopyRect = false;
					if (copyRectw != 0 && copyRecth != 0) {
						Graphics g = Graphics.FromImage(pictureBox.Image);
						XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, copyRectx, copyRecty, copyRectx + copyRectw, copyRecty + copyRecth);
						imgMotif = new DirectBitmap(Math.Abs(copyRectw / zoom), Math.Abs(copyRecth / zoom));
						for (int x = 0; x < imgMotif.Width; x++)
							for (int y = 0; y < imgMotif.Height; y++)
								imgMotif.SetPixel(x, y, bmpLock.GetPixel(offsetX + x + copyRectx / zoom, offsetY + y + copyRecty / zoom));

						if (zoom != 1) {
							zoom = 1;
							DoZoom();
						}
						imgCopy = new DirectBitmap(bmpLock.Width, bmpLock.Height);
						pictureBox.Image = imgCopy.Bitmap;
					}
				}
				if (doDraw) {
					doDraw = false;
					undo.EndUndoRedo();
				}
				DrawCopy(e);
				pictureBox.Refresh();
			}
		}

		private void ReleaseMotif() {
			if (imgCopy != null) {
				imgCopy.Dispose();
				imgCopy = null;
			}
			if (imgMotif != null) {
				imgMotif.Dispose();
				imgMotif = null;
			}
			pictureBox.Image = tmpLock != null ? tmpLock.Bitmap : bmpLock.Bitmap;
		}

		private void TrtMouseMove(object sender, MouseEventArgs e) {
			if (modeEdition.Checked) {
				switch (editToolMode) {
					case EditTool.Draw:
						ToolModeDraw(e);
						break;

					case EditTool.Zoom:
						ToolModeZoom(e);
						break;

					case EditTool.Copy:
						ToolModeCopy(e);
						break;
				}
				if (e.Button == MouseButtons.None) {
					bpUndo.Enabled = undo.CanUndo;
					bpRedo.Enabled = undo.CanRedo;
				}
			}
			else {
				// Déplacement/Zoom image
				if (e.Button == MouseButtons.Left) {
					if (!movePos) {
						main.GetSizePos(ref posx, ref posy, ref sizex, ref sizey);
						movePos = true;
						memoMouseX = e.X;
						memoMouseY = e.Y;
					}
					else {
						main.SetSizePos(posx + memoMouseX - e.X, posy + memoMouseY - e.Y, sizex, sizey, true);
					}
				}
				else {
					if (e.Button == MouseButtons.Right) {
						if (!moveSize) {
							main.GetSizePos(ref posx, ref posy, ref sizex, ref sizey);
							moveSize = true;
							memoMouseX = e.X;
							memoMouseY = e.Y;
						}
						else {
							main.SetSizePos(posx, posy, sizex - memoMouseX + e.X, sizey - memoMouseY + e.Y, true);
						}
					}
					else
						movePos = moveSize = false;
				}
			}
		}

		private void vScrollBar_Scroll(object sender, ScrollEventArgs e) {
			offsetY = (vScrollBar.Value >> 1) << 1;
			Render(true);
		}

		private void hScrollBar_Scroll(object sender, ScrollEventArgs e) {
			offsetX = (hScrollBar.Value >> 3) << 3;
			Render(true);
		}

		private void tailleCrayon_SelectedIndexChanged(object sender, System.EventArgs e) {
			penWidth = int.Parse(tailleCrayon.SelectedItem.ToString());
		}

		private void modeEdition_CheckedChanged(object sender, System.EventArgs e) {
			ReleaseMotif();
			zoom = 1;
			chkRendu.Checked = false;
			offsetX = offsetY = 0;
			vScrollBar.Visible = hScrollBar.Visible = setZoomRect = false;
			if (modeEdition.Checked) {
				undo.Reset();
				grpEdition.Visible = tailleCrayon.Enabled = true;
				bpUndo.Enabled = bpRedo.Enabled = false;
				tailleCrayon_SelectedIndexChanged(null, null);
			}
			else {
				CloseRendu();
				grpEdition.Visible = tailleCrayon.Enabled = false;
				Render();
			}
		}

		private void CloseRendu() {
			if (fenetreRendu != null) {
				fenetreRendu.Hide();
				fenetreRendu.Close();
				fenetreRendu.Dispose();
				fenetreRendu = null;
			}
		}

		private void chkRendu_CheckedChanged(object sender, System.EventArgs e) {
			if (chkRendu.Checked) {
				fenetreRendu = new Rendu();
				fenetreRendu.Show();
				Render();
			}
			else
				CloseRendu();
		}

		private void rbDraw_CheckedChanged(object sender, EventArgs e) {
			ReleaseMotif();
			editToolMode = EditTool.Draw;
		}

		private void rbZoom_CheckedChanged(object sender, EventArgs e) {
			ReleaseMotif();
			editToolMode = EditTool.Zoom;
		}

		private void rbCopy_CheckedChanged(object sender, EventArgs e) {
			editToolMode = EditTool.Copy;
		}

		private void bpUndo_Click(object sender, System.EventArgs e) {
			Enabled = false;
			List<MemoPoint> lst = undo.Undo();
			foreach (MemoPoint p in lst) {
				int Tx = 4 >> (modeVirtuel > 7 ? modeVirtuel - 8 : modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (p.posy & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				bmpLock.SetHorLineDouble(p.posx, p.posy, Tx, p.oldColor);
			}
			if (imgCopy != null)
				imgCopy.CopyBits(bmpLock);

			Render(true);
			bpUndo.Enabled = undo.CanUndo;
			bpRedo.Enabled = undo.CanRedo;
			Enabled = true;
		}

		private void bpRedo_Click(object sender, System.EventArgs e) {
			Enabled = false;
			List<MemoPoint> lst = undo.Redo();
			foreach (MemoPoint p in lst) {
				int Tx = 4 >> (modeVirtuel > 7 ? modeVirtuel - 8 : modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (p.posy & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				bmpLock.SetHorLineDouble(p.posx, p.posy, Tx, p.newColor);
			}
			if (imgCopy != null)
				imgCopy.CopyBits(bmpLock);

			Render(true);
			bpUndo.Enabled = undo.CanUndo;
			bpRedo.Enabled = undo.CanRedo;
			Enabled = true;
		}
	}
}
