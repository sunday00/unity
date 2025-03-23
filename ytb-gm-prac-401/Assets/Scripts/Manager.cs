using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Player player;

    public int life;
    public int score;

    public Text scoreText;
    public Image[] lifes;
    public GameObject gameOverPanel;

    public ObjectManager objectManager;

    private void Awake()
    {
        player.GetComponent<PlayerFiring>().curBullet.bulletCount = 1;
        player.GetComponent<PlayerFiring>().subBullet.bulletCount = 1;
    }

    private void Update()
    {
        scoreText.text = string.Format("{0:n0}", score);
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnActHalf", 1.5f);
        Invoke("RespawnActFull", 3f);
        player.GetComponent<PlayerFiring>().curBullet.bulletCount = 1;
        player.GetComponent<PlayerFiring>().subBullet.bulletCount = 1;
        foreach (var follower in player.GetComponent<PlayerItem>().followers) follower.SetActive(false);
    }

    private void RespawnActHalf()
    {
        player.transform.position = Vector3.down * 3;
        player.gameObject.SetActive(true);
        player.SetSpriteOpacity(0.4f);
    }

    private void RespawnActFull()
    {
        player.GetComponent<PlayerHit>().isHit = false;
        player.SetSpriteOpacity(1f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}