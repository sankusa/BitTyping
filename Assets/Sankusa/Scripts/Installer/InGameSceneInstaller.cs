using UnityEngine;
using Zenject;
using Sankusa.BitTyping.Domain;
using Sankusa.BitTyping.Presentation;

namespace Sankusa.BitTyping.Installer
{
    public class InGameSceneInstaller : MonoInstaller
    {
        [SerializeField] private TypingView charTypingView;
        [SerializeField] private TypingView binaryTypingView;

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
                .Bind<TypingView>()
                .WithId(TypingViewInjectId.Char)
                .FromInstance(charTypingView)
                .AsCached();

            Container
                .Bind<TypingView>()
                .WithId(TypingViewInjectId.Binary)
                .FromInstance(binaryTypingView)
                .AsCached();

            Container
                .BindInterfacesAndSelfTo<TypingPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<KeyboardInputer>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<InGameLoop>()
                .AsSingle()
                .NonLazy();
        }
    }
}