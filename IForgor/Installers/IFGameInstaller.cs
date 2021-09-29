using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
