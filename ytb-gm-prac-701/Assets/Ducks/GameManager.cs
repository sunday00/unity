using Ducks.Enemy;
using UnityEngine;

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

        [Header("---level and score---")] //
        public int level;

        public int[] nextExp;

        public int kill;
        public int exp;

        public int health;
        public int maxHealth = 100;

        private void Awake()
        {
            Instance = this;
            nextExp = new[] { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
        }

        private void Start()
        {
            health = maxHealth;

            // Temporary
            uiLevelUp.Select(0);
        }

        private void Update()
        {
            if (!isLive) return;

            gameTime += Time.deltaTime;

            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
                // next
                print("not implemented");
            }
        }

        public void GetExp()
        {
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