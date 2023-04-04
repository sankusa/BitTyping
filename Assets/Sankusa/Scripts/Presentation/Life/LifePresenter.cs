using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using Sankusa.BitTyping.Domain;
using System;

namespace Sankusa.BitTyping.Presentation
{
    public class LifePresenter : IInitializable, IDisposable
    {
        private readonly Life life;
        private readonly LifeView lifeView;
        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        [Inject]
        public LifePresenter(Life life, LifeView lifeView)
        {
            this.life = life;
            this.lifeView = lifeView;
        }

        public void Initialize()
        {
            life
                .OnValueChanged
                .Subscribe(x =>
                {
                    lifeView.Draw(x);
                })
                .AddTo(compositeDisposable);
        }

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }
    }
}