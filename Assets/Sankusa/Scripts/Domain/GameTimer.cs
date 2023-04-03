using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Assertions;

namespace Sankusa.BitTyping.Domain
{
    public class GameTimer : IDisposable
    {
        private readonly ReactiveProperty<float> elapsedTime = new ReactiveProperty<float>(0);
        public float ElapsedTime => elapsedTime.Value;
        public IObservable<float> OnElapsedTimeChanged => elapsedTime;

        private readonly ReactiveProperty<float> timeLimit = new ReactiveProperty<float>(0);
        public float TimeLimit => timeLimit.Value;
        public IObservable<float> OnTimeLimitChanged => timeLimit;

        public float RemainingTime => timeLimit.Value - elapsedTime.Value;
        public IObservable<float> OnRemainingTimeChanged =>
            Observable
                .Merge(elapsedTime, timeLimit)
                .Select(_ => timeLimit.Value - elapsedTime.Value);

        public bool IsTimeUp => timeLimit.Value != 0 && elapsedTime.Value >= timeLimit.Value;

        private IObservable<Unit> OnTimeUp =>
            OnRemainingTimeChanged
                .Where(_ => IsTimeUp)
                .AsUnitObservable();

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        public void Initialize(float timeLimit)
        {
            this.timeLimit.Value = timeLimit;
            SetElapsedTime(0);
        }

        public void Start()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => Update())
                .AddTo(disposable);
        }

        private void Update()
        {
            SetElapsedTime(ElapsedTime + Time.deltaTime);
        }

        public void Stop()
        {
            disposable.Clear();
        }

        private void SetElapsedTime(float elapsedTime)
        {
            if(timeLimit.Value == 0)
            {
                this.elapsedTime.Value = Mathf.Max(elapsedTime, 0);
            }
            else
            {
                this.elapsedTime.Value = Mathf.Clamp(elapsedTime, 0, timeLimit.Value);
            }
        }

        public void ResetElapsedTime()
        {
            SetElapsedTime(0);
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}