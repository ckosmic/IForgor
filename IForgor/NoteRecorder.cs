using BS_Utils.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IForgor
{
	internal class NoteRecorder : MonoBehaviour
	{
		public static NoteRecorder instance { get; private set; }

		public NoteData noteAData;
		public NoteData noteBData;
		public NoteCutInfo? noteACutInfo;
		public NoteCutInfo? noteBCutInfo;

		private void Awake() {
			if (instance != null) {
				Destroy(instance);
			}
			instance = this;
		}

		private void OnEnable() {
			BSEvents.noteWasCut += OnNoteWasCut;
			BSEvents.noteWasMissed += OnNoteWasMissed;
		}

		private void OnDisable() {
			BSEvents.noteWasCut -= OnNoteWasCut;
			BSEvents.noteWasMissed -= OnNoteWasMissed;
		}

		private void OnNoteWasCut(NoteData noteData, NoteCutInfo noteCutInfo, int multiplier) {
			ProcessNote(noteData, noteCutInfo);
		}
		private void OnNoteWasMissed(NoteData noteData, int multiplier) {
			ProcessNote(noteData, null);
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
