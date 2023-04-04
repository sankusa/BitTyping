using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.BitTyping.Domain;
using UniRx;
using Zenject;

namespace Sankusa.BitTyping.Presentation
{
    public class ScorePresenter
    {
        private readonly Score score;
        private readonly ScoreView scoreView;
        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        [Inject]
        public ScorePresenter(Score score, ScoreView scoreView)
        {
            this.score = score;
            this.scoreView = scoreView;

            score
                .OnValueChanged
                .Subscribe(x =>
                {
                    int byteCount = x / 8;
                    int bitCount = x % 8;
                    scoreView.Draw((byteCount > 0 ? byteCount + " Byte " : "") + bitCount + " Bit");
                })
                .AddTo(compositeDisposable);
        }

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }
    }
}