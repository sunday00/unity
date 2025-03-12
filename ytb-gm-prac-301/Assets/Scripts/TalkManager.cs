using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public Sprite[] portraits;
    private Dictionary<int, Sprite> portraitsData;

    private Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitsData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    private void GenerateData()
    {
        talkData.Add(1000, new[]
        {
            "Hello Staring?:0",
            "You should be here very right before, right?:1",
            "Let me updated something you.:2"
        });

        talkData.Add(2010, new[]
        {
            "BOY:0",
            "NOT GIRL:1",
            "WHAT??:2"
        });

        talkData.Add(1010, new[]
        {
            "Here's new quest.:0",
            "You can find something:1"
        });

        talkData.Add(2011, new[]
        {
            "Oh, you here.:0",
            "I... I.... mmm... :1"
        });

        talkData.Add(100, new[]
        {
            "Something is written.",
            "Don't Open before 9."
        });

        portraitsData.Add(1000 + 0, portraits[0]);
        portraitsData.Add(1000 + 1, portraits[1]);
        portraitsData.Add(1000 + 2, portraits[2]);
        portraitsData.Add(1000 + 3, portraits[3]);

        portraitsData.Add(2000 + 0, portraits[4]);
        portraitsData.Add(2000 + 1, portraits[5]);
        portraitsData.Add(2000 + 2, portraits[6]);
        portraitsData.Add(2000 + 3, portraits[7]);
    }

    public string GetTalk(int talkId, int index)
    {
        if (index >= talkData[talkId].Length) return null;

        return talkData[talkId][index];
    }

    public Sprite GetPortrait(int id, int index)
    {
        return portraitsData[id + index];
    }
}