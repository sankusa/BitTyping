using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Sankusa.BitTyping.Presentation
{
    public class LifeView : MonoBehaviour
    {
        [SerializeField] TMP_Text lifeText;

        public void Draw(int lifeValue)
        {
            lifeText.text = $"あと{lifeValue}回失敗で終了";
        }
    }
}