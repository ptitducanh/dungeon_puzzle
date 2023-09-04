using UnityEngine;

namespace Scripts.Components.BehavioralComponents
{
    public class UnlockableDoorComponent : BehavioralComponent
    {
        [SerializeField] private GameObject door;
        
        public void Unlock()
        {
            door.SetActive(false);
        }
        
        public void Lock()
        {
            door.SetActive(true);
        }
    }
}