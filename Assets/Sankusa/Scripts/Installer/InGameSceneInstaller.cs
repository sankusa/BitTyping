using UnityEngine;
using Zenject;
using Sankusa.BitTyping.Domain;
using Sankusa.BitTyping.Presentation;

namespace Sankusa.BitTyping.Installer
{
    public class InGameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Domain
            Container
                .BindInterfacesAndSelfTo<Score>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameTimer>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<TypingCore>()
                .AsSingle()
                .NonLazy();

            // Presentation
            Container
                .BindInterfacesAndSelfTo<InGameLoop>()
                .AsSingle()
                .NonLazy();
        }
    }
}