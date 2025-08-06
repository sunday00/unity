using Unity.VisualScripting;
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
        private Player.Player player;

        private float timer;

        private void Awake()
        {
            // player = GetComponentInParent<Player.Player>();
            player = GameManager.Instance.player;
        }

        // private void Start()
        // {
        //     Init();
        // }

        private void Update()
        {
            switch (id)
            {
                case 0:
                    Repeat0();
                    break;
                default:
                    Repeat1();
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Space)) LevelUp(20, 5);
        }

        private void Repeat0()
        {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
        }

        private void Repeat1()
        {
            timer += Time.deltaTime;
            if (timer >= speed)
            {
                timer = 0;
                Fire();
            }
        }

        private void Fire()
        {
            if (player.scanner.nearestTarget.IsUnityNull()) return;

            var target = player.scanner.nearestTarget.position;
            var dir = (target - transform.position).normalized;

            var bullet = GameManager.Instance.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Init(damage, count, dir); // -1 means Infinity per
        }

        public void Init(ItemData data)
        {
            name = "Weapon " + data.itemId;
            transform.parent = player.transform;
            transform.localPosition = Vector3.zero;

            id = data.itemId;
            damage = data.baseDamage;
            count = data.baseCount;

            for (var index = 0; index < GameManager.Instance.pool.prefabs.Length; index++)
                if (data.projectile == GameManager.Instance.pool.prefabs[index])
                {
                    prefabId = index;
                    break;
                }

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
            speed = 0.3f;
        }

        private void Batch()
        {
            for (var i = 0; i < count; i++)
            {
                var bullet = i < transform.childCount
                    ? transform.GetChild(i)
                    : GameManager.Instance.pool.Get(prefabId).transform;
                bullet.parent = transform;

                bullet.localPosition = Vector3.zero;
                bullet.localRotation = Quaternion.identity;

                var rotVec = Vector3.forward * 360 * i / count;
                bullet.Rotate(rotVec);
                bullet.Translate(bullet.up * 1.5f, Space.World);

                var bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.Init(damage, -1, Vector3.zero); // -1 means Infinity per
            }
        }

        public void LevelUp(float dam, int cnt)
        {
            damage = dam;
            count += cnt;

            if (id == 0) Batch();
        }
    }
}