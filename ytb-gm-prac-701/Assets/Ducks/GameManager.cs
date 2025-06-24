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

        private void Awake()
        {
            Instance = this;
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
    }
}