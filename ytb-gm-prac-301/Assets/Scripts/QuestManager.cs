using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;

    private Dictionary<int, QuestData> quests;

    private void Awake()
    {
        quests = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        quests.Add(questId, new QuestData("1stQuest", new[] { 1000, 2000 }));
    }

    public int GetQuestTalkId(int id)
    {
        return questId;
    }
}