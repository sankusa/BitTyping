using UnityEngine;
using Zenject;
using SankusaLib.SceneManagementLib;

namespace Sankusa.BitTyping.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SceneArgStore>()
                .AsSingle()
                .NonLazy();
        }
    }
}