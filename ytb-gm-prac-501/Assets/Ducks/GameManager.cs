using Ducks.Interactival.Enemies;
using Ducks.Player;
using Unity.VisualScripting;
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

        public BossDamage bossDamage;

        private void Awake()
        {
            maxScoreText.text = string.Format("{0:N0}", PlayerPrefs.GetInt("maxScore"));
        }

        private void Update()
        {
            if (!isBattle) return;

            playTime += Time.deltaTime;
        }

        private void LateUpdate()
        {
            if (!playerManager.gameObject.activeSelf) return;

            scoreText.text = string.Format("{0:N0}", playerManager.PlayerState.score);
            stageText.text = "Stage " + stage;

            var t = GetTimeFormat();
            playtimeText.text =
                string.Format("{0:00}", t[0]) + ":" +
                string.Format("{0:00}", t[1]) + ":" +
                string.Format("{0:00}", t[2]);

            playerHealthText.text = playerManager.PlayerState.curHealth + "/" + playerManager.PlayerState.maxHealth;
            playerCoinText.text = string.Format("{0:N0}", playerManager.PlayerState.curCoin);

            if (!playerManager.PlayerItem.equipped.IsUnityNull())
            {
                var equipped = playerManager.PlayerItem.equipped.GetComponent<PlayerWeapon>();

                if (equipped.WeaponSubManager.GeWeaponType().Equals(Constants.WeaponType.Range))
                    playerAmmoText.text = equipped.WeaponSubManager.GetCurrentAmmo() + " / " +
                                          playerManager.PlayerState.curAmmo;
                else
                    playerAmmoText.text = " - / " + playerManager.PlayerState.curAmmo;
            }

            weapon1Img.color = new Color(1, 1, 1,
                playerManager.PlayerItem.Inventory.ContainsKey("WeaponHammer") &&
                playerManager.PlayerItem.Inventory["WeaponHammer"] > 0
                    ? 1
                    : 0);

            weapon2Img.color = new Color(1, 1, 1,
                playerManager.PlayerItem.Inventory.ContainsKey("WeaponHandgun") &&
                playerManager.PlayerItem.Inventory["WeaponHandgun"] > 0
                    ? 1
                    : 0);

            weaponGImg.color = new Color(1, 1, 1,
                playerManager.PlayerState.curGrenades > 0 ? 1 : 0);

            enemyAText.text = enemyCntA.ToString();
            enemyBText.text = enemyCntB.ToString();
            enemyCText.text = enemyCntC.ToString();

            bossHealthBar.localScale =
                new Vector3((float)bossDamage.curHealth / bossDamage.maxHealth, 1, 1);
        }

        public void GameStart()
        {
            menuCam.SetActive(false);
            mainCam.SetActive(true);

            menuPanel.SetActive(false);
            mainPanel.SetActive(true);

            playerManager.gameObject.SetActive(true);

            isBattle = true;
        }

        private int[] GetTimeFormat()
        {
            var hour = (int)playTime / 3600;
            var minute = (int)((playTime - hour * 3600) / 60);
            var second = (int)(playTime % 60);

            return new[] { hour, minute, second };
        }

        public void StageStart()
        {
        }
        
        public void StageEnd()
        {
        }
    }
}