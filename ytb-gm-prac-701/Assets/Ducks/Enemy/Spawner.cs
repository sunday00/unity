using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ducks.Enemy
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public SpawnData[] spawnData;

        public int level;
        private float _timer;

        private void Awake()
        {
            spawnPoints = GetComponentsInChildren<Transform>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            level =
                Mathf.Min(
                    Mathf.FloorToInt(GameManager.Instance.gameTime / 10f),
                    spawnData.Length - 1
                );

            if (_timer > spawnData[level].spawnTime)
            {
                _timer = 0;
                Spawn();
            }
        }

        private void Spawn()
        {
            var enemy = GameManager.Instance.pool.Get(0);

            enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnData[level]);
        }
    }

    [Serializable]
    public class SpawnData
    {
        public int health;
        public float spawnTime;
        public float speed;
        public int spriteType;
    }
}