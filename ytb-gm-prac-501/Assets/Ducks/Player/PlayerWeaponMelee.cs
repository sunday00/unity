using System.Collections;
using UnityEngine;

namespace Ducks.Player
{
    public class PlayerWeaponMelee : MonoBehaviour, IPlayerWeapon
    {
        public int damage;

        public BoxCollider meleeArea;
        public TrailRenderer trailEffect;
        public PlayerManager playerManager;

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
            meleeArea.enabled = true;
            trailEffect.enabled = true;
            _animator.SetTrigger(Constants.DoSwing);

            yield return new WaitForSeconds(1f);
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.3f);
            trailEffect.enabled = false;

            yield break;
        }

        public Constants.WeaponType GeWeaponType()
        {
            return Constants.WeaponType.Melee;
        }

        public int GetCurrentAmmo()
        {
            return 0;
        }
    }
}