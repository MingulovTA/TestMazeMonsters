using TestMazeMonsters.Core.CustomInput;
using TestMazeMonsters.Core.User;
using TestMazeMonsters.UI.Popups;
using TestMazeMonsters.UI.Popups.Core;
using UnityEngine;
using Zenject;

namespace TestMazeMonsters.Core.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private PopupService _popupServicePrefab;
        public override void InstallBindings ()
        {
            Container.BindInterfacesAndSelfTo<InputController>()
                .AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<UserConfig>()
                .AsSingle()
                .NonLazy();
            Container.Bind<IPopupService>()
                .To<PopupService>()
                .FromComponentInNewPrefab(_popupServicePrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}