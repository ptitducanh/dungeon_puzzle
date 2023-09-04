using System;
using Scripts.Entities;
using UnityEngine;

namespace Scripts.Components.BehavioralComponents
{
    public class RecycleNPCComponent : BehavioralComponent
    {
        private void OnTriggerEnter(Collider other)
        {
            var entity = EntityManager.Instance.GetEntityById(other.gameObject.GetInstanceID());
            if (entity is NPCEntity)
            {
                entity.gameObject.SetActive(false);
            }
        }
    }
}