using IForgor.UI;
using Zenject;

namespace IForgor.Installers
{
    public class IFAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<AssetLoader>().AsSingle().Lazy();
        }
    }
}