using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Sankusa.BitTyping.Domain;
using System;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using SankusaLib.SceneManagementLib;


namespace Sankusa.BitTyping.Presentation
{
    public class InGameLoop : IInitializable, IDisposable
    {
        private readonly SceneArgStore sceneArgStore;
        private readonly GameTimer gameTimer;
        private readonly Life life;
        private readonly Score score;
        private readonly TypingCore typingCore;
        private readonly KeyboardInputer keyboardInputer;
        private readonly ButtonInputer buttonInputer;
        private readonly TimeView timeView;
        private readonly ScoreView scoreView;
        private readonly TypingViewController typingViewController;
        private readonly TypingManView typingManView;
        private readonly StartPerformer startPerformer;
        private readonly ResultPanel resultPanel;
        private readonly CancellationTokenSource source = new CancellationTokenSource();
        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        [Inject]
        public InGameLoop(
            SceneArgStore sceneArgStore,
            GameTimer gameTimer,
            Life life,
            Score score,
            TypingCore typingCore,
            KeyboardInputer keyboardInputer,
            ButtonInputer buttonInputer,
            TimeView timeView,
            ScoreView scoreView,
            TypingViewController typingViewController,
            TypingManView typingManView,
            StartPerformer startPerformer,
            ResultPanel resultPanel
        )
        {
            this.sceneArgStore = sceneArgStore;
            this.gameTimer = gameTimer;
            this.life = life;
            this.score = score;
            this.typingCore = typingCore;
            this.keyboardInputer = keyboardInputer;
            this.buttonInputer = buttonInputer;
            this.timeView = timeView;
            this.scoreView= scoreView;
            this.typingViewController = typingViewController;
            this.typingManView = typingManView;
            this.startPerformer = startPerformer;
            this.resultPanel = resultPanel;
        } 

        public void Initialize()
        {
            StartAsync(source.Token).Forget();
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            // シーン引数取得
            InGameArg inGameArg = sceneArgStore.Pop<InGameArg>();

            // 初期設定
            gameTimer.Initialize(inGameArg.Time);
            life.Initialize(inGameArg.Life);

            FinishReason finishReason = FinishReason.None;

            // UIViewのStart時処理を待ちたいので1フレーム待つ
            await UniTask.Yield(token);

            // 導入アニメーション
            buttonInputer.Show();

            await UniTask.Delay(300, cancellationToken: token);
            timeView.Show();

            await UniTask.Delay(300, cancellationToken: token);
            typingViewController.Show();

            await UniTask.Delay(300, cancellationToken: token);
            scoreView.Show();

            await UniTask.Delay(2000, cancellationToken: token);

            await startPerformer.Play(token);

            // スタート
            gameTimer.Start();
            keyboardInputer.SetActive(true);
            buttonInputer.SetActive(true);
            typingManView.StartLinkTypingInput();

            typingCore.AddText("あなたは速水もこみちになります。おあｆびあぶふぇｌぶえBfuibafibiblえうｓｆｈべうあ");

            typingCore.OnSuccess.Subscribe(_ => score.Increment()).AddTo(compositeDisposable);
            typingCore.OnFailed.Subscribe(_ => life.Decrement()).AddTo(compositeDisposable);

            while(true)
            {
                await UniTask.Yield(token);

                if(Keyboard.current.rKey.wasPressedThisFrame)
                {
                    Retry(inGameArg);
                }

                // ライフチェック
                if(life.Value == 0)
                {
                    finishReason = FinishReason.LifeIsNothing;
                    break;
                }
                // 時間チェック
                if(gameTimer.IsTimeUp)
                {
                    finishReason = FinishReason.TimeUp;
                    break;
                }
            }

            // 終了
            gameTimer.Stop();
            keyboardInputer.SetActive(false);
            buttonInputer.SetActive(false);
            typingManView.StopLinkTypingInput();

            // 終了アニメーション
            typingManView.MoveEyeAsync(token).Forget();
            buttonInputer.Hide();

            await UniTask.Delay(300, cancellationToken: token);
            scoreView.Hide();

            await UniTask.Delay(300, cancellationToken: token);
            typingViewController.Hide();

            await UniTask.Delay(300, cancellationToken: token);
            timeView.Hide();

            await UniTask.Delay(2000, cancellationToken: token);

            typingManView.PlayFadeOut();

            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score.Value);

            resultPanel.Show();

            compositeDisposable.Clear();

            await UniTask.CompletedTask;
        }

        public void Dispose()
        {
            source.Cancel();
            compositeDisposable.Dispose();
        }

        private void Retry(InGameArg inGameArg)
        {
            sceneArgStore.Set(inGameArg);
            SceneManager.LoadScene(GameConst.SCENE_NAME_INGAME);
        }
    }
}