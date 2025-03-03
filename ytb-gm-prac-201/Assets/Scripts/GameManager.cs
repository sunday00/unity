using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalPoints = 0;
    public int stagePoints = 0;
    public int stageIndex = 0;

    public PlayerMove player;
    public int health = 100;

    public GameObject[] stages;
    
    public void NextStage()
    {
        if (stageIndex >= stages.Length - 1)
        {
            Time.timeScale = 0;
            
            return;
        }
        
        stages[stageIndex].SetActive(false);
        totalPoints += stagePoints;
        stagePoints = 0;
        
        stageIndex++;
        Respawn();
        stages[stageIndex].SetActive(true);
    }

    public void PlayerDamaged()
    {
        if (health > 0)
        {
            health -= 25;
        }
        else
        {
            player.OnDeath();
        }
    }

    public void Respawn()
    {
        player.transform.position = new Vector3(0, 0, 0);
        player.rb.linearVelocity = Vector3.zero;
    }
}
