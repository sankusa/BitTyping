using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Containers;

namespace Sankusa.BitTyping.Presentation
{
    public class TypingViewController : MonoBehaviour
    {
        [SerializeField] private UIView uiView;

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