using UnityEngine;

namespace Ducks.Interactival.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemyDamage EnemyDamage;
        public EnemyMove EnemyMove;

        public Animator animator;

        private void Awake()
        {
            EnemyDamage = GetComponent<EnemyDamage>();
            EnemyMove = GetComponent<EnemyMove>();

            animator = GetComponentInChildren<Animator>();
        }
    }
}