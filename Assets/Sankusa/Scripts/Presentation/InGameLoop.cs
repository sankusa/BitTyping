using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.BitTyping.Domain;
using System;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

namespace Sankusa.BitTyping.Presentation
{
    public class InGameLoop : IInitializable, IDisposable
    {
        private readonly GameTimer gameTimer;
        private readonly Score score;
        private readonly TypingCore typingCore;
        private readonly KeyboardInputer keyboardInputer;
        private readonly CancellationTokenSource source = new CancellationTokenSource();
        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        [Inject]
        public InGameLoop(
            GameTimer gameTimer,
            Score score,
            TypingCore typingCore,
            KeyboardInputer keyboardInputer
        )
        {
            this.gameTimer = gameTimer;
            this.score = score;
            this.typingCore = typingCore;
            this.keyboardInputer = keyboardInputer;
        } 

        public void Initialize()
        {
            StartAsync(source.Token).Forget();
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            // 初期設定
            gameTimer.Initialize(600f);

            // スタート
            gameTimer.Start();
            keyboardInputer.Start();

            typingCore.AddText("あなたは速水もこみちになります。おあｆびあぶふぇｌぶえBfuibafibiblえうｓｆｈべうあ");

            while(true)
            {
                await UniTask.Yield(token);

                // 時間チェック
                if(gameTimer.IsTimeUp) break;
            }

            // 終了
            gameTimer.Stop();
            keyboardInputer.Stop();

            Debug.Log(gameTimer.ElapsedTime);

            compositeDisposable.Clear();

            await UniTask.CompletedTask;
        }

        public void Dispose()
        {
            source.Cancel();
            compositeDisposable.Dispose();
        }
    }
}