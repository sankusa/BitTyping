using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Runtime.UIManager.Containers;

namespace Sankusa.BitTyping.Presentation
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] UIView uiView;
        [SerializeField] TMP_Text timeText;

        public void Draw(string timeText)
        {
            this.timeText.text = timeText;
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