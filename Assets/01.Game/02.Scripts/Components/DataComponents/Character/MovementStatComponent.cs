using Scripts.Components.DataComponents.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Components
{
    public class MovementStatComponent : EntityStatsComponent
    {
        [OnValueChanged("UpdateMaxSpeed")]
        [SerializeField] private float maxSpeed;

        [OnValueChanged("UpdatePushSpeed")]
        [SerializeField] private float PushSpeed;

        public override void OnAwake()
        {
            base.OnAwake();
            
            AddFloatStat("MaxSpeed", maxSpeed);
            AddFloatStat("PushSpeed", PushSpeed);
        }
        
        private void UpdateMaxSpeed()
        {
            SetFloatStat("MaxSpeed", maxSpeed);
        }
        
        private void UpdatePushSpeed()
        {
            SetFloatStat("PushSpeed", PushSpeed);
        }
    }
}
