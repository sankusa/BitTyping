using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using Sankusa.BitTyping.Domain;
using UniRx;
using System.Linq;

namespace Sankusa.BitTyping.Presentation
{
    public class TypingPresenter : IDisposable
    {
        private CompositeDisposable compositeDisposable = new CompositeDisposable();

        [Inject(Id = TypingViewInjectId.Char)] private TypingView charTypingView;
        [Inject(Id = TypingViewInjectId.Binary)] private TypingView binaryTypingView;

        [Inject]
        public TypingPresenter(TypingCore typingCore)
        {
            typingCore
                .OnAddText
                .Subscribe(x =>
                {
                    charTypingView.AddText(x);
                })
                .AddTo(compositeDisposable);

            typingCore
                .OnAdvanceText
                .Subscribe(_ =>
                {
                    charTypingView.AdvanceCharUnitlist();
                })
                .AddTo(compositeDisposable);
                
            typingCore
                .OnAddBinaryText
                .Subscribe(x =>
                {
                    binaryTypingView.AddText(new string(x.Select(y => y ? '1' : '0').ToArray()));
                })
                .AddTo(compositeDisposable);

            typingCore
                .OnAdvanceBinaryText
                .Subscribe(_ =>
                {
                    binaryTypingView.AdvanceCharUnitlist();
                })
                .AddTo(compositeDisposable);
        }

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }
    }
}