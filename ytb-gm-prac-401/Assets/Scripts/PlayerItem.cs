using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public Manager manager;
    public Player player;

    public int boomCount = 3;
    public Text boomCountText;
    public GameObject boom;

    public ObjectManager objectManager;

    public GameObject[] followers;


    private void Update()
    {
        boomCountText.text = boomCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Item")) return;

        if (other.name.Contains("Bomb"))
            DoItemBomb();
        else if (other.name.Contains("Coin"))
            DoItemCoin();
        else if (other.name.Contains("Power")) DoItemPower();
        else if (other.name.Contains("Life")) DoItemLife();

        // Destroy(other.gameObject);
        other.gameObject.SetActive(false);
    }

    private void DoItemBomb()
    {
        boomCount++;
    }

    public void EnableBoom()
    {
        if (boomCount <= 0) return;
        if (boom.activeSelf) return;

        boomCount--;
        boom.SetActive(true);

        // var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var enemiesGroup = objectManager.GetEnemiesObjects();
        foreach (var enemisGroup in enemiesGroup) DestroyEnemies(enemisGroup);

        Invoke("DisableBoom", 2f);
    }

    private void DestroyEnemies(GameObject[] enemies)
    {
        // for (var i = 0; i < enemies.Length; i++)
        foreach (var enemy in enemies)
        {
            if (!enemy.activeSelf) continue;
            if (enemy.name.Contains("EnemyBullet"))
            {
                // Destroy(enemy);
                enemy.SetActive(false);
                continue;
            }

            if (enemy.name.Contains("Boss"))
                enemy.GetComponent<BossHit>().OnHit(300);
            else
                enemy.GetComponent<EnemyHit>().OnHit(300);
        }
    }

    private void DisableBoom()
    {
        boom.SetActive(false);
    }


    private void DoItemCoin()
    {
        manager.score += 1000;
    }

    private void DoItemPower()
    {
        var cur = player.GetComponent<PlayerFiring>().curBullet;
        if (cur.bulletCount >= 7)
        {
            manager.score += 500;
            foreach (var follower in followers)
                if (!follower.activeSelf)
                {
                    follower.SetActive(true);
                    break;
                }
        }
        else
        {
            cur.bulletCount += 2;
        }
    }

    private void DoItemLife()
    {
        if (manager.life >= 3)
        {
            manager.score += 1000;
            return;
        }

        GetComponent<PlayerHit>().UpdateLifeIcon(1);
    }
}