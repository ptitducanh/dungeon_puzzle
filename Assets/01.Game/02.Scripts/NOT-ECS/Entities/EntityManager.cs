using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Entities
{
    public class EntityManager : Singleton<EntityManager>
    {
        [SerializeField] private Dictionary<int, BaseEntity> _entities = new();

        private Dictionary<Type, List<object>> _entitiesByType = new();

        public void RegisterEntity(BaseEntity entity)
        {
            if (_entities == null) _entities = new Dictionary<int, BaseEntity>();
            
            _entities.Add(entity.Id, entity);

            //Add entity to _entitiesByType
            if (!_entitiesByType.ContainsKey(entity.GetType()))
            {
                _entitiesByType.Add(entity.GetType(), new List<object>());
            }

            _entitiesByType[entity.GetType()].Add(entity);
        }
        
        public void UnregisterEntity(BaseEntity entity)
        {
            //Remove entity from _entitiesByType
            if (_entitiesByType.ContainsKey(entity.GetType()))
            {
                _entitiesByType[entity.GetType()].Remove(entity);
            }

            //remove from _entities
            _entities.Remove(entity.Id);
        }

        public BaseEntity GetEntityById(int id)
        {
            if (!_entities.ContainsKey(id))
            {
                return null;
            }

            return _entities[id];
        }

        public T[] GetEntitiesByType<T>() where T : BaseEntity
        {
            if (!_entitiesByType.ContainsKey(typeof(T)))
            {
                return null;
            }

            return _entitiesByType[typeof(T)].Select(entity => entity as T).ToArray();
        }
    }
}