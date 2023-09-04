using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIControlledComponent : BehavioralComponent
    {
        private NavMeshAgent       _agent;
        private Animator           _animator;
        private NPCDataComponent   _dataComponent;
        private ChairStatComponent _chair;

        #region life cycle

        public override void OnAwake()
        {
            base.OnAwake();

            _agent         = GetComponent<NavMeshAgent>();
            _animator      = GetComponent<Animator>();
            _dataComponent = Entity.GetDataComponent<NPCDataComponent>();

            _dataComponent.State = NPCState.Initial;
        }

        public override void OnStart()
        {
            base.OnStart();

            _dataComponent.State = NPCState.MovingIn;
            MoveToAnEmptyChair();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            UpdateNPCState();
        }

        #endregion

        #region private functions

        private void UpdateNPCState()
        {
            // update the speed parameter of the animator
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
            _animator.SetBool("Eating", _dataComponent.State == NPCState.UsingItem);

            // check if the agent has reached the destination
            if (_dataComponent.State == NPCState.MovingIn && (transform.position - _agent.destination).magnitude < float.Epsilon && _agent.velocity.magnitude < float.Epsilon)
            {
                _dataComponent.State               = NPCState.WaitingForItem;       // update the state
                _dataComponent.RemainingEatingTime = _dataComponent.EatingDuration; // set the remaining eating time

                var tablePosition = _chair.Table.position;
                transform.LookAt(new Vector3(tablePosition.x, transform.position.y, tablePosition.z)); // look at the table
            }

            if (_dataComponent.State == NPCState.UsingItem)
            {
                // if NPC is eating, decrease the remaining eating time
                if (_dataComponent.RemainingEatingTime > 0)
                {
                    _dataComponent.RemainingEatingTime -= Time.deltaTime;
                }
                else
                {
                    // otherwise, set the state to finished eating
                    _dataComponent.State = NPCState.FinishedUsing;

                    // TODO: leave some money on the table.
                    _dataComponent.IsDoneEating  = true;
                    _dataComponent.DidPayForFood = false;

                    // move to the exit
                    _chair.IsOccupied = false;
                    _chair.Occupant   = null;
                    MoveToExit();
                }
            }
        }

        /// <summary>
        /// Move the NPC to an empty chair
        /// </summary>
        private void MoveToAnEmptyChair()
        {
            // find an empty chair
            var chair = FindEmptyChair();
            if (chair == null) return;


            _chair             = chair.GetDataComponent<ChairStatComponent>(); //Cache the chair
            _chair.IsOccupied  = true;                                         // Set the chair to occupied
            _chair.Occupant    = Entity;                                       // Set the chair's occupant to this NPC
            _agent.destination = _chair.transform.position;                    // move the agent to the chair
        }

        private void MoveToExit()
        {
            _dataComponent.State = NPCState.MovingOut;
            var exitEntity = EntityManager.Instance.GetEntitiesByType<ExitEntity>().FirstOrDefault();
            if (exitEntity != null)
            {
                _agent.destination = exitEntity.transform.position;
            }
        }

        /// <summary>
        /// Find an empty chair in the scene
        /// </summary>
        /// <returns></returns>
        private ChairEntity FindEmptyChair()
        {
            var allChairs = EntityManager.Instance.GetEntitiesByType<ChairEntity>();
            if (allChairs == null || allChairs.Length == 0)
            {
                return null;
            }

            foreach (var chair in allChairs)
            {
                var chairStatComponent = chair.GetDataComponent<ChairStatComponent>();

                if (!chairStatComponent.IsOccupied) return chair;
            }

            return null;
        }

        #endregion

        #region public functions

        [Button("Start eating")]
        public void Eat()
        {
            _dataComponent.State = NPCState.UsingItem;
        }

        #endregion
    }
}