using System;
using Scripts.Entities;
using UnityEngine;
using DG.Tweening;

namespace Scripts.Components.BehavioralComponents
{
    
    public class ButtonComponent : BehavioralComponent
    {
        [SerializeField] private BaseEntity doorEntity;
        [SerializeField] private GameObject button;
        
        private Vector3 _originalPosition;
        private Vector3 _pressedPosition = new Vector3(0, 0.3f, 0); // this can be exposed to the inspector
        
        UnlockableDoorComponent _doorComponent;

        public override void OnStart()
        {
            base.OnStart();
            
            _originalPosition = button.transform.localPosition;
            _doorComponent    = doorEntity.GetBehavioralComponent<UnlockableDoorComponent>();
        }


        private void PushButton(bool isPushed)
        {
            button.transform.DOLocalMove(isPushed ? _pressedPosition : _originalPosition, 0.5f);
            if (isPushed)
            {
                _doorComponent.Unlock();
            }
            else
            {
                _doorComponent.Lock();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var otherEntity = EntityManager.Instance.GetEntityById(other.gameObject.GetInstanceID());
            if (otherEntity == null) return;
            var triggerComponent = otherEntity.GetBehavioralComponent<TriggerButtonComponent>();
            Debug.Log($"Trigger Enter {otherEntity.name}");
            if (triggerComponent == null) return;
            
            PushButton(true);
        }
        
        private void OnTriggerExit(Collider other)
        {
            var otherEntity = EntityManager.Instance.GetEntityById(other.gameObject.GetInstanceID());
            if (otherEntity == null) return;
            var triggerComponent = otherEntity.GetBehavioralComponent<TriggerButtonComponent>();
            
            if (triggerComponent == null) return;
            
            PushButton(false);
        }
    }
}