using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    private Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[]
        {
            "Hello Staring?",
            "You should be here very right before, right?",
            "Let me updated something you.",
        });
        
        talkData.Add(100, new string[]
        {
            "Something is written.",
            "Don't Open before 9.",
        });
    }

    public string GetTalk(int talkId, int index)
    {
        if(index >= talkData[talkId].Length) return null;
        
        return talkData[talkId][index];
    }
}
