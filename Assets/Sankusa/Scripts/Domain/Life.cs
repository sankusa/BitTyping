using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Assertions;

namespace Sankusa.BitTyping.Domain
{
    public class Life
    {
        private readonly ReactiveProperty<int> value = new ReactiveProperty<int>();
        public int Value
        {
            get => value.Value;
            set => this.value.Value = Mathf.Max(value, 0);
        }
        public IObservable<int> OnValueChanged => value;

        public void Initialize(int initialValue)
        {
            Assert.IsTrue(initialValue > 0);
            
            Value = initialValue;
        }

        public void Decrement()
        {
            Value--;
        }
    }
}