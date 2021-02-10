using System.Collections.Generic;

namespace ConvImgCpc {
	public class Multilingue {
		private string langue;

		private Dictionary<string, string> dicLng = new Dictionary<string, string>();

		public Multilingue() {
			// Main
			dicLng.Add("Main.bpLoad.FR", "Lecture");
			dicLng.Add("Main.bpLoad.EN", "Read");
			dicLng.Add("Main.bpConvert.FR", "Conversion");
			dicLng.Add("Main.bpConvert.EN", "Convert");
			dicLng.Add("Main.label1.FR", "Nb Colonnes");
			dicLng.Add("Main.label1.EN", "Nb Columns");
			dicLng.Add("Main.label2.FR", "Nb Lignes");
			dicLng.Add("Main.label2.EN", "Nb Lines");
			dicLng.Add("Main.bpCalcSprite.FR", "Calcul auto. sprite");
			dicLng.Add("Main.bpCalcSprite.EN", "Auto. sprite calculation");
			dicLng.Add("Main.bpEditTrame.FR", "Edition trames");
			dicLng.Add("Main.bpEditTrame.EN", "Frames edition");
			dicLng.Add("Main.resoCPC.FR", "Résolution CPC");
			dicLng.Add("Main.resoCPC.EN", "CPC resolution");
			dicLng.Add("Main.chkDiffErr.FR", "Diffusion");
			dicLng.Add("Main.chkTrameTC.FR", "Trames[TC]");
			dicLng.Add("Main.chkTrameTC.EN", "[TC] Frames");
			dicLng.Add("Main.chkLissage.FR", "Lissage");
			dicLng.Add("Main.chkLissage.EN", "Smoothing");
			dicLng.Add("Main.tramage.FR", "Tramage et rendu");
			dicLng.Add("Main.tramage.EN", "Dithering and rendering");
			dicLng.Add("Main.chkNewReduc.FR", "Méthode alternative");
			dicLng.Add("Main.chkNewReduc.EN", "Alternative method");
			dicLng.Add("Main.chkPalCpc.FR", "Réduction palette\r\nimage source");
			dicLng.Add("Main.chkPalCpc.EN", "Pallet reduction");
			dicLng.Add("Main.autoRecalc.FR", "Recalculer Automatiquement");
			dicLng.Add("Main.autoRecalc.EN", "Automatic recalculation");
			dicLng.Add("Main.radioOrigin.FR", "Taille d'origine");
			dicLng.Add("Main.radioOrigin.EN", "Original size");
			dicLng.Add("Main.label5.FR", "Taille:");
			dicLng.Add("Main.label5.EN", "Size:");
			dicLng.Add("Main.radioUserSize.FR", "Taille utilisateur");
			dicLng.Add("Main.radioUserSize.EN", "User size");
			dicLng.Add("Main.groupBox1.FR", "Taille image source");
			dicLng.Add("Main.groupBox1.EN", "Source image size");
			dicLng.Add("Main.bpSave.FR", "Enregistrement");
			dicLng.Add("Main.bpSave.EN", "Save");
			dicLng.Add("Main.withCode.FR", "Inclure le code d'affichage dans l'image");
			dicLng.Add("Main.withCode.EN", "Include display code in image");
			dicLng.Add("Main.withPalette.FR", "Inclure la palette dans l'image");
			dicLng.Add("Main.withPalette.EN", "Include palette in image");
			dicLng.Add("Main.chkAllPics.FR", "Toutes les images");
			dicLng.Add("Main.chkAllPics.EN", "All images");
			dicLng.Add("Main.bpCreate.FR", "Création");
			dicLng.Add("Main.bpCreate.EN", "Create");
			dicLng.Add("Main.chkParamInterne.FR", "Paramètres internes");
			dicLng.Add("Main.chkParamInterne.EN", "Internal settings");
			dicLng.Add("Main.reducPal4.FR", "Réduction 4");
			dicLng.Add("Main.reducPal4.EN", "Reduction 4");
			dicLng.Add("Main.nb.FR", "Noir && blanc");
			dicLng.Add("Main.nb.EN", "Black && white");
			dicLng.Add("Main.sortPal.FR", "Trier");
			dicLng.Add("Main.sortPal.EN", "Sort");
			dicLng.Add("Main.reducPal3.FR", "Réduction 3");
			dicLng.Add("Main.reducPal3.EN", "Reduction 3");
			dicLng.Add("Main.reducPal2.FR", "Réduction 2");
			dicLng.Add("Main.reducPal2.EN", "Reduction 2");
			dicLng.Add("Main.reducPal1.FR", "Réduction 1");
			dicLng.Add("Main.reducPal1.EN", "Reduction 1");
			dicLng.Add("Main.newMethode.FR", "Plus précise");
			dicLng.Add("Main.newMethode.EN", "More accurate");
			dicLng.Add("Main.rb24bits.FR", "Couleurs 24bits");
			dicLng.Add("Main.rb24bits.EN", "24bits colors");
			dicLng.Add("Main.rb12bits.FR", "Couleurs 12bits");
			dicLng.Add("Main.rb12bits.EN", "12bits colors");
			dicLng.Add("Main.rb9bits.FR", "Couleurs 9bits");
			dicLng.Add("Main.rb9bits.EN", "9bits colors");
			dicLng.Add("Main.rb6bits.FR", "Couleurs 6bits");
			dicLng.Add("Main.rb6bits.EN", "6bits colors");
			dicLng.Add("Main.bpRaz.FR", "Raz paramètres couleurs");
			dicLng.Add("Main.bpRaz.EN", "Reset color parameters");
			dicLng.Add("Main.groupBox2.FR", "Gestion des couleurs");
			dicLng.Add("Main.groupBox2.EN", "Color management");
			dicLng.Add("Main.bpEditSprites.FR", "Edition Sprites Hard");
			dicLng.Add("Main.bpEditSprites.EN", "Editing Hard Sprites");

			// ImageCpc
			dicLng.Add("ImageCpc.lockAllPal.FR", "Tout vérouiller");
			dicLng.Add("ImageCpc.lockAllPal.EN", "Lock all");
			dicLng.Add("ImageCpc.modeEdition.FR", "Editer image");
			dicLng.Add("ImageCpc.modeEdition.EN", "Edit picture");
			dicLng.Add("ImageCpc.label3.FR", "Couleur crayon :");
			dicLng.Add("ImageCpc.label3.EN", "Pen color :");
			dicLng.Add("ImageCpc.label2.FR", "Taille crayon :");
			dicLng.Add("ImageCpc.label2.EN", "Pen size :");
			dicLng.Add("ImageCpc.chkDoRedo.FR", "Garder retouches");
			dicLng.Add("ImageCpc.chkDoRedo.EN", "Keep retouchings");
			dicLng.Add("ImageCpc.chkRendu.FR", "Fenêtre de rendu");
			dicLng.Add("ImageCpc.chkRendu.EN", "Render window");
			dicLng.Add("ImageCpc.modeCaptureSprites.FR", "Capture de sprites");
			dicLng.Add("ImageCpc.modeCaptureSprites.EN", "Sprites capture");
			dicLng.Add("ImageCpc.bpCopyPal.FR", "Copier palette dans presse-papier");
			dicLng.Add("ImageCpc.bpCopyPal.EN", "Copy palette to clipboard");

			// Animation
			dicLng.Add("Animation.bpSup1.FR", "Supprimer");
			dicLng.Add("Animation.bpSup1.EN", "Delete");
			dicLng.Add("Animation.bpSup2.FR", "Supprimer");
			dicLng.Add("Animation.bpSup2.EN", "Delete");
			dicLng.Add("Animation.bpSup3.FR", "Supprimer");
			dicLng.Add("Animation.bpSup3.EN", "Delete");
			dicLng.Add("Animation.bpSup4.FR", "Supprimer");
			dicLng.Add("Animation.bpSup4.EN", "Delete");
			dicLng.Add("Animation.bpSup5.FR", "Supprimer");
			dicLng.Add("Animation.bpSup5.EN", "Delete");
			dicLng.Add("Animation.rbSource.FR", "Affichier images source");
			dicLng.Add("Animation.rbSource.EN", "Display source pictures");
			dicLng.Add("Animation.rvCalculee.FR", "Affichier images calculées");
			dicLng.Add("Animation.rvCalculee.EN", "Display calculated pictures");
			dicLng.Add("Animation.bpSaveGif.FR", "Sauvegarde GIF Anim");
			dicLng.Add("Animation.bpSaveGif.EN", "Save as GIF Anim");
			dicLng.Add("Animation.lblMaxImage.FR", "Nbre images:");
			dicLng.Add("Animation.lblMaxImage.EN", "Nb pictures:");

			// Capture
			dicLng.Add("Capture.rbCapt1.FR", "Capturer 1 sprite");
			dicLng.Add("Capture.rbCapt1.EN", "Capture 1 sprite");
			dicLng.Add("Capture.rbCapt2.FR", "Capturer 2x2 sprites");
			dicLng.Add("Capture.rbCapt2.EN", "Capture 2x2 sprites");
			dicLng.Add("Capture.rbCapt4.FR", "Capturer 4x4 sprites");
			dicLng.Add("Capture.rbCapt4.EN", "Capture 4x4 sprites");
			dicLng.Add("Capture.bpCapture.FR", "Capturer");
			dicLng.Add("Capture.bpCapture.EN", "Capture");

			// CreationImages
			dicLng.Add("CreationImages.rbSingle.FR", "Image unique");
			dicLng.Add("CreationImages.rbSingle.EN", "Single image");
			dicLng.Add("CreationImages.bpCreer.FR", "Créer");
			dicLng.Add("CreationImages.bpCreer.EN", "Create");
			dicLng.Add("CreationImages.label1.FR", "Type de média à créer :");
			dicLng.Add("CreationImages.label1.EN", "Type of media to create :");
			dicLng.Add("CreationImages.lblNbImages.FR", "image(s)");
			dicLng.Add("CreationImages.lblNbImages.EN", "picture(s)");

			// EditColor
			dicLng.Add("EditColor.lblNumColor.FR", "Couleur ");
			dicLng.Add("EditColor.lblNumColor.EN", "Color ");
			dicLng.Add("EditColor.bpValide.FR", "Valider");
			dicLng.Add("EditColor.bpValide.EN", "Validate");
			dicLng.Add("EditColor.bpAnnule.FR", "Annuler");
			dicLng.Add("EditColor.bpAnnule.EN", "Cancel");

			// EditSprites
			dicLng.Add("EditSprites.bpPrev.FR", "Précédent");
			dicLng.Add("EditSprites.bpPrev.EN", "Previous");
			dicLng.Add("EditSprites.bpSuiv.FR", "Suivant");
			dicLng.Add("EditSprites.bpSuiv.EN", "Next");
			dicLng.Add("EditSprites.label1.FR", "Bp gauche");
			dicLng.Add("EditSprites.label1.EN", "Left bp");
			dicLng.Add("EditSprites.label2.FR", "Bp droite");
			dicLng.Add("EditSprites.label2.EN", "Right bp");
			dicLng.Add("EditSprites.bpRead.FR", "Lire sprites");
			dicLng.Add("EditSprites.bpRead.EN", "Read sprites");
			dicLng.Add("EditSprites.bpSave.FR", "Sauver sprites (bank courante)");
			dicLng.Add("EditSprites.bpSave.EN", "Save sprites (current bank)");
			dicLng.Add("EditSprites.bpSaveAll.FR", "Sauver sprites (toutes les banks)");
			dicLng.Add("EditSprites.bpSaveAll.EN", "Save sprites (all banks)");
			dicLng.Add("EditSprites.bpReadPal.FR", "Lire palette");
			dicLng.Add("EditSprites.bpReadPal.EN", "Read pallet");
			dicLng.Add("EditSprites.bpSavePal.FR", "Sauver palette");
			dicLng.Add("EditSprites.bpSavePal.EN", "Save pallet");

			// EditTrameAscii
			dicLng.Add("EditTrameAscii.bpPrev.FR", "Précédente");
			dicLng.Add("EditTrameAscii.bpPrev.EN", "Previous");
			dicLng.Add("EditTrameAscii.bpSuiv.FR", "Suivante");
			dicLng.Add("EditTrameAscii.bpSuiv.EN", "Next");
			dicLng.Add("EditTrameAscii.label1.FR", "Bp gauche");
			dicLng.Add("EditTrameAscii.label1.EN", "Left bp");
			dicLng.Add("EditTrameAscii.label2.FR", "Bp droite");
			dicLng.Add("EditTrameAscii.label2.EN", "Right bp");
			dicLng.Add("EditTrameAscii.bpRead.FR", "Lire trames");
			dicLng.Add("EditTrameAscii.bpRead.EN", "Read frames");
			dicLng.Add("EditTrameAscii.bpSave.FR", "Sauver trames");
			dicLng.Add("EditTrameAscii.bpSave.EN", "Save frames");
			dicLng.Add("EditTrameAscii.bpAutoGene.FR", "Génération Automatique");
			dicLng.Add("EditTrameAscii.bpAutoGene.EN", "Automatic Generation");

		}

		public void SetLangue(string lg) {
			langue = lg;
		}

		public string GetString(string ctrlName) {
			string key = ctrlName + "." + langue;
			if (dicLng.ContainsKey(key))
				return dicLng[key];

			return null;
		}
	}
}
