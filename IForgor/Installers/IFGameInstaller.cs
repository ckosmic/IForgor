using Zenject;

namespace IForgor
{
	internal class IFGameInstaller : Installer<IFGameInstaller>
	{
		public override void InstallBindings() {
			Container.BindInterfacesAndSelfTo<SaberRecorder>().AsSingle();
			Container.BindInterfacesAndSelfTo<NoteRecorder>().AsSingle();
			Container.BindInterfacesAndSelfTo<PauseUIManager>().AsSingle();
		}
	}
}
