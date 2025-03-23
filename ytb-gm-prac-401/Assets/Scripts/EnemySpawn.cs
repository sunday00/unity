using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public float maxEnemySpawnDelay;
    public float curEnemySpawnDelay;

    public GameObject[] EnemySpwanPoints;
    private string[] enemies;

    private Manager manager;
    private Player player;

    private void Awake()
    {
        enemies = new[] { "enemyL", "enemyM", "enemyS" };

        manager = GetComponent<Manager>();
        player = manager.player;
    }

    private void Update()
    {
        curEnemySpawnDelay += Time.deltaTime;
        if (curEnemySpawnDelay >= maxEnemySpawnDelay) SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        curEnemySpawnDelay = 0;
        maxEnemySpawnDelay = Random.Range(0.5f, 3f);
        var enemyIndex = Random.Range(0, enemies.Length);
        var enemyPoint = EnemySpwanPoints[Random.Range(0, EnemySpwanPoints.Length)];

        // var enemyObj = Instantiate(Enemies[enemyIndex], enemyPoint.transform.position, enemyPoint.transform.rotation);
        var enemyObj = manager.objectManager.MakeObject(enemies[enemyIndex]);

        if (enemyObj.IsUnityNull()) return;
        enemyObj.transform.position = enemyPoint.transform.position;
        enemyObj.transform.rotation = enemyPoint.transform.rotation;

        enemyObj.GetComponent<Enemy>().manager = manager;
        enemyObj.GetComponent<Enemy>().SetVelocity(enemyPoint.name);
        enemyObj.GetComponent<Enemy>().GetComponent<EnemyFiring>().player = player;
        enemyObj.GetComponent<Enemy>().GetComponent<EnemyFiring>().objectManager = manager.objectManager;
    }
}