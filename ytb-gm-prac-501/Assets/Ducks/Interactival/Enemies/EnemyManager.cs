using UnityEngine;

namespace Ducks.Interactival.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public Constants.EnemyType Type;

        public Animator animator;

        public IEnemyDamage EnemyDamage;
        public IEnemyMove EnemyMove;

        private void Awake()
        {
            EnemyDamage = GetComponent<IEnemyDamage>();
            EnemyMove = GetComponent<IEnemyMove>();

            animator = GetComponentInChildren<Animator>();
        }
    }
}