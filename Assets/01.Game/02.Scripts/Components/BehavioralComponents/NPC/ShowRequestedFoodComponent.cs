using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Components.BehavioralComponents
{
    public class ShowRequestedFoodComponent : BehavioralComponent
    {
        private NPCDataComponent _npcDataComponent;

        [SerializeField] private Image[] foodIcon;

        public override void OnAwake()
        {
            base.OnAwake();
            _npcDataComponent = GetComponent<NPCDataComponent>();
        }

        public override void OnStart()
        {
            base.OnStart();

            for (int i = 0; i < foodIcon.Length; i++)
            {
                foodIcon[i].gameObject.SetActive(false);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();


            // only show the food icon if the NPC is waiting for food
            if (_npcDataComponent.State == NPCState.WaitingForItem)
            {
                int foodTypeIndex = (int)_npcDataComponent.requestedItemType;
                foodTypeIndex--; //Because the first food type is None

                for (int i = 0; i < foodIcon.Length; i++)
                {
                    foodIcon[i].gameObject.SetActive(i == foodTypeIndex);
                }
            }
            else
            {
                for (int i = 0; i < foodIcon.Length; i++)
                {
                    foodIcon[i].gameObject.SetActive(false);
                }
            }
        }
    }
}