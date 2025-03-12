using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject talkTarget;
    public Image talkTargetImage;

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
            talkTargetImage.sprite = null;
            return;
        }

        if (isNpc)
        {
            string[] talks = talk.Split(':');
            talkText.text = talks[0];
            talkTargetImage.sprite = talkManager.GetPortrait(talkId, int.Parse(talks[1])); 
            talkTargetImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            talkText.text = talk;
            talkTargetImage.color = new Color(1f, 1f, 1f, 0f);
        }

        talkIndex++;
    }
}
