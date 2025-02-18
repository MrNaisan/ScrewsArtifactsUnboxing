using Services;
using Services.Impl;
using Zenject;

namespace Installers
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObjectRotationService>().AsSingle();
            Container.Resolve<IObjectRotationService>();
            
            Container.BindInterfacesAndSelfTo<UnscrewService>().AsSingle();
            Container.Resolve<IUnscrewService>();
            
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.Resolve<IInputService>();
        }
    }
}