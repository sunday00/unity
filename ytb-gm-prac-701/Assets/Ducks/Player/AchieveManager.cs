using System;
using UnityEngine;

namespace Ducks.Player
{
    public class AchieveManager : MonoBehaviour
    {
        public enum Achieve
        {
            UnlockBob
        }

        public GameObject[] lockCharacter;
        public GameObject[] unlockCharacter;

        private Achieve[] _achieves;

        private void Awake()
        {
            _achieves = (Achieve[])Enum.GetValues(typeof(Achieve));

            if (!PlayerPrefs.HasKey("MyData")) Init();
        }

        private void Init()
        {
            PlayerPrefs.SetInt("MyData", 1);

            foreach (var achieve in _achieves) PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }
}