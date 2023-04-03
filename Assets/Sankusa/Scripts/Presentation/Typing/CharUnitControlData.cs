using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sankusa.BitTyping.Presentation
{
    public class CharUnitControlData
    {
        private int displayIndex;
        public int DisplayIndex
        {
            get => displayIndex;
            set => displayIndex = value;
        }
        private CharUnit charUnit;
        public CharUnit CharUnit
        {
            get => charUnit;
            set => charUnit = value;
        }
        public CharUnitControlData(CharUnit charUnit, int displayIndex = 0)
        {
            this.charUnit = charUnit;
            this.displayIndex = displayIndex;
        }
    }
}