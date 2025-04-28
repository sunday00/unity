using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Ducks.Interactival.Enemies
{
    public class BossMove : MonoBehaviour, IEnemyMove
    {
        public Transform target;
        public BoxCollider meleeArea;

        public bool isAttacking;

        public GameObject bulletBox;
        public GameObject missile;
        public Transform missilePortA;
        public Transform missilePortB;
        public bool isLook;

        private bool _isChase;

        private EnemyManager _manager;
        private NavMeshAgent _nav;
        private Rigidbody _rb;

        private Vector3 lookVec;
        private Vector3 tauntVec;

        private void Awake()
        {
            _manager = GetComponent<EnemyManager>();
            _nav = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();

            _nav.isStopped = true;

            StartCoroutine(Think());
        }

        private void Update()
        {
            if (!GameObject.FindGameObjectWithTag("Player").IsUnityNull() && target.IsUnityNull())
                target = GameObject.FindGameObjectWithTag("Player").transform;


            if (!_manager.EnemyDamage.IsUnityNull() && _manager.EnemyDamage.GetCurHealth() <= 0)
            {
                isLook = false;
                StopAllCoroutines();
                return;
            }

            if (isLook)
            {
                lookVec = new Vector3(
                    Input.GetAxisRaw("Horizontal"),
                    0,
                    Input.GetAxisRaw("Vertical")
                ) * 5f;

                transform.LookAt(target.position + lookVec);
            }
            else
            {
                _nav.SetDestination(tauntVec);
            }
        }

        private void FixedUpdate()
        {
        }


        public void SetChase(bool isChase)
        {
            _isChase = isChase;
        }

        public void SetNav(bool active)
        {
            _nav.enabled = active;
        }

        public bool GetIsTest()
        {
            return false;
        }

        private IEnumerator Think()
        {
            yield return new WaitForSeconds(2f);

            var ranAction = Random.Range(0, 5);
            switch (ranAction)
            {
                case 0:
                case 1:
                    StartCoroutine(MissileShot());
                    break;
                case 2:
                case 3:
                    StartCoroutine(RockShot());
                    break;
                case 4:
                    StartCoroutine(Taunt());
                    break;
            }

            IEnumerator MissileShot()
            {
                _manager.animator.SetTrigger("DoShot");
                yield return new WaitForSeconds(0.2f);
                GenerateMissile(missilePortA.position, missilePortA.rotation);

                yield return new WaitForSeconds(0.3f);
                GenerateMissile(missilePortB.position, missilePortB.rotation);

                yield return new WaitForSeconds(2f);
                StartCoroutine(Think());
            }

            void GenerateMissile(Vector3 position, Quaternion rotation)
            {
                var m = Instantiate(missile, position, rotation);
                var ma = m.GetComponent<BossBullet>();
                ma.target = target;
            }

            IEnumerator RockShot()
            {
                isLook = false;
                _manager.animator.SetTrigger("DoBigShot");

                Instantiate(bulletBox, transform.position, transform.rotation);

                yield return new WaitForSeconds(3f);

                isLook = true;
                StartCoroutine(Think());
            }

            IEnumerator Taunt()
            {
                tauntVec = target.position + lookVec;
                isLook = false;
                _nav.isStopped = false;

                gameObject.GetComponent<BoxCollider>().enabled = false;

                _manager.animator.SetTrigger("DoTaunt");

                yield return new WaitForSeconds(1.5f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                _nav.isStopped = true;
                isLook = true;
                gameObject.GetComponent<BoxCollider>().enabled = true;

                StartCoroutine(Think());
            }
        }

        private IEnumerator Attacking()
        {
            yield return new WaitForSeconds(0.2f);
        }
    }
}