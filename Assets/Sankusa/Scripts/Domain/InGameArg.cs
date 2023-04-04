using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib.SceneManagementLib;

namespace Sankusa.BitTyping.Domain
{
    public class InGameArg : ISceneArg
    {
        private float time;
        public float Time => time;
        private int life;
        public int Life => life;

        public InGameArg(float time, int life)
        {
            this.time = time;
            this.life = life;
        }
    }
}