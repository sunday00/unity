using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public Manager manager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        {
            if (manager.life > 0)
            {
                UpdateLifeIcon(-1);
                manager.RespawnPlayer();
            }
            else
            {
                manager.gameOverPanel.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }

    private void UpdateLifeIcon(int amount)
    {
        manager.life += amount;

        for (var i = 0; i <= manager.life; i++)
        {
            if (i < manager.life) continue;

            manager.lifes[i].color = new Color(0, 0, 0, 0);
        }
    }
}