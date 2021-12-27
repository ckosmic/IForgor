using System;
using Zenject;

namespace IForgor
{
	internal class NoteRecorder : IInitializable, IDisposable
	{

		public static NoteRecorder instance { get; private set; }

		public NoteData noteAData;
		public NoteData noteBData;
		public NoteCutInfo? noteACutInfo;
		public NoteCutInfo? noteBCutInfo;

		private BeatmapObjectManager _beatmapObjectManager;

		public NoteRecorder(BeatmapObjectManager beatmapObjectManager) {
			_beatmapObjectManager = beatmapObjectManager;
		}

		public void Initialize() {
			instance = this;

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
			ProcessNote(noteController.noteData, noteCutInfo);
		}
		private void OnNoteWasMissed(NoteController noteController) {
			ProcessNote(noteController.noteData, null);
		}

		private void ProcessNote(NoteData noteData, NoteCutInfo? noteCutInfo) {
			if (noteData != null) {
				if (noteData.colorType == ColorType.ColorA)
					noteAData = noteData;
				else if (noteData.colorType == ColorType.ColorB)
					noteBData = noteData;
			}

			if (noteData.colorType == ColorType.ColorA)
				noteACutInfo = noteCutInfo;
			else if (noteData.colorType == ColorType.ColorB)
				noteBCutInfo = noteCutInfo;
		}
	}
}
