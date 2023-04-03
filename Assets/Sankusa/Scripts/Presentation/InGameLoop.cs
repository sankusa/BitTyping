using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.BitTyping.Domain;
using System;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Sankusa.BitTyping.Presentation
{
    public class InGameLoop : IInitializable, IDisposable
    {
        private readonly GameTimer gameTimer;
        private readonly Score score;
        private readonly TypingCore typingCore;
        private readonly CancellationTokenSource source = new CancellationTokenSource();

        [Inject]
        public InGameLoop(
            GameTimer gameTimer,
            Score score,
            TypingCore typingCore
        )
        {
            this.gameTimer = gameTimer;
            this.score = score;
            this.typingCore = typingCore;
        } 

        public void Initialize()
        {
            StartAsync(source.Token).Forget();
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            // 初期設定
            gameTimer.Initialize(10f);

            // スタート
            gameTimer.Start();

            while(true)
            {
                await UniTask.Yield(token);

                // 時間チェック
                if(gameTimer.IsTimeUp) break;
            }

            // 終了
            gameTimer.Stop();

            Debug.Log(gameTimer.ElapsedTime);

            await UniTask.CompletedTask;
        }

        public void Dispose()
        {
            source.Cancel();
        }
    }
}