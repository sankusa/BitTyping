using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib.SingletonLib;

namespace Sankusa.BitTyping.Domain
{
    [CreateAssetMenu(fileName = nameof(TypingTextMaster), menuName = nameof(TypingTextMaster))]
    public class TypingTextMaster : SingletonScriptableObject<TypingTextMaster>
    {
        [SerializeField] private List<TypingTextData> textDataList;

        public string GetRandom()
        {
            return textDataList.GetRandom(Random.value).TypingText;
        }
    }
}