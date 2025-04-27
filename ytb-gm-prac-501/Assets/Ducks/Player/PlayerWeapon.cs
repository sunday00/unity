using Unity.VisualScripting;
using UnityEngine;

namespace Ducks.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        public Constants.WeaponType type;
        public float rate;

        public PlayerManager playerManager;

        private Animator _animator;

        private float _fireDelay;
        public IPlayerWeapon WeaponSubManager;

        public void Awake()
        {
            _animator = playerManager.PlayerMove.GetAnimator;
            WeaponSubManager = type.Equals(Constants.WeaponType.Melee)
                ? GetComponent<PlayerWeaponMelee>()
                : GetComponent<PlayerWeaponRange>();
        }

        private void Update()
        {
            var isFire = Input.GetButton("Fire1");
            Attack(isFire);
        }

        public void Use()
        {
            WeaponSubManager.StartRoutine();
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
            _fireDelay = 0;
        }
    }
}