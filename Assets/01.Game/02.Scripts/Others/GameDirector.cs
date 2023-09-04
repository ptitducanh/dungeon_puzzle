using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Common;
using Scripts.Entities;
using Scripts.Others;
using UnityEngine;

public class GameDirector : Singleton<GameDirector>
{
    [SerializeField] private float     npcSpawnInterval;
    [SerializeField] private Transform SpawnPoint;

    [SerializeField] private List<GameObject[]> unlockableWaves;

    private float _remainingSpawnTime;
    private int   _currentWaveIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        _remainingSpawnTime = npcSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        _remainingSpawnTime -= Time.deltaTime;
        if (_remainingSpawnTime <= 0)
        {
            if (HasEmptyChair())
            {
                _remainingSpawnTime = npcSpawnInterval;
                SpawnNPC();
            }

            _remainingSpawnTime = npcSpawnInterval;
        }

        if (_currentWaveIndex < unlockableWaves.Count)
        {
            var currentWave = unlockableWaves[_currentWaveIndex];
            if (currentWave.All(slot => EntityManager.Instance.GetEntityById(slot.GetInstanceID()) == null))
            {
                _currentWaveIndex++;
                var nextWave = unlockableWaves[_currentWaveIndex];
                foreach (var slot in nextWave)
                {
                    slot.gameObject.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// Get a npc from the object pool and spawn it at the spawn point
    /// </summary>
    private void SpawnNPC()
    {
        var newNPC = ObjectPool.Instance.Get("NPCMale");
        newNPC.transform.position = SpawnPoint.position;
    }

    private bool HasEmptyChair()
    {
        var allChairs = EntityManager.Instance.GetEntitiesByType<ChairEntity>();
        if (allChairs == null || allChairs.Length == 0)
        {
            return false;
        }

        // is there any chair that is not occupied?
        return allChairs.Any(chair => !chair.GetDataComponent<ChairStatComponent>().IsOccupied);
    }
}