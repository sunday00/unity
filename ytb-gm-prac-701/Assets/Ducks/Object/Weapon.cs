using UnityEngine;

namespace Ducks.Object
{
    public class Weapon : MonoBehaviour
    {
        public int id;
        public int prefabId;
        public float damage;
        public int count;
        public float speed;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
        }

        public void Init()
        {
            switch (id)
            {
                case 0:
                    Action0();
                    break;
                default:
                    Action1();
                    break;
            }
        }

        private void Action0()
        {
            speed = -150;
            Batch();
        }

        private void Action1()
        {
        }

        private void Batch()
        {
            for (var i = 0; i < count; i++)
            {
                var bullet = GameManager.Instance.pool.Get(prefabId).transform;
                bullet.parent = transform;

                var bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.Init(damage, -1); // -1 means Infinity per
            }
        }
    }
}