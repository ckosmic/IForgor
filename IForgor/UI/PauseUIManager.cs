using IPA.Utilities;
using IForgor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HMUI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

namespace IForgor
{
	internal class PauseUIManager : IInitializable, IDisposable
	{
		private Transform _pauseCanvasTransform;
		private ColorManager _colorManager;
		private PauseController _pauseController;

		private bool _groupANullified = true;
		private bool _groupBNullified = true;

		public static Sprite spr_bloq = ResourceUtilities.LoadSpriteFromResource("IForgor.Resources.bloq.png");
		public static Sprite spr_arrow = ResourceUtilities.LoadSpriteFromResource("IForgor.Resources.arrow.png");
		public static Sprite spr_dot = ResourceUtilities.LoadSpriteFromResource("IForgor.Resources.dot.png");
		public static Sprite spr_cut_arrow = ResourceUtilities.LoadSpriteFromResource("IForgor.Resources.cut_arrow.png");
		public static Sprite spr_saber_bg = ResourceUtilities.LoadSpriteFromResource("IForgor.Resources.saber_bg.png");
		public static Sprite spr_saber_fg = ResourceUtilities.LoadSpriteFromResource("IForgor.Resources.saber_fg.png");

		public static Material mat_UINoGlow;

		public UIGroup groupA;
		public UIGroup groupB;

		internal void CreateUIElements() {
			if (_pauseCanvasTransform == null)
				_pauseCanvasTransform = Resources.FindObjectsOfTypeAll<PauseMenuManager>().FirstOrDefault().transform.Find("Wrapper").Find("MenuWrapper").Find("Canvas").transform;

			Sprite spr_RoundRect10 = _pauseCanvasTransform.Find("MainBar").Find("LevelBarSimple").Find("BG").GetComponent<ImageView>().sprite;
			mat_UINoGlow = new Material(Resources.FindObjectsOfTypeAll<Material>().Where(m => m.name == "UINoGlow").First());
			mat_UINoGlow.name = "UINoGlowEvenMoreCustomThanBSMLLOLnojk";

			RectTransform uiContainer = new GameObject("IFUIContainer", typeof(RectTransform)).GetComponent<RectTransform>();
			uiContainer.SetParent(_pauseCanvasTransform, false);
			uiContainer.localScale = Vector3.one;
			uiContainer.localPosition = Vector3.zero;
			uiContainer.sizeDelta = new Vector2(3.0f, 10.0f);

			ImageView background = new GameObject("IFUIBackground").AddComponent<ImageView>();
			background.transform.SetParent(uiContainer.transform, false);
			background.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			background.rectTransform.localPosition = new Vector3(0.0f, 14.0f, 0.0f);
			background.rectTransform.sizeDelta = new Vector2(40.0f, 10.0f);
			background.sprite = spr_RoundRect10;
			background.type = Image.Type.Sliced;
			background.color = new Color(0.125f, 0.125f, 0.125f, 0.75f);
			background.material = mat_UINoGlow;
			IPA.Utilities.ReflectionUtil.SetField<ImageView, float>(background, "_skew", 0.18f);
			background.SetAllDirty();

			groupA = new GameObject("IFUIBloq_TypeA").AddComponent<UIGroup>();
			groupB = new GameObject("IFUIBloq_TypeB").AddComponent<UIGroup>();

			groupA.Initialize();
			groupB.Initialize();

			groupA.SetNoteColor(_colorManager.ColorForType(ColorType.ColorA));
			groupB.SetNoteColor(_colorManager.ColorForType(ColorType.ColorB));
			groupA.SetSaberColor(_colorManager.ColorForSaberType(SaberType.SaberA));
			groupB.SetSaberColor(_colorManager.ColorForSaberType(SaberType.SaberB));

			groupA.SetSaberXPosition(-9.0f);
			groupB.SetSaberXPosition(9.0f);

			groupA.transform.SetParent(background.transform, false);
			groupB.transform.SetParent(background.transform, false);
			groupA.transform.localPosition = new Vector3(-5.0f, 0.0f, 0.0f);
			groupB.transform.localPosition = new Vector3(5.0f, 0.0f, 0.0f);
		}

		public void Initialize() {
			if (_colorManager == null)
				_colorManager = IPA.Utilities.ReflectionUtil.GetField<ColorManager, ColorNoteVisuals>(Resources.FindObjectsOfTypeAll<ColorNoteVisuals>().FirstOrDefault(), "_colorManager");
			if(_pauseController == null)
				_pauseController = Resources.FindObjectsOfTypeAll<PauseController>().LastOrDefault();

			CreateUIElements();
			_pauseController.didPauseEvent += OnPause;
		}

		public void Dispose() {
			_pauseController.didPauseEvent -= OnPause;
		}

		public void OnPause() {
			if (NoteRecorder.instance.noteAData != null) {
				if (_groupANullified && _colorManager != null) {
					groupA.SetNoteColor(_colorManager.ColorForType(ColorType.ColorA));
				}
				_groupANullified = false;
				groupA.SetNoteData(NoteRecorder.instance.noteAData);
			} else {
				if (_colorManager != null) {
					groupA.SetNoteColor(Color.gray);
				}
				_groupANullified = true;
			}
			if (NoteRecorder.instance.noteBData != null) {
				if (_groupBNullified && _colorManager != null) {
					groupB.SetNoteColor(_colorManager.ColorForType(ColorType.ColorB));
				}
				_groupBNullified = false;
				groupB.SetNoteData(NoteRecorder.instance.noteBData);
			} else {
				if (_colorManager != null) {
					groupB.SetNoteColor(Color.gray);
				}
				_groupBNullified = true;
			}

			groupA.SetNoteCutInfo(NoteRecorder.instance.noteACutInfo);
			groupB.SetNoteCutInfo(NoteRecorder.instance.noteBCutInfo);


			SaberRecorder.instance.RecordSaberAngles();
			groupA.SetSaberAngle(SaberRecorder.instance.saberAAngle);
			groupB.SetSaberAngle(SaberRecorder.instance.saberBAngle);
		}
	}
}
