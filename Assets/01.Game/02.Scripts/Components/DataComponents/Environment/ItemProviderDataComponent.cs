using Scripts.Components;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Entities
{
    public class ItemProviderDataComponent : DataComponent
    {
        public ItemType itemType;
        public float    PreparationTime;
    }
}