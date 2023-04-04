using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Sankusa.BitTyping.Domain;
using TMPro;
using System.Linq;

namespace Sankusa.BitTyping.Presentation
{
    public class TypingView : MonoBehaviour
    {
        [SerializeField] private Transform charUnitParent;
        [SerializeField] private CharUnit charUnitPrefab;
        [SerializeField] private int displayIndexMin;
        [SerializeField] private int displayIndexMax;
        [SerializeField] private Transform charCenterPositionMarker;
        [SerializeField] private float charSpace = 30f;
        [SerializeField] private float charUnitSlideDuration = 0.2f;
        [SerializeField] private Vector2 featuredCharUnitScale;
        
        private List<char> charList = new List<char>();
        private List<CharUnitControlData> charUnitList = new List<CharUnitControlData>();
        private int CharTextListDisplayIndexMax => charUnitList.Count > 0 ? charUnitList.Select(x => x.DisplayIndex).Max() : -1;

        // 待機文字列に追加し、空きがあれば可能な限り生成
        public void AddText(string text)
        {
            charList.AddRange(text);
            AddCharUnitsFromCharList();
        }

        // 全体を1つ左にずらし、待機文字列があれば生成
        public void AdvanceCharUnitlist()
        {
            charUnitList.ForEach(x => x.DisplayIndex--);
            MoveAllCharUnitToCorrectPosition();
            // 不要なものは破棄
            for(int i = charUnitList.Count - 1; i >= 0; i--)
            {
                if(charUnitList[i].DisplayIndex < displayIndexMin)
                {
                    CharUnitControlData data = charUnitList[i];
                    charUnitList.Remove(data);
                    Destroy(data.CharUnit.gameObject);
                }
            }
            // 生成
            AddCharUnitsFromCharList();

            UpdateTextColor();
        }

        // できる限り待機文字列から文字を取り出してCharUnit生成
        private void AddCharUnitsFromCharList()
        {
            while(CharTextListDisplayIndexMax < displayIndexMax && charList.Count > 0)
            {
                AddCharUnitFromCharList();
            }
        }

        // 待機文字列から最古の文字を取り出してCharUnit生成
        private void AddCharUnitFromCharList()
        {
            if(charList.Count == 0) return;
            if(CharTextListDisplayIndexMax >= displayIndexMax) return;

            char listHead = charList[0];
            charList.RemoveAt(0);

            CharUnit charUnit = Instantiate(charUnitPrefab, new Vector2(2000, 0), Quaternion.identity, charUnitParent);
            charUnit.SetText(listHead.ToString());

            CharUnitControlData data = new CharUnitControlData(charUnit, CharTextListDisplayIndexMax + 1);
            charUnitList.Add(data);

            MoveCharUnitToCorrectPosition(data);
        }

        // 全CharUnitを既定の位置に動かす
        private void MoveAllCharUnitToCorrectPosition()
        {
            charUnitList.ForEach(x => MoveCharUnitToCorrectPosition(x));
        }

        // 全CharUnitを既定の位置に動かす
        private void MoveCharUnitToCorrectPosition(CharUnitControlData data)
        {
            data.CharUnit.Move(GetCharPosition(data.DisplayIndex), charUnitSlideDuration);
            data.CharUnit.Scale(data.DisplayIndex == 0 ? featuredCharUnitScale : Vector2.one, charUnitSlideDuration);
        }

        public Vector2 GetCharPosition(int displayIndex)
        {
            return (Vector2)charCenterPositionMarker.position + new Vector2(displayIndex * charSpace, 0);
        }

        private void UpdateTextColor()
        {
            charUnitList.ForEach(x => x.CharUnit.SetAlpha(x.DisplayIndex >= 0 ? 1 : 0.5f));
        }
    }
}