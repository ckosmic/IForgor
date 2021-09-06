using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace IForgor
{
	internal class UIGroup : MonoBehaviour
	{
		public ImageView bloqImage;
		public ImageView directionImage;
		public ImageView cutAngleImage;
		public ImageView saberBgImage;
		public ImageView saberFgImage;
		public NoteData noteData = null;
		public NoteCutInfo? noteCutInfo;
		public Color noteColor;
		public Color saberColor;


		public void Initialize() {
			bloqImage = new GameObject("IFBloqImage").AddComponent<ImageView>();
			bloqImage.transform.SetParent(transform, false);
			bloqImage.rectTransform.localScale = Vector3.one * 0.075f;
			bloqImage.rectTransform.localPosition = Vector3.zero;
			bloqImage.sprite = PauseUIManager.spr_bloq;
			bloqImage.type = Image.Type.Simple;
			bloqImage.color = Color.red;
			bloqImage.material = PauseUIManager.mat_UINoGlow;
			//ReflectionUtil.SetField<ImageView, float>(bloqImage, "_skew", 0.18f);
			//bloqImage.SetVerticesDirty();
			// Skew cringe? ^^

			directionImage = new GameObject("IFDirectionImage").AddComponent<ImageView>();
			directionImage.transform.SetParent(bloqImage.transform, false);
			directionImage.rectTransform.localScale = Vector3.one;
			directionImage.rectTransform.localPosition = Vector3.zero;
			directionImage.sprite = PauseUIManager.spr_dot;
			directionImage.type = Image.Type.Simple;
			directionImage.color = Color.white;
			directionImage.material = PauseUIManager.mat_UINoGlow;

			cutAngleImage = new GameObject("IFCutAngleImage").AddComponent<ImageView>();
			cutAngleImage.transform.SetParent(bloqImage.transform, false);
			cutAngleImage.rectTransform.localScale = Vector3.one * 1.2f;
			cutAngleImage.rectTransform.localPosition = Vector3.zero;
			cutAngleImage.sprite = PauseUIManager.spr_cut_arrow;
			cutAngleImage.type = Image.Type.Simple;
			cutAngleImage.color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
			cutAngleImage.material = PauseUIManager.mat_UINoGlow;
			cutAngleImage.enabled = false;

			saberBgImage = new GameObject("IFSaberBGImage").AddComponent<ImageView>();
			saberBgImage.transform.SetParent(transform, false);
			saberBgImage.rectTransform.localScale = Vector3.one * 0.075f;
			saberBgImage.rectTransform.localPosition = new Vector3(0.0f, 10.0f, 0.0f);
			saberBgImage.sprite = PauseUIManager.spr_saber_bg;
			saberBgImage.type = Image.Type.Simple;
			saberBgImage.color = Color.white;
			saberBgImage.material = PauseUIManager.mat_UINoGlow;

			saberFgImage = new GameObject("IFSaberFGImage").AddComponent<ImageView>();
			saberFgImage.transform.SetParent(saberBgImage.transform, false);
			saberFgImage.rectTransform.localScale = Vector3.one;
			saberFgImage.rectTransform.localPosition = Vector3.zero;
			saberFgImage.sprite = PauseUIManager.spr_saber_fg;
			saberFgImage.type = Image.Type.Simple;
			saberFgImage.color = Color.white;
			saberFgImage.material = PauseUIManager.mat_UINoGlow;
		}

		public void SetNoteData(NoteData noteData) {
			this.noteData = noteData;

			directionImage.sprite = PauseUIManager.spr_arrow;

			RectTransform bloqRootTransform = bloqImage.rectTransform;
			switch (noteData.cutDirection) {
				case NoteCutDirection.Down:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
					break;
				case NoteCutDirection.Up:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
					break;
				case NoteCutDirection.Left:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
					break;
				case NoteCutDirection.Right:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
					break;
				case NoteCutDirection.DownLeft:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 315f));
					break;
				case NoteCutDirection.DownRight:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
					break;
				case NoteCutDirection.UpLeft:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 225f));
					break;
				case NoteCutDirection.UpRight:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 135f));
					break;
				case NoteCutDirection.Any:
					bloqRootTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
					directionImage.sprite = PauseUIManager.spr_dot;
					break;
			}
		}

		public void SetNoteCutInfo(NoteCutInfo? noteCutInfo) {
			this.noteCutInfo = noteCutInfo;

			if (noteCutInfo != null) {
				NoteCutInfo cutInfo = ((NoteCutInfo)noteCutInfo);
				// Maffs from SliceVisualizer bc I cant even pass calc 2 (https://github.com/m1el/BeatSaber-SliceVisualizer/blob/master/Core/NsvSlicedBlock.cs)
				Vector3 cutDirection = new Vector3(-cutInfo.cutNormal.y, cutInfo.cutNormal.x, 0f);
				float cutAngle = Mathf.Atan2(cutDirection.y, cutDirection.x) * Mathf.Rad2Deg + 270.0f;
				cutAngleImage.enabled = true;
				cutAngleImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, cutAngle));
			} else {
				cutAngleImage.enabled = false;
			}
		}

		public void SetSaberAngle(float angle) {
			saberBgImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
		}

		public void SetSaberXPosition(float xPos) {
			saberBgImage.rectTransform.localPosition = new Vector3(xPos, 0.0f, 0.0f);
		}

		public void SetNoteColor(Color color) {
			noteColor = color;
			bloqImage.color = color;
		}

		public void SetSaberColor(Color color) {
			saberColor = color;
			saberBgImage.color = color;
		}
	}
}
