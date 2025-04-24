using Ducks.Interactival.Enemies;
using Ducks.Player;
using UnityEngine;

namespace Ducks
{
    public class GameManager : MonoBehaviour
    {
        public GameObject menuCam;
        public GameObject mainCam;

        public PlayerManager playerManager;

        public EnemyManager boss;

        public int stage;
        public float playTime;
        public bool isBattle;

        public int enemyCntA;
        public int enemyCntB;
        public int enemyCntC;
    }
}