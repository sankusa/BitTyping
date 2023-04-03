using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using System;
using Zenject;
using Sankusa.BitTyping.Domain;

namespace Sankusa.BitTyping.Presentation
{
    public class KeyboardInputer : IDisposable
    {
        private readonly TypingCore typingCore;
        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        [Inject]
        public KeyboardInputer(TypingCore typingCore)
        {
            this.typingCore = typingCore;
        }

        public void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    Update();
                })
                .AddTo(compositeDisposable);
        }

        public void Stop()
        {
            compositeDisposable.Clear();
        }

        private void Update()
        {
            if(Keyboard.current.digit0Key.wasPressedThisFrame)
            {
                typingCore.Input(false);
            }
            else if(Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                typingCore.Input(true);
            }
        }

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }
    }
}