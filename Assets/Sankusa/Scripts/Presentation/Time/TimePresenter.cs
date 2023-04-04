using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using Sankusa.BitTyping.Domain;
using System;

namespace Sankusa.BitTyping.Presentation
{
    public class TimePresenter : IDisposable
    {
        private readonly GameTimer gameTimer;
        private readonly TimeView timeView;
        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        [Inject]
        public TimePresenter(GameTimer gameTimer, TimeView timeView)
        {
            this.gameTimer = gameTimer;
            this.timeView = timeView;

            gameTimer
                .OnRemainingTimeChanged
                .Subscribe(x => timeView.Draw(x.ToString("0.0")))
                .AddTo(compositeDisposable);
        }

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }
    }
}