using UnityEngine;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] EnemySpwanPoints;

    public Player Player;

    public float maxEnemySpawnDelay;
    public float curEnemySpawnDelay;

    private void Update()
    {
        curEnemySpawnDelay += Time.deltaTime;
        if (curEnemySpawnDelay >= maxEnemySpawnDelay) SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        curEnemySpawnDelay = 0;
        maxEnemySpawnDelay = Random.Range(0.5f, 3f);
        var enemyIndex = Random.Range(0, Enemies.Length);
        var enemyPoint = EnemySpwanPoints[Random.Range(0, EnemySpwanPoints.Length)];

        var enemyObj = Instantiate(Enemies[enemyIndex], enemyPoint.transform.position, enemyPoint.transform.rotation);
        enemyObj.GetComponent<Enemy>().SetVelocity(enemyPoint.name);
        enemyObj.GetComponent<Enemy>().GetComponent<EnemyFiring>().player = Player;
    }
}