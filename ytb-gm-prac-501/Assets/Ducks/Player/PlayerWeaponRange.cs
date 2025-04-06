using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public int ammoMax;
        public int ammoCur;

        private Animator _animator;


        public void Awake()
        {
            _animator = playerManager.PlayerMove.GetAnimator;
        }

        private void Update()
        {
            var isReload = Input.GetButton("Reload");
            Reload(isReload);
        }

        public void StartRoutine()
        {
            ammoCur--;
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

        private void Reload(bool isReload)
        {
            if (!isReload) return;

            if (playerManager.PlayerState.curAmmo <= 0) return;

            if (playerManager.PlayerMove.IsActing) return;

            _animator.SetTrigger(Constants.DoReload);
        }
    }
}