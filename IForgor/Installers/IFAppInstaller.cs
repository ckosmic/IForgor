using Zenject;

namespace IForgor
{
    public class IFAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<AssetLoader>().AsSingle().Lazy();
        }
    }
}