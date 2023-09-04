using System;
using System.Collections;
using Scripts.Components.DataComponents;
using Scripts.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Components.BehavioralComponents
{
    public class UnlockableSlotComponent : BehavioralComponent
    {
        [SerializeField] private TMP_Text   remainingCoinTxt;
        [SerializeField] private Image      progressImage;
        [SerializeField] private GameObject objectToUnlock;

        private UnlockableSlotDataComponent _unlockableSlotDataComponent;
        private InventoryDataComponent      _inventoryDataComponent;
        private Coroutine                   _unlockCoroutine;


        public override void OnAwake()
        {
            base.OnAwake();

            _unlockableSlotDataComponent = Entity.GetDataComponent<UnlockableSlotDataComponent>();
        }

        public override void OnStart()
        {
            base.OnStart();
            
            remainingCoinTxt.text = _unlockableSlotDataComponent.RemainingCoin.ToString();
        }

        /// <summary>
        /// When player stand on the slot
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            // if the slot is already unlocked, return
            if (_unlockableSlotDataComponent.IsUnlocked) return;

            // if not now we'll try to get the inveo
            if (_inventoryDataComponent == null)
            {
                var entity = EntityManager.Instance.GetEntityById(other.gameObject.GetInstanceID());
                if (entity is PlayerCharacterEntity playerEntity)
                {
                    _inventoryDataComponent = playerEntity.GetDataComponent<InventoryDataComponent>();
                }
            }

            if (_inventoryDataComponent == null) return;


            if (_unlockCoroutine != null)
            {
                StopCoroutine(_unlockCoroutine);
                _unlockCoroutine = null;
            }

            _unlockCoroutine = StartCoroutine(IEUnlockSlot());
        }

        /// <summary>
        /// When player walks out of the slot
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            var entity = EntityManager.Instance.GetEntityById(other.gameObject.GetInstanceID());
            if (entity is PlayerCharacterEntity playerEntity)
            {
                if (_unlockCoroutine != null)
                {
                    StopCoroutine(_unlockCoroutine);
                    _unlockCoroutine = null;
                }
            }
        }

        private IEnumerator IEUnlockSlot()
        {
            var wait1Sec = new WaitForSeconds(1);
            while (!_unlockableSlotDataComponent.IsUnlocked)
            {
                // Wait for 1 second
                yield return wait1Sec;
                
                // check if player has enough coin
                var playerCoin = _inventoryDataComponent.GetIntStat("Coin");
                if (playerCoin <= 0) yield break;

                // calculate how much coin to take in this second
                // then take it from player
                var coinToTake = Mathf.Min(playerCoin, _unlockableSlotDataComponent.CoinPerSecond);
                _inventoryDataComponent.UpdateIntStat("Coin", -coinToTake);
                _unlockableSlotDataComponent.RemainingCoin -= coinToTake;
                SoundController.Instance.PlaySFX("UseCoinToUnlock");
                
                remainingCoinTxt.text = _unlockableSlotDataComponent.RemainingCoin.ToString();
                progressImage.rectTransform.localScale = new Vector3(1, 1f - (float) _unlockableSlotDataComponent.RemainingCoin / _unlockableSlotDataComponent.RequiredCoin, 1);
                
                if (_unlockableSlotDataComponent.RemainingCoin <= 0)
                {
                    UnlockSlot();
                    break;
                }
            }
        }
        
        private void UnlockSlot()
        {
            SoundController.Instance.PlaySFX("NewSlot");
            _unlockableSlotDataComponent.IsUnlocked = true;
            objectToUnlock.SetActive(true);
            Destroy(Entity.gameObject);
        }
    }
}