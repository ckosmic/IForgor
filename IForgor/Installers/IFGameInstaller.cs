using IForgor.Recorders;
using IForgor.UI;
using Zenject;

namespace IForgor.Installers
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
