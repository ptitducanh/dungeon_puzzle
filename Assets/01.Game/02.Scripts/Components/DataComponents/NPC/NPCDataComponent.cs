using System.Collections;
using System.Collections.Generic;
using Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Components
{
    public enum NPCState
    {
        Initial,
        MovingIn,
        WaitingForItem,
        UsingItem,
        FinishedUsing,
        MovingOut
    }

    public class NPCDataComponent : DataComponent
    {
        [ReadOnly] public NPCState State;
        [ReadOnly] public float    RemainingEatingTime;
        [ReadOnly] public ItemType requestedItemType;
        [ReadOnly] public int      CoinAmount;

        public float                        EatingDuration;
        public CustomerRequestConfiguration customerRequestConfiguration;
        public bool                         IsDoneEating;
        public bool                         DidPayForFood;

        public override void OnAwake()
        {
            base.OnAwake();

            requestedItemType = customerRequestConfiguration.GetRandomFoodType();
        }
    }
}