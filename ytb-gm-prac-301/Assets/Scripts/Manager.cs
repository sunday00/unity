using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject talkTarget;

    public PlayerAnimatorProps playerAnimatorProps;

    public void Interact(GameObject target)
    {
        if (talkPanel.activeSelf)
        {
            talkText.text = "";
            talkPanel.SetActive(false);
            return;
        }
        
        talkPanel.SetActive(true);
        
        talkTarget = target;
        talkText.text = "Introduce: " + talkTarget.name;
    }
}
