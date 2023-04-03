using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemyiesAlive = 0;

    [SerializeField]
    private Wave[] waves;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves = 2f;

    
    public Text waveCountdownText;

    private float countdown = 2f;

    private int waveIndex = 0;

    public GameManager gameManager;

     void Update()
    {
        if (EnemyiesAlive>0)
        {
            return;
        }
        
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);

    }

    IEnumerator SpawnWave()
    {
        
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemyiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/ wave.rate);
        }
        
        waveIndex++;
        
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position, spawnPoint.rotation);       
    }
}
