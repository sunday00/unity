using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public Manager manager;
    public bool isHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        {
            if (manager.life > 0)
            {
                if (isHit) return;
                isHit = true;

                UpdateLifeIcon(-1);
                manager.CallExplosion(transform.position, 1f);

                manager.RespawnPlayer();
            }
            else
            {
                manager.gameOverPanel.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }

    public void UpdateLifeIcon(int amount)
    {
        manager.life += amount;

        for (var i = 0; i < manager.lifes.Length; i++)
        {
            manager.lifes[i].color = new Color(1, 1, 1, 1);

            if (i < manager.life) continue;

            manager.lifes[i].color = new Color(0, 0, 0, 0);
        }
    }
}