using Scripts.Others;
using UnityEngine;
using DG.Tweening;

namespace Scripts.Components.BehavioralComponents
{
    public class NPCCoinGeneratorComponent : BehavioralComponent
    {
        [SerializeField] private Transform[] coinSpawnPoints;
        
        private NPCDataComponent _npcDataComponent;

        public override void OnAwake()
        {
            base.OnAwake();

            _npcDataComponent = Entity.GetDataComponent<NPCDataComponent>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            if (_npcDataComponent.IsDoneEating && !_npcDataComponent.DidPayForFood)
            {
                _npcDataComponent.DidPayForFood = true;
                var spawnPoint = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Length)];
                var coinObject = ObjectPool.Instance.Get("Coin");
                coinObject.transform.DOJump(spawnPoint.position, 1, 1, .5f).SetEase(Ease.OutCubic);
            }
        }
    }
}