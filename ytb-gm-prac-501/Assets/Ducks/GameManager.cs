using Ducks.Interactival.Enemies;
using Ducks.Player;
using UnityEngine;
using UnityEngine.UI;

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

        public GameObject menuPanel;
        public GameObject mainPanel;
        public Text maxScoreText;

        public Text scoreText;
        public Text stageText;
        public Text playtimeText;

        public Text playerHealthText;
        public Text playerAmmoText;
        public Text playerCoinText;

        public Image weapon1Img;
        public Image weapon2Img;
        public Image weaponGImg;

        public Text enemyAText;
        public Text enemyBText;
        public Text enemyCText;

        public RectTransform bossHealthGroup;
        public RectTransform bossHealthBar;
    }
}