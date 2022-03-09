using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IForgor.Recorders
{
	internal class NoteRecorder : IInitializable, IDisposable
	{
		private readonly BeatmapObjectManager _beatmapObjectManager;

		public NoteData noteAData;
		public NoteData noteBData;
		public NoteCutInfo? noteACutInfo;
		public NoteCutInfo? noteBCutInfo;

		public NoteRecorder(BeatmapObjectManager beatmapObjectManager) {
			_beatmapObjectManager = beatmapObjectManager;
		}

		public void Initialize() {
			noteAData = null;
			noteBData = null;
			noteACutInfo = null;
			noteBCutInfo = null;

			_beatmapObjectManager.noteWasCutEvent += OnNoteWasCut;
			_beatmapObjectManager.noteWasMissedEvent += OnNoteWasMissed;
		}

		public void Dispose() {
			_beatmapObjectManager.noteWasCutEvent -= OnNoteWasCut;
			_beatmapObjectManager.noteWasMissedEvent -= OnNoteWasMissed;
		}

		private void OnNoteWasCut(NoteController noteController, in NoteCutInfo noteCutInfo) {
			if (noteController.noteData.colorType == ColorType.None) return;
			if (!noteCutInfo.saberTypeOK)
			{
				ProcessNote(noteController.noteData, null);
			}
			else
			{
				ProcessNote(noteController.noteData, noteCutInfo);
			}
		}
		private void OnNoteWasMissed(NoteController noteController) {
			ProcessNote(noteController.noteData, null);
		}

		private void ProcessNote(NoteData noteData, NoteCutInfo? noteCutInfo) {
			if (noteData.colorType == ColorType.ColorA)
			{
				noteAData = noteData;
				noteACutInfo = noteCutInfo;
			}
			else if (noteData.colorType == ColorType.ColorB)
			{
				noteBData = noteData;
				noteBCutInfo = noteCutInfo;
			}
		}
	}
}
