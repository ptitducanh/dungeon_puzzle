using System.Collections;
using System.Linq;
using Scripts.Entities;
using TMPro;
using UnityEngine;

namespace Scripts.Components.BehavioralComponents
{
    public class HUDComponent : BehavioralComponent
    {
        [SerializeField] private Joystick   joystick;
        [SerializeField] private GameObject tutorial;
        [SerializeField] private TMP_Text[] coinTexts;

#if DEBUG_BUILD
        public TMP_Text debugText;
#endif

        private InventoryDataComponent _inventoryDataComponent;
        private int                    _currentCoinAmount;

        public override void OnStart()
        {
            base.OnStart();

            var characterEntites = EntityManager.Instance.GetEntitiesByType<PlayerCharacterEntity>();
            var characterEntity  = characterEntites.FirstOrDefault();
            if (characterEntity == null) return;

            _inventoryDataComponent = characterEntity.GetDataComponent<InventoryDataComponent>();

            StartCoroutine(WaitForJoystick());
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
#if DEBUG_BUILD
            debugText.text = $"{Mathf.RoundToInt(1 / Time.deltaTime)}";
#endif
            
            // if (_inventoryDataComponent.GetIntStat("Coin") == _currentCoinAmount) return;
            // _currentCoinAmount = _inventoryDataComponent.GetIntStat("Coin");
            // var coinAmountText = _currentCoinAmount.ToString();
            // foreach (var text in coinTexts)
            // {
            //     text.text = coinAmountText;
            // }

        }
        
        IEnumerator WaitForJoystick()
        {
            yield return new WaitUntil(() => joystick.Direction.magnitude > float.Epsilon);
            
            tutorial.SetActive(false);
        }
    }
}