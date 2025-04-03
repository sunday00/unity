using System.Collections;
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

        public void Use()
        {
            switch (type)
            {
                case Constants.WeaponType.Melee:
                    StopCoroutine("Swing");
                    StartCoroutine("Swing");
                    break;
            }
        }

        private IEnumerator Swing()
        {
            yield return new WaitForSeconds(0.1f);
            meleeArea.enabled = true;
            trailEffect.enabled = true;

            yield return new WaitForSeconds(0.3f);
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.3f);
            trailEffect.enabled = false;

            yield break;
        }
    }
}