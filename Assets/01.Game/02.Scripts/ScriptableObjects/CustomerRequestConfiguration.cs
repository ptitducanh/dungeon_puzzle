using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Data
{
    [CreateAssetMenu(fileName = "RequestSO", menuName = "Data/Request", order = 0)]
    public class CustomerRequestConfiguration : ScriptableObject
    {  
        public ItemRequest[] Requests;
        
        public ItemType GetRandomFoodType()
        {
            var randomIndex = UnityEngine.Random.Range(0, Requests.Length);
            return Requests[randomIndex].itemType;
        }
    }

    [Serializable]
    public class ItemRequest
    {
        [FormerlySerializedAs("FoodType")] public ItemType itemType;
        public  int      Amount;
        public  float    Factor;
    }

    public enum ItemType
    {
        None,
        Pistol,
        Rifle
    }
}