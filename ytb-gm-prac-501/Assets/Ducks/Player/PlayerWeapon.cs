using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Ducks.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        public Constants.WeaponType type;

        public int damage;
        public float rate;

        public BoxCollider meleeArea;
        public TrailRenderer trailEffect;
        public PlayerManager playerManager;
        private Animator _animator;

        private float _fireDelay;

        public void Awake()
        {
            _animator = playerManager.PlayerMove.GetAnimator;
        }

        private void Update()
        {
            var isFire = Input.GetButton("Fire1");
            Attack(isFire);
        }

        public void Use()
        {
            switch (type)
            {
                case Constants.WeaponType.Melee:
                    StopCoroutine("Swing");
                    StartCoroutine("Swing");
                    break;
                case Constants.WeaponType.Range:
                    break;
            }
        }

        private IEnumerator Swing()
        {
            yield return new WaitForSeconds(0.1f);
            meleeArea.enabled = true;
            trailEffect.enabled = true;
            _animator.SetTrigger("DoSwing");
            _fireDelay = 0;

            yield return new WaitForSeconds(0.3f);
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.3f);
            trailEffect.enabled = false;

            yield break;
        }

        private void Attack(bool isFire)
        {
            _fireDelay += Time.deltaTime;

            if (!isFire) return;

            var equipped = playerManager.PlayerItem.equipped;

            if (equipped.IsUnityNull()) return;
            if (playerManager.PlayerMove.IsActing) return;

            if (_fireDelay < rate) return;

            Use();
        }
    }
}