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
    public int talkIndex;

    public QuestManager questManager;

    public PlayerAnimatorProps playerAnimatorProps;

    public void Interact(GameObject target)
    {
        talkPanel.SetActive(true);

        talkTarget = target;
        var talkData = talkTarget.GetComponent<ObjectData>();
        Talk(talkData.id, talkData.isNpc);
    }

    public void Talk(int talkId, bool isNpc)
    {
        var questTalkId = questManager.GetQuestTalkId(talkId);

        // var talk = talkManager.GetTalk(talkId, talkIndex);
        var talk = talkManager.GetTalk(talkId + questTalkId, talkIndex);
        if (talk.IsUnityNull())
        {
            talkPanel.SetActive(false);
            talkText.text = "";
            talkIndex = 0;
            talkTargetImage.sprite = null;

            var questName = questManager.CheckQuest(talkId);
            print(questName);
            return;
        }

        if (isNpc)
        {
            var talks = talk.Split(':');
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