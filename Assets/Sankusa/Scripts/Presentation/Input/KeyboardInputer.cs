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
    public class KeyboardInputer : ITickable,  IDisposable
    {
        private readonly TypingCore typingCore;
        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        private bool active;

        [Inject]
        public KeyboardInputer(TypingCore typingCore)
        {
            this.typingCore = typingCore;
        }

        public void SetActive(bool active)
        {
            this.active = active;
        }

        public void Tick()
        {
            if(!active) return;
            
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