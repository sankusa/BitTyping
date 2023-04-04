using UnityEngine;
using Zenject;
using Sankusa.BitTyping.Domain;
using Sankusa.BitTyping.Presentation;

namespace Sankusa.BitTyping.Installer
{
    public class InGameSceneInstaller : MonoInstaller
    {
        [SerializeField] private ButtonInputer buttonInputer;
        [SerializeField] private LifeView lifeView;
        [SerializeField] private TimeView timeView;
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private TypingView charTypingView;
        [SerializeField] private TypingView binaryTypingView;
        [SerializeField] private TypingViewController typingViewController;
        [SerializeField] private TypingManView typingManView;
        [SerializeField] private StartPerformer startPerformer;
        [SerializeField] private ResultPanel resultPanel;

        public override void InstallBindings()
        {
            // Domain
            Container
                .BindInterfacesAndSelfTo<Score>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<Life>()
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
                .BindInterfacesAndSelfTo<LifeView>()
                .FromInstance(lifeView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<LifePresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<TimeView>()
                .FromInstance(timeView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<TimePresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<ScoreView>()
                .FromInstance(scoreView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ScorePresenter>()
                .AsSingle()
                .NonLazy();

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
                .BindInterfacesAndSelfTo<TypingViewController>()
                .FromInstance(typingViewController)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<TypingManView>()
                .FromInstance(typingManView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<TypingPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<KeyboardInputer>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<ButtonInputer>()
                .FromInstance(buttonInputer)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<StartPerformer>()
                .FromInstance(startPerformer)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ResultPanel>()
                .FromInstance(resultPanel)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<InGameLoop>()
                .AsSingle()
                .NonLazy();
        }
    }
}