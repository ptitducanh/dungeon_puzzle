using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Data;
using Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;

using MoreMountains.NiceVibrations;

namespace Scripts.Components.BehavioralComponents
{
    /// <summary>
    /// This component is responsible for adding item to the player item container
    /// </summary>
    public class ItemProviderComponent : BehavioralComponent
    {
        [SerializeField] private Image     fillImage;
        [SerializeField] private Transform sourceFoodTransform;
        
        private ItemProviderDataComponent itemData;
        private Coroutine _addFoodToContainerCoroutine;
        
        public override void OnAwake()
        {
            base.OnAwake();
            
            itemData = Entity.GetDataComponent<ItemProviderDataComponent>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // check if the object is the main character
            var mainCharacterEntity = EntityManager.Instance.GetEntityById(other.gameObject.GetInstanceID());
            if (mainCharacterEntity is not PlayerCharacterEntity) return;

            // get the item container component
            var foodContainer = mainCharacterEntity.GetDataComponent<ItemContainerComponent>();
            if (foodContainer == null) return;
            
            // start adding item to the container
            if (_addFoodToContainerCoroutine != null)
            {
                StopCoroutine(_addFoodToContainerCoroutine);
            }
            _addFoodToContainerCoroutine = StartCoroutine(IEAddItemToContainer(foodContainer));
        }

        private void OnTriggerExit(Collider other)
        {
            var mainCharacterEntity = EntityManager.Instance.GetEntityById(other.gameObject.GetInstanceID());
            if (mainCharacterEntity == null) return;
            
            if (_addFoodToContainerCoroutine != null)
            {
                // stop the coroutine and reset the fill image
                StopCoroutine(_addFoodToContainerCoroutine);
                fillImage.fillAmount = 0;
            }
        }
        
        /// <summary>
        /// Keep adding item to the container until the player exit the trigger
        /// </summary>
        /// <param name="itemContainer"></param>
        /// <returns></returns>
        private IEnumerator IEAddItemToContainer(ItemContainerComponent itemContainer)
        {
            float remainingTime = itemData.PreparationTime;
            while (true)
            {
                remainingTime = itemData.PreparationTime;
                while (remainingTime > 0)
                {
                    remainingTime -= Time.deltaTime;
                    fillImage.fillAmount = 1f - remainingTime / itemData.PreparationTime;
                    yield return null;
                }
                itemContainer.AddItem(itemData.itemType, sourceFoodTransform.position);
                MMVibrationManager.Haptic(HapticTypes.Success);
                SoundController.Instance.PlaySFX("FoodCollect");
            }
        }
    }
}
