using System.Collections.Generic;

namespace ConvImgCpc {
	public class Multilingue {
		private string langue;

		private Dictionary<string, string> dic = new Dictionary<string, string>();

		public Multilingue() {
			// Main
			dic.Add("Main.bpLoad.FR", "Lecture");
			dic.Add("Main.bpLoad.EN", "Read");
			dic.Add("Main.bpConvert.FR", "Conversion");
			dic.Add("Main.bpConvert.EN", "Convert");
			dic.Add("Main.label1.FR", "Nb Colonnes");
			dic.Add("Main.label1.EN", "Nb Columns");
			dic.Add("Main.label2.FR", "Nb Lignes");
			dic.Add("Main.label2.EN", "Nb Lines");
			dic.Add("Main.bpCalcSprite.FR", "Calcul auto. sprite");
			dic.Add("Main.bpCalcSprite.EN", "Auto. sprite calculation");
			dic.Add("Main.bpEditTrame.FR", "Edition trames");
			dic.Add("Main.bpEditTrame.EN", "Frames edition");
			dic.Add("Main.resoCPC.FR", "Résolution CPC");
			dic.Add("Main.resoCPC.EN", "CPC resolution");
			dic.Add("Main.chkDiffErr.FR", "Diffusion");
			dic.Add("Main.chkTrameTC.FR", "Trames[TC]");
			dic.Add("Main.chkTrameTC.EN", "[TC] Frames");
			dic.Add("Main.chkLissage.FR", "Lissage");
			dic.Add("Main.chkLissage.EN", "Smoothing");
			dic.Add("Main.tramage.FR", "Tramage et rendu");
			dic.Add("Main.tramage.EN", "Dithering and rendering");
			dic.Add("Main.chkNewReduc.FR", "Méthode alternative");
			dic.Add("Main.chkNewReduc.EN", "Alternative method");
			dic.Add("Main.chkPalCpc.FR", "Réduction palette\r\nimage source");
			dic.Add("Main.chkPalCpc.EN", "Pallet reduction");
			dic.Add("Main.autoRecalc.FR", "Recalculer Automatiquement");
			dic.Add("Main.autoRecalc.EN", "Automatic recalculation");
			dic.Add("Main.radioOrigin.FR", "Taille d'origine");
			dic.Add("Main.radioOrigin.EN", "Original size");
			dic.Add("Main.label5.FR", "Taille:");
			dic.Add("Main.label5.EN", "Size:");
			dic.Add("Main.radioUserSize.FR", "Taille utilisateur");
			dic.Add("Main.radioUserSize.EN", "User size");
			dic.Add("Main.groupBox1.FR", "Taille image source");
			dic.Add("Main.groupBox1.EN", "Source image size");
			dic.Add("Main.bpSave.FR", "Enregistrement");
			dic.Add("Main.bpSave.EN", "Save");
			dic.Add("Main.withCode.FR", "Inclure le code d'affichage dans l'image");
			dic.Add("Main.withCode.EN", "Include display code in image");
			dic.Add("Main.withPalette.FR", "Inclure la palette dans l'image");
			dic.Add("Main.withPalette.EN", "Include palette in image");
			dic.Add("Main.chkAllPics.FR", "Toutes les images");
			dic.Add("Main.chkAllPics.EN", "All images");
			dic.Add("Main.bpCreate.FR", "Création");
			dic.Add("Main.bpCreate.EN", "Create");
			dic.Add("Main.chkParamInterne.FR", "Paramètres internes");
			dic.Add("Main.chkParamInterne.EN", "Internal settings");
			dic.Add("Main.reducPal4.FR", "Réduction 4");
			dic.Add("Main.reducPal4.EN", "Reduction 4");
			dic.Add("Main.nb.FR", "Noir && blanc");
			dic.Add("Main.nb.EN", "Black && white");
			dic.Add("Main.sortPal.FR", "Trier");
			dic.Add("Main.sortPal.EN", "Sort");
			dic.Add("Main.reducPal3.FR", "Réduction 3");
			dic.Add("Main.reducPal3.EN", "Reduction 3");
			dic.Add("Main.reducPal2.FR", "Réduction 2");
			dic.Add("Main.reducPal2.EN", "Reduction 2");
			dic.Add("Main.reducPal1.FR", "Réduction 1");
			dic.Add("Main.reducPal1.EN", "Reduction 1");
			dic.Add("Main.newMethode.FR", "Plus précise");
			dic.Add("Main.newMethode.EN", "More accurate");
			dic.Add("Main.rb24bits.FR", "Couleurs 24bits");
			dic.Add("Main.rb24bits.EN", "24bits colors");
			dic.Add("Main.rb12bits.FR", "Couleurs 12bits");
			dic.Add("Main.rb12bits.EN", "12bits colors");
			dic.Add("Main.rb9bits.FR", "Couleurs 9bits");
			dic.Add("Main.rb9bits.EN", "9bits colors");
			dic.Add("Main.rb6bits.FR", "Couleurs 6bits");
			dic.Add("Main.rb6bits.EN", "6bits colors");
			dic.Add("Main.bpRaz.FR", "Raz paramètres couleurs");
			dic.Add("Main.bpRaz.EN", "Reset color parameters");
			dic.Add("Main.groupBox2.FR", "Gestion des couleurs");
			dic.Add("Main.groupBox2.EN", "Color management");
			dic.Add("Main.bpEditSprites.FR", "Edition Sprites Hard");
			dic.Add("Main.bpEditSprites.EN", "Editing Hard Sprites");
			dic.Add("Main.chkImpDraw.FR", "Mode ImpDraw");
			dic.Add("Main.chkImpDraw.EN", "ImpDraw mode");
			dic.Add("Main.rbStdPack.FR", "Compression standard");
			dic.Add("Main.rbZx0Pack.FR", "Compression ZX0");
			dic.Add("Main.rbStdPack.EN", "Standard compression");
			dic.Add("Main.rbZx0Pack.EN", "ZX0 Compression");

			// --- Libellés du programme
			dic.Add("Main.Prg.TxtInfo1.FR", "Conversion en cours...");
			dic.Add("Main.Prg.TxtInfo1.EN", "Conversion in progress...");
			dic.Add("Main.Prg.TxtInfo2.FR", "Création animation (IMP) avec ");
			dic.Add("Main.Prg.TxtInfo2.EN", "Animation creation (IMP) with ");
			dic.Add("Main.prg.TxtInfo3.FR", "Lecture image de type CPC");
			dic.Add("Main.prg.TxtInfo3.EN", "CPC type image reading");
			dic.Add("Main.prg.TxtInfo4.FR", "Lecture image PC");
			dic.Add("Main.prg.TxtInfo4.EN", "PC image reading");
			dic.Add("Main.prg.TxtInfo5.FR", " de type animation avec ");
			dic.Add("Main.prg.TxtInfo5.EN", " animation type with ");
			dic.Add("Main.prg.TxtInfo6.FR", "Impossible de lire l'image (format inconnu ???)");
			dic.Add("Main.prg.TxtInfo6.EN", "Unable to read image (unknown format ???)");
			dic.Add("Main.prg.TxtInfo7.FR", "Image sélectionnée: ");
			dic.Add("Main.prg.TxtInfo7.EN", "Selected image: ");
			dic.Add("Main.prg.TxtInfo8.FR", "Lecture paramètres ok");
			dic.Add("Main.prg.TxtInfo8.EN", "Reading parameters ok");
			dic.Add("Main.prg.TxtInfo9.FR", "Ce fichier de paramètres ne peut pas être décodé");
			dic.Add("Main.prg.TxtInfo9.EN", "This parameter file cannot be decoded");
			dic.Add("Main.prg.TxtInfo10.FR", "Sauvegarde paramètres ok");
			dic.Add("Main.prg.TxtInfo10.EN", "Save parameters ok");
			dic.Add("Main.prg.TxtInfo11.FR", "Impossible de sauvegarder le fichier de paramètres");
			dic.Add("Main.prg.TxtInfo11.EN", "Cannot save settings file");
			dic.Add("Main.prg.TxtInfo12.FR", "Lecture palette ok");
			dic.Add("Main.prg.TxtInfo12.EN", "Pallet reading ok");
			dic.Add("Main.prg.TxtInfo13.FR", "Sauvegarde palette ok");
			dic.Add("Main.prg.TxtInfo13.EN", "Pallet save ok");
			dic.Add("Main.prg.TxtInfo14.FR", "Création image vierge");
			dic.Add("Main.prg.TxtInfo14.EN", "Blank image creation");
			dic.Add("Main.prg.TxtInfo15.FR", "Création animation avec ");
			dic.Add("Main.prg.TxtInfo15.EN", "Animation creation with ");
			dic.Add("Main.prg.TxtInfo16.FR", "Images");
			dic.Add("Main.prg.TxtInfo16.EN", "Pictures");
			dic.Add("Main.prg.TxtInfo17.FR", "Tous fichiers");
			dic.Add("Main.prg.TxtInfo17.EN", "All files");
			dic.Add("Main.prg.TxtInfo18.FR", "Palette");
			dic.Add("Main.prg.TxtInfo18.EN", "Pallet");
			dic.Add("Main.prg.TxtInfo19.FR", "Paramètres ConvImgCpc");
			dic.Add("Main.prg.TxtInfo19.EN", "ConvImgCpc parameters");
			dic.Add("Main.prg.TxtInfo20.FR", "Image CPC");
			dic.Add("Main.prg.TxtInfo20.EN", "CPC image");
			dic.Add("Main.prg.TxtInfo21.FR", "Sprite assembleur");
			dic.Add("Main.prg.TxtInfo21.EN", "Assembler sprite");
			dic.Add("Main.prg.TxtInfo22.FR", "Sprite assembleur compacté");
			dic.Add("Main.prg.TxtInfo22.EN", "Compacted assembler sprite");
			dic.Add("Main.prg.TxtInfo23.FR", "Ecran compacté");
			dic.Add("Main.prg.TxtInfo23.EN", "Compacted screen");
			dic.Add("Main.prg.TxtInfo24.FR", "Ecran assembleur compacté");
			dic.Add("Main.prg.TxtInfo24.EN", "Compact assembly screen");
			dic.Add("Main.prg.TxtInfo25.FR", "Animation DeltaPack");
			dic.Add("Main.prg.TxtInfo25.EN", "DeltaPack animation");
			dic.Add("Main.prg.TxtInfo26.FR", "Animation imp");
			dic.Add("Main.prg.TxtInfo26.EN", "imp animation");
			dic.Add("Main.prg.TxtInfo27.FR", "Palette CPC+");
			dic.Add("Main.prg.TxtInfo27.EN", "CPC+ pallet");
			dic.Add("Main.prg.TxtInfo28.FR", "2 images séparées");
			dic.Add("Main.prg.TxtInfo28.EN", "2 separate images");
			dic.Add("Main.prg.TxtInfo29.FR", "Il existe une version plus récente sur le site web :");
			dic.Add("Main.prg.TxtInfo29.EN", "There is a newer version on the website :");
			dic.Add("Main.prg.TxtInfo30.FR", "");
			dic.Add("Main.prg.TxtInfo39.EN", "");

			// ImageCpc
			dic.Add("ImageCpc.lockAllPal.FR", "Verrouiller toute la palette");
			dic.Add("ImageCpc.lockAllPal.EN", "Lock all pallet");
			dic.Add("ImageCpc.modeEdition.FR", "Editer image");
			dic.Add("ImageCpc.modeEdition.EN", "Edit picture");
			dic.Add("ImageCpc.label3.FR", "Couleur crayon :");
			dic.Add("ImageCpc.label3.EN", "Pen color :");
			dic.Add("ImageCpc.label2.FR", "Taille crayon :");
			dic.Add("ImageCpc.label2.EN", "Pen size :");
			dic.Add("ImageCpc.chkDoRedo.FR", "Garder retouches");
			dic.Add("ImageCpc.chkDoRedo.EN", "Keep retouchings");
			dic.Add("ImageCpc.chkRendu.FR", "Fenêtre de rendu");
			dic.Add("ImageCpc.chkRendu.EN", "Render window");
			dic.Add("ImageCpc.modeCaptureSprites.FR", "Capture de sprites");
			dic.Add("ImageCpc.modeCaptureSprites.EN", "Sprites capture");
			dic.Add("ImageCpc.bpCopyPal.FR", "Copier palette dans presse-papier");
			dic.Add("ImageCpc.bpCopyPal.EN", "Copy palette to clipboard");

			// Animation
			dic.Add("Animation.bpSup1.FR", "Supprimer");
			dic.Add("Animation.bpSup1.EN", "Delete");
			dic.Add("Animation.bpSup2.FR", "Supprimer");
			dic.Add("Animation.bpSup2.EN", "Delete");
			dic.Add("Animation.bpSup3.FR", "Supprimer");
			dic.Add("Animation.bpSup3.EN", "Delete");
			dic.Add("Animation.bpSup4.FR", "Supprimer");
			dic.Add("Animation.bpSup4.EN", "Delete");
			dic.Add("Animation.bpSup5.FR", "Supprimer");
			dic.Add("Animation.bpSup5.EN", "Delete");
			dic.Add("Animation.rbSource.FR", "Afficher images source");
			dic.Add("Animation.rbSource.EN", "Display source pictures");
			dic.Add("Animation.rvCalculee.FR", "Afficher images calculées");
			dic.Add("Animation.rvCalculee.EN", "Display calculated pictures");
			dic.Add("Animation.bpSaveGif.FR", "Sauvegarde GIF Anim");
			dic.Add("Animation.bpSaveGif.EN", "Save as GIF Anim");
			dic.Add("Animation.lblMaxImage.FR", "Nbre images:");
			dic.Add("Animation.lblMaxImage.EN", "Nb pictures:");

			// Capture
			dic.Add("Capture.rbCapt1.FR", "Capturer 1 sprite");
			dic.Add("Capture.rbCapt1.EN", "Capture 1 sprite");
			dic.Add("Capture.rbCapt2.FR", "Capturer 2x2 sprites");
			dic.Add("Capture.rbCapt2.EN", "Capture 2x2 sprites");
			dic.Add("Capture.rbCapt4.FR", "Capturer 4x4 sprites");
			dic.Add("Capture.rbCapt4.EN", "Capture 4x4 sprites");
			dic.Add("Capture.bpCapture.FR", "Capturer");
			dic.Add("Capture.bpCapture.EN", "Capture");

			// CreationImages
			dic.Add("CreationImages.rbSingle.FR", "Image unique");
			dic.Add("CreationImages.rbSingle.EN", "Single image");
			dic.Add("CreationImages.bpCreer.FR", "Créer");
			dic.Add("CreationImages.bpCreer.EN", "Create");
			dic.Add("CreationImages.label1.FR", "Type de média à créer :");
			dic.Add("CreationImages.label1.EN", "Type of media to create :");
			dic.Add("CreationImages.lblNbImages.FR", "image(s)");
			dic.Add("CreationImages.lblNbImages.EN", "picture(s)");

			// EditColor
			dic.Add("EditColor.lblNumColor.FR", "Couleur ");
			dic.Add("EditColor.lblNumColor.EN", "Color ");
			dic.Add("EditColor.bpValide.FR", "Valider");
			dic.Add("EditColor.bpValide.EN", "Validate");
			dic.Add("EditColor.bpAnnule.FR", "Annuler");
			dic.Add("EditColor.bpAnnule.EN", "Cancel");

			// EditSprites
			dic.Add("EditSprites.bpPrev.FR", "Précédent");
			dic.Add("EditSprites.bpPrev.EN", "Previous");
			dic.Add("EditSprites.bpSuiv.FR", "Suivant");
			dic.Add("EditSprites.bpSuiv.EN", "Next");
			dic.Add("EditSprites.label1.FR", "Bp gauche");
			dic.Add("EditSprites.label1.EN", "Left bp");
			dic.Add("EditSprites.label2.FR", "Bp droite");
			dic.Add("EditSprites.label2.EN", "Right bp");
			dic.Add("EditSprites.bpRead.FR", "Lire sprites");
			dic.Add("EditSprites.bpRead.EN", "Read sprites");
			dic.Add("EditSprites.bpSave.FR", "Sauver sprites (bank courante)");
			dic.Add("EditSprites.bpSave.EN", "Save sprites (current bank)");
			dic.Add("EditSprites.bpSaveAll.FR", "Sauver sprites (toutes les banks)");
			dic.Add("EditSprites.bpSaveAll.EN", "Save sprites (all banks)");
			dic.Add("EditSprites.bpReadPal.FR", "Lire palette");
			dic.Add("EditSprites.bpReadPal.EN", "Read pallet");
			dic.Add("EditSprites.bpSavePal.FR", "Sauver palette");
			dic.Add("EditSprites.bpSavePal.EN", "Save pallet");

			// EditTrameAscii
			dic.Add("EditTrameAscii.bpPrev.FR", "Précédente");
			dic.Add("EditTrameAscii.bpPrev.EN", "Previous");
			dic.Add("EditTrameAscii.bpSuiv.FR", "Suivante");
			dic.Add("EditTrameAscii.bpSuiv.EN", "Next");
			dic.Add("EditTrameAscii.label1.FR", "Bp gauche");
			dic.Add("EditTrameAscii.label1.EN", "Left bp");
			dic.Add("EditTrameAscii.label2.FR", "Bp droite");
			dic.Add("EditTrameAscii.label2.EN", "Right bp");
			dic.Add("EditTrameAscii.bpRead.FR", "Lire trames");
			dic.Add("EditTrameAscii.bpRead.EN", "Read frames");
			dic.Add("EditTrameAscii.bpSave.FR", "Sauver trames");
			dic.Add("EditTrameAscii.bpSave.EN", "Save frames");
			dic.Add("EditTrameAscii.bpAutoGene.FR", "Génération Automatique");
			dic.Add("EditTrameAscii.bpAutoGene.EN", "Automatic Generation");

			// ParamInterne
			dic.Add("ParamInterne.groupBox1.FR", "Constantes conversion luminance");
			dic.Add("ParamInterne.groupBox1.EN", "Luminance conversion constants");
			dic.Add("ParamInterne.groupBox2.FR", "Seuil passage niveau Rouge CPC");
			dic.Add("ParamInterne.groupBox2.EN", "Red CPC level crossing threshold");
			dic.Add("ParamInterne.groupBox3.FR", "Seuil passage niveau Vert CPC");
			dic.Add("ParamInterne.groupBox3.EN", "Green CPC level crossing threshold");
			dic.Add("ParamInterne.groupBox4.FR", "Seuil passage niveau Bleu CPC");
			dic.Add("ParamInterne.groupBox4.EN", "Blue CPC level crossing threshold");
			dic.Add("ParamInterne.label4.FR", "Niveau 1");
			dic.Add("ParamInterne.label4.EN", "Level 1");
			dic.Add("ParamInterne.label5.FR", "Niveau 2");
			dic.Add("ParamInterne.label5.EN", "Level 2");
			dic.Add("ParamInterne.label6.FR", "Niveau 3");
			dic.Add("ParamInterne.label6.EN", "Level 3");
			dic.Add("ParamInterne.label7.FR", "Niveau 4");
			dic.Add("ParamInterne.label7.EN", "Level 4");
			dic.Add("ParamInterne.label8.FR", "Niveau 4");
			dic.Add("ParamInterne.label8.EN", "Level 4");
			dic.Add("ParamInterne.label9.FR", "Niveau 3");
			dic.Add("ParamInterne.label9.EN", "Level 3");
			dic.Add("ParamInterne.label10.FR", "Niveau 2");
			dic.Add("ParamInterne.label10.EN", "Level 2");
			dic.Add("ParamInterne.label11.FR", "Niveau 1");
			dic.Add("ParamInterne.label11.EN", "Level 1");
			dic.Add("ParamInterne.label12.FR", "Niveau 1");
			dic.Add("ParamInterne.label12.EN", "Level 1");
			dic.Add("ParamInterne.label13.FR", "Niveau 2");
			dic.Add("ParamInterne.label13.EN", "Level 2");
			dic.Add("ParamInterne.label14.FR", "Niveau 3");
			dic.Add("ParamInterne.label14.EN", "Level 3");
			dic.Add("ParamInterne.label15.FR", "Niveau 4");
			dic.Add("ParamInterne.label15.EN", "Level 4");

			// SaveAnim
			dic.Add("SaveAnim.bpSave.FR", "Enregistrer");
			dic.Add("SaveAnim.bpSave.EN", "Save");
			dic.Add("SaveAnim.chk128Ko.FR", "Gérer 128Ko de mémoire");
			dic.Add("SaveAnim.chk128Ko.EN", "Manage 128Kb of memory");
			dic.Add("SaveAnim.chkBoucle.FR", "Rebouclage sur la première image");
			dic.Add("SaveAnim.chkBoucle.EN", "Loop back to the first frame");
			dic.Add("SaveAnim.label1.FR", "Adresse de début :");
			dic.Add("SaveAnim.label1.EN", "Start address :");
			dic.Add("SaveAnim.chkMaxMem.FR", "Adresse mémoire à ne pas dépasser");
			dic.Add("SaveAnim.chkMaxMem.EN", "Memory address not to be exceeded");
			dic.Add("SaveAnim.chkDirecMem.FR", "Mode \'Mémoire Direct\'");
			dic.Add("SaveAnim.chkDirecMem.EN", "\'Direct Memory\' mode");
			dic.Add("SaveAnim.chkDelai.FR", "Ajout délai inter-images");
			dic.Add("SaveAnim.chkDelai.EN", "Add inter-frame delay");
			dic.Add("SaveAnim.rb1L.FR", "Générer toutes les lignes");
			dic.Add("SaveAnim.rb1L.EN", "Generate all rows");
			dic.Add("SaveAnim.rb2L.FR", "Générer 1 ligne / 2");
			dic.Add("SaveAnim.rb2L.EN", "Generate 1 line / 2");
			dic.Add("SaveAnim.rb4L.FR", "Générer 1 ligne / 4");
			dic.Add("SaveAnim.rb4L.EN", "Generate 1 line / 4");
			dic.Add("SaveAnim.rb8L.FR", "Générer 1 ligne / 8");
			dic.Add("SaveAnim.rb8L.EN", "Generate 1 line / 8");
			dic.Add("SaveAnim.chk2Zone.FR", "2 Zones par image");
			dic.Add("SaveAnim.chk2Zone.EN", "2 Zones per image");
			dic.Add("SaveAnim.chkZoneVert.FR", "Zones verticales");
			dic.Add("SaveAnim.chkZoneVert.EN", "Vertical zones");
			dic.Add("SaveAnim.chkCol.FR", "Compacter en \"colonnes\"");
			dic.Add("SaveAnim.chkCol.EN", "Compact into \"columns\"");
			dic.Add("SaveAnim.rbFrameFull.FR", "Tous types de frames");
			dic.Add("SaveAnim.rbFrameFull.EN", "All types of frames");
			dic.Add("SaveAnim.rbFrameD.FR", "Forcer frame \'D\'");
			dic.Add("SaveAnim.rbFrameD.EN", "Force frame \'D\'");
			dic.Add("SaveAnim.rbFrameO.FR", "Forcer frame \'O\'");
			dic.Add("SaveAnim.rbFrameO.EN", "Force frame \'O\'");
			dic.Add("SaveAnim.chkDataBrut.FR", "Export données \"brut\"");
			dic.Add("SaveAnim.chkDataBrut.EN", "\"Raw\" data export");

		}

		public void SetLangue(string lg) {
			langue = lg;
		}

		public string GetString(string ctrlName) {
			string key = ctrlName + "." + langue;
			if (dic.ContainsKey(key))
				return dic[key];

			return null;
		}
	}
}
