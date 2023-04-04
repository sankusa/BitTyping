using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.BitTyping.Domain;
using UniRx;
using Doozy.Runtime.UIManager.Components;
using Zenject;
using SankusaLib;
using Doozy.Runtime.UIManager.Containers;

namespace Sankusa.BitTyping.Presentation
{
    public class ButtonInputer : MonoBehaviour
    {
        [SerializeField] private UIView uiView;
        [SerializeField] private UIButton button0;
        [SerializeField] private UIButton button1;
        [Inject] private readonly TypingCore typingCore;
        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        private bool active = false;

        void Start()
        {
            button0.AddListenerToPointerDown(() =>
            {
                if(active) typingCore.Input(false);
            });

            button1.AddListenerToPointerDown(() =>
            {
                if(active) typingCore.Input(true);
            });
        }

        public void SetActive(bool active)
        {
            this.active = active;
        }

        public void Show()
        {
            uiView.Show();
        }

        public void Hide()
        {
            uiView.Hide();
        }
    }
}