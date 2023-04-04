using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sankusa.BitTyping.Domain
{
    [CreateAssetMenu(fileName = nameof(TypingTextData), menuName = nameof(TypingTextData))]
    public class TypingTextData : ScriptableObject
    {
        [SerializeField, TextArea] private string typingText;
        public string TypingText => typingText;
    }
}