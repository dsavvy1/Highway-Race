using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<waveConfig> waveConfigs;

    int startingWave = 0;
    [SerializeField] bool looping = false;
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }   while (looping);
    }

    private IEnumerator SpawnAllEnemiesInWave(waveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            GameObject enemyPrefab = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].position,
                Quaternion.Euler(0, 0, 180f)
                );

            enemyPrefab.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }


    }
    IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
