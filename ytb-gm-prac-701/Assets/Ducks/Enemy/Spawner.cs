using UnityEngine;

namespace Ducks.Enemy
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] spawnPoints;

        public int level;
        private float _timer;

        private void Awake()
        {
            spawnPoints = GetComponentsInChildren<Transform>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            level = Mathf.FloorToInt(GameManager.Instance.gameTime / 10f);

            if (_timer > 1f / (level + 1))
            {
                _timer = 0;
                Spawn();
            }
        }

        private void Spawn()
        {
            var enemy = GameManager.Instance.pool.Get(level);

            enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        }
    }
}