using System.Collections;
using Ducks.Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ducks
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public bool isLive;
        public float gameTime;
        public float maxGameTime = 2 * 10f;

        public PoolManager pool;
        public Player.Player player;
        public LevelUp uiLevelUp;

        public Result uiResult;
        public GameObject enemyCleaner;

        [Header("---level and score---")] //
        public int playerId;

        public int level;

        public int[] nextExp;

        public int kill;
        public int exp;

        public float health;
        public float maxHealth = 100f;

        private void Awake()
        {
            Instance = this;
            nextExp = new[] { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
        }

        private void Update()
        {
            if (!isLive) return;

            gameTime += Time.deltaTime;

            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
                // next
                GameVictory();
            }
        }

        public void GameStart(int id)
        {
            playerId = id;

            health = maxHealth;

            player.gameObject.SetActive(true);

            uiLevelUp.Select(playerId % 2); // Temporary

            Resume();
        }

        public void GameReStart()
        {
            SceneManager.LoadScene("Main");
        }

        public void GameOver()
        {
            StartCoroutine(GameOverRoutine());
        }

        private IEnumerator GameOverRoutine()
        {
            isLive = false;
            yield return new WaitForSeconds(0.5f);

            // uiResult.SetActive(true);
            uiResult.gameObject.SetActive(true);
            uiResult.Lose();
            Stop();
        }

        public void GameVictory()
        {
            StartCoroutine(GameVictoryRoutine());
        }

        private IEnumerator GameVictoryRoutine()
        {
            isLive = false;

            enemyCleaner.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            // uiResult.SetActive(true);
            uiResult.gameObject.SetActive(true);
            uiResult.Win();
            Stop();
        }

        public void GetExp()
        {
            if (!isLive) return;

            exp++;

            // if (exp == nextExp[level])
            if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
            {
                level++;
                exp = 0;
                uiLevelUp.Show();
            }
        }

        public void Stop()
        {
            isLive = false;
            Time.timeScale = 0;
        }

        public void Resume()
        {
            isLive = true;
            Time.timeScale = 1;
        }
    }
}