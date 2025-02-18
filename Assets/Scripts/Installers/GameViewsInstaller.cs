using UnityEngine;
using Views;
using Zenject;

namespace Installers
{
    public class GameViewsInstaller : MonoInstaller
    {
        [SerializeField] private InputView inputView;
        [SerializeField] private ObjectView objectView;
        [SerializeField] private CurrentBoltView currentBoltView;
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private PrizeView prizeView;
        [SerializeField] private RestartButton restartButton;
        [SerializeField] private WinView winView;
        
        public override void InstallBindings()
        {
            Container.Bind<InputView>().FromInstance(inputView).AsSingle();
            Container.Bind<ObjectView>().FromInstance(objectView).AsSingle();
            Container.Bind<CurrentBoltView>().FromInstance(currentBoltView).AsSingle();
            Container.Bind<InventoryView>().FromInstance(inventoryView).AsSingle();
            Container.Bind<PrizeView>().FromInstance(prizeView).AsSingle();
            Container.Bind<WinView>().FromInstance(winView).AsSingle();
            Container.Inject(restartButton);
            Container.Inject(Container.Resolve<WinView>());
        }
    }
}