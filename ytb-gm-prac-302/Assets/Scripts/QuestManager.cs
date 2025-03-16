using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionId;
    public GameObject[] questObjects;

    public Text questText;

    private Dictionary<int, QuestData> quests;

    private void Awake()
    {
        quests = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        quests.Add(10, new QuestData("1stQuest", new[] { 1000, 2000 }));
        quests.Add(20, new QuestData("2ndQuest", new[] { 1000, 2000 }));
    }

    public int GetQuestTalkId(int id)
    {
        return questId + questActionId;
    }

    public string CheckQuest(int id)
    {
        if (id == quests[questId].npcId[questActionId]) questActionId++;

        ControlObject(id);

        if (questActionId == quests[questId].npcId.Length) NextQuest();

        // TODO: 
        // when get coin then skip this. 
        if (questActionId >= 2) questActionId = 1;

        return quests[questId].questName;
    }

    public void NextQuest()
    {
        // TODO: 
        // when get coin then turn off.
        if (questId >= 20) return;

        questId += 10;
        questActionId = 0;
    }

    private void ControlObject(int targetId)
    {
        // print("questId: " + questId);
        // print("questActionId: " + questActionId);

        switch (questId)
        {
            case 10:
                if (questActionId.Equals(2)) questObjects[0].SetActive(true);
                break;
            case 20:
                // TODO: 
                // when get coin then turn off.
                if (targetId.Equals(5000)) questObjects[0].SetActive(false);
                break;
        }
    }
}