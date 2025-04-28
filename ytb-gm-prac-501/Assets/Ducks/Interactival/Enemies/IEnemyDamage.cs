using UnityEngine;

namespace Ducks.Interactival.Enemies
{
    public interface IEnemyDamage
    {
        public void HitByGrenade(Vector3 pos);

        public int GetCurHealth();

        public int GetMaxHealth();
    }
}