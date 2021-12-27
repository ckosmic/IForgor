using IPA.Utilities;
using System;
using System.Linq;
using UnityEngine;
using HMUI;
using UnityEngine.UI;
using Zenject;

namespace IForgor
{
	internal class PauseUIManager : IInitializable, IDisposable
	{
		private readonly AssetLoader _assetLoader;
		private readonly NoteRecorder _noteRecorder;
		private readonly SaberRecorder _saberRecorder;
		private readonly DiContainer _container;

		private Transform _pauseCanvasTransform;
		private ColorManager _colorManager;
		private PauseController _pauseController;

		private bool _groupANullified = true;
		private bool _groupBNullified = true;

		public UIGroup groupA;
		public UIGroup groupB;

		public PauseUIManager(AssetLoader assetLoader, NoteRecorder noteRecorder, SaberRecorder saberRecorder,
			DiContainer container, ColorManager colorManager, PauseController pauseController)
		{
			_assetLoader = assetLoader;
			_noteRecorder = noteRecorder;
			_saberRecorder = saberRecorder;
			_container = container;
			_colorManager = colorManager;
			_pauseController = pauseController;
		}
		
		public void Initialize() {
			CreateUIElements();
			_pauseController.didPauseEvent += OnPause;
		}

		public void Dispose() {
			_pauseController.didPauseEvent -= OnPause;
		}

		private void CreateUIElements() {
			if (_pauseCanvasTransform == null)
				_pauseCanvasTransform = Resources.FindObjectsOfTypeAll<PauseMenuManager>().FirstOrDefault().transform.Find("Wrapper").Find("MenuWrapper").Find("Canvas").transform;

			Sprite spr_RoundRect10 = _pauseCanvasTransform.Find("MainBar").Find("LevelBarSimple").Find("BG").GetComponent<ImageView>().sprite;

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
			background.material = _assetLoader.mat_UINoGlow;
			background.SetField<ImageView, float>("_skew", 0.18f);
			background.SetAllDirty();

			groupA = _container.InstantiateComponentOnNewGameObject<UIGroup>("IFUIBloq_TypeA");
			groupB = _container.InstantiateComponentOnNewGameObject<UIGroup>("IFUIBloq_TypeB");

			groupA.Initialize();
			groupB.Initialize();

			groupA.SetNoteColor(_colorManager.ColorForType(ColorType.ColorA));
			groupB.SetNoteColor(_colorManager.ColorForType(ColorType.ColorB));
			groupA.SetSaberColor(_colorManager.ColorForSaberType(SaberType.SaberA));
			groupB.SetSaberColor(_colorManager.ColorForSaberType(SaberType.SaberB));

			groupA.SetSaberXPosition(-9.0f);
			groupB.SetSaberXPosition(9.0f);

			Transform groupATransform  = groupA.transform;
			groupATransform.SetParent(background.transform, false);
			groupATransform.localPosition = new Vector3(-5.0f, 0.0f, 0.0f);
			Transform groupBTransform  = groupB.transform;
			groupBTransform.SetParent(background.transform, false);
			groupBTransform.localPosition = new Vector3(5.0f, 0.0f, 0.0f);
		}

		private void OnPause() {
			if (_noteRecorder.noteAData != null) {
				if (_groupANullified && _colorManager != null) {
					groupA.SetNoteColor(_colorManager.ColorForType(ColorType.ColorA));
				}
				_groupANullified = false;
				groupA.SetNoteData(_noteRecorder.noteAData);
			} else {
				if (_colorManager != null) {
					groupA.SetNoteColor(Color.gray);
				}
				_groupANullified = true;
			}
			if (_noteRecorder.noteBData != null) {
				if (_groupBNullified && _colorManager != null) {
					groupB.SetNoteColor(_colorManager.ColorForType(ColorType.ColorB));
				}
				_groupBNullified = false;
				groupB.SetNoteData(_noteRecorder.noteBData);
			} else {
				if (_colorManager != null) {
					groupB.SetNoteColor(Color.gray);
				}
				_groupBNullified = true;
			}

			groupA.SetNoteCutInfo(_noteRecorder.noteACutInfo);
			groupB.SetNoteCutInfo(_noteRecorder.noteBCutInfo);

			_saberRecorder.RecordSaberAngles();
			groupA.SetSaberAngle(_saberRecorder.saberAAngle);
			groupB.SetSaberAngle(_saberRecorder.saberBAngle);
		}
	}
}
