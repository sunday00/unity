public class QuestData
{
    public int[] npcId;
    public string questName;

    public QuestData(string questName, int[] npcId)
    {
        this.questName = questName;
        this.npcId = npcId;
    }
}