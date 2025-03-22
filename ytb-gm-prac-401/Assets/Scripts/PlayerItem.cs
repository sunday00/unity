using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public Manager manager;
    public Player player;

    public int boomCount = 3;
    public Text boomCountText;
    public GameObject boom;

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

        Destroy(other.gameObject);
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

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (var i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].name.Contains("EnemyBullet"))
            {
                Destroy(enemies[i]);
                continue;
            }

            enemies[i].GetComponent<Enemy>().OnHit(1000);
        }

        Invoke("DisableBoom", 2f);
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
        if (cur.bulletCount >= 7) manager.score += 500;
        else cur.bulletCount += 2;
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