using Scripts.Components.DataComponents.Stats;
using UnityEngine;

namespace Scripts.Entities
{
    public class InventoryDataComponent : EntityStatsComponent
    {
        public override void OnAwake()
        {
            base.OnAwake();
            
            AddIntStat("Coin", 0);
        }
    }
}