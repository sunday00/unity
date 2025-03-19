using UnityEngine;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] EnemySpwanPoints;

    public float maxEnemySpawnDelay;
    public float curEnemySpawnDelay;

    private void Update()
    {
        curEnemySpawnDelay += Time.deltaTime;
        if (curEnemySpawnDelay >= maxEnemySpawnDelay) spawnEmemy();
    }

    private void spawnEmemy()
    {
        curEnemySpawnDelay = 0;
        maxEnemySpawnDelay = Random.Range(0.5f, 3f);
        var enemyIndex = Random.Range(0, Enemies.Length);
        var enemyPoint = EnemySpwanPoints[Random.Range(0, EnemySpwanPoints.Length)];

        Instantiate(Enemies[enemyIndex], enemyPoint.transform.position, enemyPoint.transform.rotation);
    }
}