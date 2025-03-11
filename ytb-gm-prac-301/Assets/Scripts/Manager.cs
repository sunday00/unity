using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject talkTarget;

    public TalkManager talkManager;
    public int talkIndex = 0;
    
    public PlayerAnimatorProps playerAnimatorProps;

    public void Interact(GameObject target)
    {
        talkPanel.SetActive(true);
        
        talkTarget = target;
        ObjectData talkData = talkTarget.GetComponent<ObjectData>();
        Talk(talkData.id, talkData.isNpc);   
    }

    public void Talk(int talkId, bool isNpc)
    {
        string talk = talkManager.GetTalk(talkId, talkIndex);
        if (talk.IsUnityNull())
        {
            talkPanel.SetActive(false);
            talkText.text = "";
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talk;
        }
        else
        {
            talkText.text = talk;
        }

        talkIndex++;
    }
}
