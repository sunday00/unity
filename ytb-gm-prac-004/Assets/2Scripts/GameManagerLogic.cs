using System;
using UnityEngine;
using UnityEngine.UI;

namespace _2Scripts
{
    public class GameManagerLogic : MonoBehaviour
    {
        public int totalItemCount;
        public int stage;
        public Text playerItemText;
        public Text stageItemText;

        public void Awake()
        {
            stageItemText.text = this.totalItemCount.ToString();
        }

        public void SetPlayerItemText(int score)
        {
            this.playerItemText.text = score.ToString();    
        }
    }
}

