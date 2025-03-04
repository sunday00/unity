using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoints = 0;
    public int stagePoints = 0;
    public int stageIndex = 0;

    public PlayerMove player;
    public int health = 100;

    public GameObject[] stages;
    
    public Image[] UIHealth;
    public Text UIPoints;
    public Text UIStage;
    public GameObject UIRestart;

    public void Update()
    {
         UIPoints.text = (totalPoints + stagePoints).ToString();
         UIStage.text = "STAGE " + (stageIndex + 1).ToString();  
    }

    public void NextStage()
    {
        if (stageIndex >= stages.Length - 1)
        {
            Time.timeScale = 0;

            Text btnText = UIRestart.GetComponentInChildren<Text>();
            btnText.text = "CLEAR!";
            UIRestart.SetActive(true);
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
            health -= 1;
            UIHealth[health].color = new Color(1, 1, 1, 0.2f);
        }
        else
        {
            player.OnDeath();
            
            Text btnText = UIRestart.GetComponentInChildren<Text>();
            btnText.text = "Retry?";
            UIRestart.SetActive(true);
        }
    }

    public void Respawn()
    {
        player.transform.position = new Vector3(0, 0, 0);
        player.rb.linearVelocity = Vector3.zero;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/S0");
        Time.timeScale = 1;
    }
}
