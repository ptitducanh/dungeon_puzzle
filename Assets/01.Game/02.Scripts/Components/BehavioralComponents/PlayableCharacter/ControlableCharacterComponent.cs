using System;
using Scripts.Entities;
using UnityEngine;

namespace Scripts.Components.BehavioralComponents
{
    [RequireComponent(typeof(MovementStatComponent))]
    public class ControlableCharacterComponent : BehavioralComponent
    {
        [SerializeField] private float    pushPower = 2.0f;
        [SerializeField] private Joystick joystick;

        private Animator              _animator;
        private CharacterController   _characterController;
        private MovementStatComponent _movementStatComponent;
        
        private bool  _isPushing;
        private float _pushDelay = 1f;
        private float _pushDelayTimer;
        private bool  _didStartTimer;
        private bool  _didStartPushing;

        #region lifecycle methods

        public override void OnAwake()
        {
            base.OnAwake();

            _characterController    = GetComponent<CharacterController>();
            _animator               = GetComponent<Animator>();
            _movementStatComponent  = Entity.GetDataComponent<MovementStatComponent>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();


            var joyStickDirection = joystick.Direction;
            var movingDirection   = new Vector3(joyStickDirection.x, 0, joyStickDirection.y);

            _characterController.Move(movingDirection * Time.deltaTime * _movementStatComponent.GetFloatStat("MaxSpeed"));
            _animator.SetFloat("speed", _characterController.velocity.magnitude);
            _characterController.Move(Vector3.down * 9.8f);
            
            if (movingDirection.magnitude > float.Epsilon)
            {
                _characterController.transform.LookAt(_characterController.transform.position + movingDirection);
            }

            _animator.SetBool("Pushing", _isPushing);
        }

        
        /// <summary>If the character touch a pushable object, we will push it</summary>
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!DidTouchPushable(hit.gameObject)) return;
            Rigidbody body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null || body.isKinematic)
            {
                return;
            }

            // We dont want to push objects below us
            if (hit.moveDirection.y < -0.3)
            {
                return;
            }

            if (!_didStartTimer)
            {
                _pushDelayTimer = _pushDelay;
                _didStartTimer = true;
            }
            
            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = hit.moveDirection;
            
            if (_pushDelayTimer > 0)
            {
                _pushDelayTimer -= Time.fixedDeltaTime;
                
                pushDir   = hit.moveDirection; // only push along X and Z
                pushDir.y = 0;
                if (Mathf.Abs(pushDir.x) > Mathf.Abs(pushDir.z))  pushDir.z = 0;
                else pushDir.x = 0;
                pushDir.Normalize();
                return;
            }
            

            // Apply the push
            body.velocity = pushDir * _movementStatComponent.GetFloatStat("PushSpeed");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!DidTouchPushable(other.gameObject)) return;
            _isPushing = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!DidTouchPushable(other.gameObject)) return;
            _isPushing       = false;
            _didStartTimer   = false;
            _didStartPushing = false;
        }
        
        private bool DidTouchPushable(GameObject other)
        {
            var otherEntity = EntityManager.Instance.GetEntityById(other.gameObject.GetInstanceID());
            if (otherEntity == null) return false;
            var pushableComponent = otherEntity.GetBehavioralComponent<PushableObject>();
            return pushableComponent != null;
        }

        #endregion

        public void Move(Vector3 direction)
        {
            _characterController.Move(direction);
        }

        
    }
}
