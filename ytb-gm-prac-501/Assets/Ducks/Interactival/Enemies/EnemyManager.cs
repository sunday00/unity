using Ducks.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Ducks.Interactival.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public Constants.EnemyType Type;

        public Animator animator;

        public int score;

        public GameObject player;
        public PlayerManager playerManager;

        public IEnemyDamage EnemyDamage;
        public IEnemyMove EnemyMove;

        private void Awake()
        {
            EnemyDamage = GetComponent<IEnemyDamage>();
            EnemyMove = GetComponent<IEnemyMove>();

            animator = GetComponentInChildren<Animator>();

            player = GameObject.FindGameObjectWithTag("Player");

            if (player.IsUnityNull()) return;

            playerManager = player.GetComponent<PlayerManager>();
        }
    }
}