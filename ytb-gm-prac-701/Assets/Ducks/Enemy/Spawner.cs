using UnityEngine;

namespace Ducks.Enemy
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] spawnPoints;

        private float _timer;

        private void Awake()
        {
            spawnPoints = GetComponentsInChildren<Transform>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > 0.2f)
            {
                _timer = 0;
                Spawn();
            }
        }

        private void Spawn()
        {
            var enemy = GameManager.Instance.pool.Get(Random.Range(0, 2));

            enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        }
    }
}