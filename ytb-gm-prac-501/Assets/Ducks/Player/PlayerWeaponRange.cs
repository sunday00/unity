using System.Collections;
using UnityEngine;

namespace Ducks.Player
{
    public class PlayerWeaponRange : MonoBehaviour, IPlayerWeapon
    {
        public Constants.WeaponType type;

        public int damage;

        public PlayerManager playerManager;

        public GameObject bullet;
        public Transform bulletPos;
        public GameObject bulletCase;
        public Transform bulletCasePos;

        private Animator _animator;

        public void Awake()
        {
            _animator = playerManager.PlayerMove.GetAnimator;
        }

        public void StartRoutine()
        {
            StopCoroutine("Action");
            StartCoroutine("Action");
        }

        public IEnumerator Action()
        {
            yield return new WaitForSeconds(0.1f);
            _animator.SetTrigger(Constants.DoShot);

            var insBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity);
            insBullet.GetComponent<Rigidbody>().linearVelocity = bulletPos.forward * 50f;

            yield return null;

            var insBulletCase = Instantiate(bulletCase, bulletCasePos.position, Quaternion.identity);
            var insBulletCaseRb = insBulletCase.GetComponent<Rigidbody>();
            insBulletCaseRb.AddForce(bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3),
                ForceMode.Impulse);
            insBulletCaseRb.AddTorque(Vector3.up * 10, ForceMode.Impulse);

            yield break;
        }
    }
}