using Ducks.Enemy;
using UnityEngine;

namespace Ducks
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public float gameTime;
        public float maxGameTime = 2 * 10f;

        public PoolManager pool;
        public Player.Player player;

        [Header("---level and score---")] //
        public int level;

        public int[] nextExp;

        public int kill;
        public int exp;

        private void Awake()
        {
            Instance = this;
            nextExp = new[] { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
        }

        private void Update()
        {
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

            if (exp == nextExp[level])
            {
                level++;
                exp = 0;
            }
        }
    }
}