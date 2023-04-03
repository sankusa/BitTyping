using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Sankusa.BitTyping.Domain
{
    public class Score
    {
        private readonly ReactiveProperty<int> value = new ReactiveProperty<int>();
        public int Value => value.Value;
        public IObservable<int> OnValueChanged => value;

        public void Increment()
        {
            value.Value++;
        }
    }
}