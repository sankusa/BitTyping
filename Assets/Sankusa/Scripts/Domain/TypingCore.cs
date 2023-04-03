using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Text;

namespace Sankusa.BitTyping.Domain
{
    public class TypingCore
    {
        private const string encodeCharacterCode = "shift-jis";

        private string text;
        private readonly List<bool> binaryText = new List<bool>();
        private readonly Subject<string> onAddText = new Subject<string>();
        public IObservable<string> OnAddText => onAddText;
        private readonly Subject<IEnumerable<bool>> onAddBinaryText = new Subject<IEnumerable<bool>>();
        public IObservable<IEnumerable<bool>> OnAddBinaryText => onAddBinaryText;
        private readonly Subject<Unit> onSuccess = new Subject<Unit>();
        public IObservable<Unit> OnSuccess => onSuccess;
        private readonly Subject<Unit> onFailed = new Subject<Unit>();
        public IObservable<Unit> OnFailed => onFailed;
        private readonly Subject<Unit> onAdvanceText = new Subject<Unit>();
        public IObservable<Unit> OnAdvanceText => onAdvanceText;
        private readonly Subject<Unit> onAdvanceBinaryText = new Subject<Unit>();
        public IObservable<Unit> OnAdvanceBinaryText => onAdvanceBinaryText;

        private int successBitCount = 0;

        public void AddText(string additionalText)
        {
            text += additionalText;
            AddBinaryText(additionalText);

            onAddText.OnNext(additionalText);
        }

        private void AddBinaryText(string additionalText)
        {
            byte[] dataArray = Encoding.GetEncoding(encodeCharacterCode).GetBytes(additionalText);
            List<bool> bits = new List<bool>();
            foreach(byte data in dataArray)
            {
                for(int i = 0; i < 8; i++)
                {
                    bits.Add((data & (128 >> i)) != 0);
                }
            }
            binaryText.AddRange(bits);
            onAddBinaryText.OnNext(bits);
        }

        public void Input(bool input)
        {
            if(binaryText.Count == 0) return;
            
            if(binaryText[0] == input)
            {
                AdvanceBinaryText();
                if(successBitCount == 16)
                {
                    AdvanceText();
                    successBitCount = 0;
                }
                onSuccess.OnNext(Unit.Default);
            }
            else
            {
                onFailed.OnNext(Unit.Default);
            }
        }

        private void AdvanceBinaryText()
        {
            binaryText.RemoveAt(0);
            successBitCount++;
            onAdvanceBinaryText.OnNext(Unit.Default);
        }

        private void AdvanceText()
        {
            text.Remove(0, 1);
            onAdvanceText.OnNext(Unit.Default);
        }
    }
}