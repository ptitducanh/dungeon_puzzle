using System;
using System.Collections.Generic;
using Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Components
{
    public class ItemContainerComponent : DataComponent
    {
        [SerializeField] [ReadOnly] 
        public List<ItemType> Foods { get; private set; } = new();

        public Action<ItemType, Vector3> OnItemAdded;
        public Action<ItemType> OnItemRemoved;
            
        public void AddItem(ItemType item, Vector3 sourcePosition)
        {
            Foods.Add(item);
            OnItemAdded?.Invoke(item, sourcePosition);
        }

        public ItemType GetItem(ItemType item)
        {
            foreach (var foodItem in Foods)
            {
                if (foodItem == item)
                {
                    Foods.Remove(foodItem);
                    OnItemRemoved?.Invoke(foodItem);
                    return foodItem;
                }
            }

            return ItemType.None;
        }
    }
}