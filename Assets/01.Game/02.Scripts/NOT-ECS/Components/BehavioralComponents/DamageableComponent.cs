using Scripts.Components.DataComponents.Stats;
using Scripts.Entities;

namespace Scripts.Components.BehavioralComponents
{
    public class DamageableComponent : BehavioralComponent
    {
        private EntityStatsComponent _entityStatsComponent;
        
        public override void OnStart()
        {
            base.OnStart();
            
            _entityStatsComponent = Entity.GetDataComponent<EntityStatsComponent>();
        }

        public void TakeDamage(BaseEntity attacker, float damage)
        {
            if (_entityStatsComponent == null) return;
            
            _entityStatsComponent.UpdateFloatStat("HP", -damage);
        }
    }
}