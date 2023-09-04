using System;
using System.Collections.Generic;
using Scripts.Common;
using UnityEngine;
using Scripts.Components;
using Sirenix.OdinInspector;

namespace Scripts.Entities
{
    /// <summary>
    /// An entity can be any object in the game world. And each entity has a unique id.
    /// An entity can be a player, a monster, a npc, a item, a door, a chest, etc.
    /// </summary>
    public class BaseEntity : SerializedMonoBehaviour
    {
        [SerializeField] [ReadOnly] public int Id { get; protected set; }

        [SerializeField] private Dictionary<Type, object> _dataComponents       = new();
        [SerializeField] private Dictionary<Type, object> _behavioralComponents = new();

        #region Life Cycle

        private void Awake()
        {
            Id = gameObject.GetInstanceID();             // generate an unique id for the entity.
            EntityManager.Instance.RegisterEntity(this); // register the entity to the entity manager.

            GetAllComponents();
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            foreach (var component in _dataComponents.Values)
            {
                if (component is DataComponent dataComponent)
                {
                    dataComponent.OnAwake();
                }
            }

            foreach (var component in _behavioralComponents.Values)
            {
                if (component is BehavioralComponent behavioralComponent)
                {
                    behavioralComponent.OnAwake();
                }
            }
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {
            foreach (var component in _dataComponents.Values)
            {
                if (component is DataComponent dataComponent)
                {
                    dataComponent.OnStart();
                }
            }

            foreach (var component in _behavioralComponents.Values)
            {
                if (component is BehavioralComponent behavioralComponent)
                {
                    behavioralComponent.OnStart();
                }
            }
        }

        private void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            foreach (var component in _dataComponents.Values)
            {
                if (component is DataComponent dataComponent)
                {
                    dataComponent.OnUpdate();
                }
            }

            foreach (var component in _behavioralComponents.Values)
            {
                if (component is BehavioralComponent behavioralComponent)
                {
                    behavioralComponent.OnUpdate();
                }
            }
        }

        private void OnDestroy()
        {
            EntityManager.Instance.UnregisterEntity(this);
        }

        #endregion

        #region public methods

        public T GetDataComponent<T>() where T : DataComponent
        {
            if (_dataComponents.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }

            return null;
        }

        public T GetBehavioralComponent<T>() where T : BehavioralComponent
        {
            if (_behavioralComponents.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }

            return null;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Get all the components of the entity. Then put them into the corresponding dictionary.
        /// </summary>
        private void GetAllComponents()
        {
            var allComponents = GetComponents<BaseComponent>();
            foreach (var component in allComponents)
            {
                component.Entity = this;

                if (component is DataComponent dataComponent)
                {
                    _dataComponents.Add(dataComponent.GetType(), dataComponent);
                }

                if (component is BehavioralComponent behavioralComponent)
                {
                    _behavioralComponents.Add(behavioralComponent.GetType(), behavioralComponent);
                }
            }
        }

        #endregion
    }
}