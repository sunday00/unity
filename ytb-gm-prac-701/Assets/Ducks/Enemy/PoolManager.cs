using System.Collections.Generic;
using UnityEngine;

namespace Ducks.Enemy
{
    public class PoolManager : MonoBehaviour
    {
        public GameObject[] prefabs;
        private List<GameObject>[] pools;

        private void Awake()
        {
            pools = new List<GameObject>[prefabs.Length];

            for (var i = 0; i < pools.Length; i++) pools[i] = new List<GameObject>();
        }
    }
}