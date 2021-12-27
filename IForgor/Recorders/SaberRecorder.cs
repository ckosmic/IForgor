using UnityEngine;

namespace IForgor.Recorders
{
	internal class SaberRecorder
	{
		private readonly Saber _saberA;
		private readonly Saber _saberB;

		public float saberAAngle = 0.0f;
		public float saberBAngle = 0.0f;

		public SaberRecorder(SaberManager saberManager)
		{
			_saberA = saberManager.leftSaber;
			_saberB = saberManager.rightSaber;
		}

		public void RecordSaberAngles() {
			RecordSaberAngle(_saberA, out saberAAngle);
			RecordSaberAngle(_saberB, out saberBAngle);
		}

		private static void RecordSaberAngle(Saber saber, out float saberAngle) {
			Vector3 saberVector = saber.saberBladeTopPos - saber.saberBladeBottomPos;
			saberVector.z = 0;
			saberVector.Normalize();

			saberAngle = Mathf.Atan2(saberVector.y, saberVector.x) * Mathf.Rad2Deg;
		}
	}
}
