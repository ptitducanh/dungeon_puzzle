using Scripts.Entities;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Components
{
    public abstract class BaseComponent : SerializedMonoBehaviour
    {
        [HideInInspector] public BaseEntity Entity;


        #region Life Cycle
        // These methods are called by Base Entity.
        public virtual void OnAwake() {}
        
        public virtual void OnStart() {}
        
        public virtual void OnUpdate() {}
        #endregion
    }
}