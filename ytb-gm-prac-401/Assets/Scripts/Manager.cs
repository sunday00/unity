using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] EnemySpwanPoints;

    public Player player;

    public int life;
    public int score;

    public float maxEnemySpawnDelay;
    public float curEnemySpawnDelay;

    public Text scoreText;
    public Image[] lifes;
    public GameObject gameOverPanel;

    private void Update()
    {
        curEnemySpawnDelay += Time.deltaTime;
        if (curEnemySpawnDelay >= maxEnemySpawnDelay) SpawnEnemy();

        scoreText.text = string.Format("{0:n0}", score);
    }

    private void SpawnEnemy()
    {
        curEnemySpawnDelay = 0;
        maxEnemySpawnDelay = Random.Range(0.5f, 3f);
        var enemyIndex = Random.Range(0, Enemies.Length);
        var enemyPoint = EnemySpwanPoints[Random.Range(0, EnemySpwanPoints.Length)];

        var enemyObj = Instantiate(Enemies[enemyIndex], enemyPoint.transform.position, enemyPoint.transform.rotation);
        enemyObj.GetComponent<Enemy>().manager = this;
        enemyObj.GetComponent<Enemy>().SetVelocity(enemyPoint.name);
        enemyObj.GetComponent<Enemy>().GetComponent<EnemyFiring>().player = player;
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnAct", 1.5f);
    }

    private void RespawnAct()
    {
        player.GetComponent<PlayerHit>().isHit = false;
        player.transform.position = Vector3.down * 3;
        player.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}