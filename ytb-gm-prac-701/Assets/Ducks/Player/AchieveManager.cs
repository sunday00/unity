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

        public GameObject[] lockCharacters;
        public GameObject[] unlockCharacters;

        private Achieve[] _achieves;

        private void Awake()
        {
            _achieves = (Achieve[])Enum.GetValues(typeof(Achieve));

            if (!PlayerPrefs.HasKey("MyData")) Init();
        }

        private void Start()
        {
            UnlockCharacter();
        }

        private void Init()
        {
            PlayerPrefs.SetInt("MyData", 1);

            foreach (var achieve in _achieves) PlayerPrefs.SetInt(achieve.ToString(), 0);
        }

        private void UnlockCharacter()
        {
            for (var index = 0; index < lockCharacters.Length; index++)
            {
                var achieveName = _achieves[index].ToString();
                var isUnlock = PlayerPrefs.GetInt(achieveName) == 1;

                lockCharacters[index].SetActive(!isUnlock);
                unlockCharacters[index].SetActive(isUnlock);
            }
        }
    }
}