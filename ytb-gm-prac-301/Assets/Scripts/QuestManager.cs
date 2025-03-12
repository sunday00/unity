using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionId;

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

        if (questActionId == quests[questId].npcId.Length) NextQuest();

        return quests[questId].questName;
    }

    public void NextQuest()
    {
        questId += 10;
        questActionId = 0;
    }
}