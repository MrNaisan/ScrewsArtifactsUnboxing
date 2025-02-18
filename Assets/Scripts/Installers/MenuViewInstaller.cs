using UnityEngine;
using Views;
using Zenject;

namespace Installers
{
    public class MenuViewInstaller : MonoInstaller
    {
        [SerializeField] private MenuView menuView;
        
        public override void InstallBindings()
        {
            Container.Inject(menuView);
        }
    }
}