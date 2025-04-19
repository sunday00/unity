using System.Collections;
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

        private bool _isChase;

        private EnemyManager _manager;
        private NavMeshAgent _nav;
        private Rigidbody _rb;
        private bool isLook;

        private Vector3 lookVec;
        private Vector3 tauntVec;

        private void Awake()
        {
            _manager = GetComponent<EnemyManager>();
            _nav = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();

            StartCoroutine(Think());
        }

        private void Update()
        {
            if (isLook)
            {
                lookVec = new Vector3(
                    Input.GetAxisRaw("Horizontal"),
                    0,
                    Input.GetAxisRaw("Vertical")
                ) * 5f;

                transform.LookAt(target.position + lookVec);
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
            yield return new WaitForSeconds(0.1f);

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
                yield return new WaitForSeconds(2.5f);

                StartCoroutine(Think());
            }

            IEnumerator RockShot()
            {
                _manager.animator.SetTrigger("DoBigShot");
                yield return new WaitForSeconds(3f);

                StartCoroutine(Think());
            }

            IEnumerator Taunt()
            {
                _manager.animator.SetTrigger("DoTaunt");
                yield return new WaitForSeconds(3f);

                StartCoroutine(Think());
            }
        }

        private IEnumerator Attacking()
        {
            yield return new WaitForSeconds(0.2f);
        }
    }
}