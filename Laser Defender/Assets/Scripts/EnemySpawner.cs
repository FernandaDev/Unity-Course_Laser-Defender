using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } 
        while (looping);
    }

    IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig _currentWave)
    {
        for (int i = 0; i < _currentWave.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(_currentWave.GetEnemyPrefab(), _currentWave.GetWaypoints()[0].position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(_currentWave);

            yield return new WaitForSeconds(_currentWave.GetTimeBetweenSpawns());
        }
    }
}
