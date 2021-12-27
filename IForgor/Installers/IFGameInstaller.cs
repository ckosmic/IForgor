using Zenject;

namespace IForgor
{
	internal class IFGameInstaller : Installer<IFGameInstaller>
	{
		public override void InstallBindings() {
			Container.Bind<SaberRecorder>().AsSingle();
			Container.BindInterfacesAndSelfTo<NoteRecorder>().AsSingle();
			Container.BindInterfacesTo<PauseUIManager>().AsSingle();
			Container.Bind<UIGroup>().AsTransient();
		}
	}
}
