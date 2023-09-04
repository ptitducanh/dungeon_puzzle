using System;
using UnityEngine;

namespace Scripts.Others
{
    public class GameConfiguration : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}