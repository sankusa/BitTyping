using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Sankusa.BitTyping.Presentation
{
    public class CharUnit : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private Tweener moveTweener;

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void Move(Vector2 targetPos, float duration)
        {
            if(moveTweener != null && moveTweener.IsActive() && moveTweener.IsPlaying())
            {
                moveTweener.Kill();
            }

            transform.DOMove(targetPos, duration).SetLink(gameObject);
        }

        public void SetColor(Color color)
        {
            text.color = color;
        }

        public void SetAlpha(float alpha)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
    }
}