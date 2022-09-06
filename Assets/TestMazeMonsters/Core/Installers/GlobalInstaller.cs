using TestMazeMonsters.Core.Input;
using TestMazeMonsters.Core.User;
using Zenject;

namespace TestMazeMonsters.Core.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings ()
        {
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UserConfig>().AsSingle().NonLazy();
        }
    }
}