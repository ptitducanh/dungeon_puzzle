using Sirenix.Utilities;
using UnityEngine;

namespace Scripts.Components
{
    public class RandomizedOutfitComponent : BehavioralComponent
    {
        [SerializeField] private GameObject[] headParts;
        [SerializeField] private GameObject[] bodyParts;
        [SerializeField] private GameObject[] legParts;
        
        public override void OnAwake()
        {
            base.OnAwake();

            RandomizeOutfit();
        }

        private void RandomizeOutfit()
        {
            headParts.ForEach(part => part.SetActive(false));
            bodyParts.ForEach(part => part.SetActive(false));
            legParts.ForEach(part => part.SetActive(false));
            
            var headIndex = Random.Range(0, headParts.Length);
            var bodyIndex = Random.Range(0, bodyParts.Length);
            var legIndex = Random.Range(0, legParts.Length);

            headParts[headIndex].SetActive(true);
            bodyParts[bodyIndex].SetActive(true);
            legParts[legIndex].SetActive(true);
        }
    }
}
