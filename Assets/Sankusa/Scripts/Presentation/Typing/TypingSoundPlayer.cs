using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.BitTyping.Domain;
using Zenject;
using SankusaLib.SoundLib;
using UniRx;

namespace Sankusa.BitTyping.Presentation
{
    public class TypingSoundPlayer : MonoBehaviour
    {
        [Inject] private TypingCore typingCore;
        [SerializeField, SoundId] private string successSeId;
        [SerializeField, SoundId] private string failedSeId;
        [SerializeField, SoundId] private string advanceSeId;

        void Start()
        {
            typingCore
                .OnSuccess
                .Subscribe(_ => SoundManager.Instance.PlaySe(successSeId))
                .AddTo(this);

            typingCore
                .OnFailed
                .Subscribe(_ => SoundManager.Instance.PlaySe(failedSeId))
                .AddTo(this);

            typingCore
                .OnAdvanceText
                .Subscribe(_ => SoundManager.Instance.PlaySe(advanceSeId))
                .AddTo(this);
        }
    }
}