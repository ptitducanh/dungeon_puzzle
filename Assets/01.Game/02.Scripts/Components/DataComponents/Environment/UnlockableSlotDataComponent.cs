using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Components.DataComponents
{
    public class UnlockableSlotDataComponent : DataComponent
    {
        public            bool IsUnlocked;
        public            int  RequiredCoin;
        public            int  UnlockDuration;
        [ReadOnly] public int  CoinPerSecond;
        [ReadOnly] public int  RemainingCoin;


        public override void OnAwake()
        {
            base.OnAwake();

            RemainingCoin = RequiredCoin;
            CoinPerSecond = RequiredCoin / UnlockDuration;
        }
    }
}