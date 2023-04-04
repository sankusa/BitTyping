using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Doozy.Runtime.UIManager.Containers;
using SankusaLib.SoundLib;

namespace Sankusa.BitTyping.Presentation
{
    public class StartPerformer : MonoBehaviour
    {
        [SerializeField] private UIView uiView;
        [SerializeField, SoundId] private string startSeId;

        public async UniTask Play(CancellationToken token)
        {
            uiView.Show();
            SoundManager.Instance.PlaySe(startSeId);

            await UniTask.WaitUntil(() => uiView.isHidden);
        } 
    }
}