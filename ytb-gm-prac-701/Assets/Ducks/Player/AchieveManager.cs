using System;
using System.Collections;
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
        public GameObject UiNotice;

        private Achieve[] _achieves;

        private WaitForSecondsRealtime wait;

        private void Awake()
        {
            _achieves = (Achieve[])Enum.GetValues(typeof(Achieve));
            wait = new WaitForSecondsRealtime(5f);

            if (!PlayerPrefs.HasKey("MyData")) Init();
        }

        private void Start()
        {
            UnlockCharacter();
        }

        private void LateUpdate()
        {
            foreach (var achieve in _achieves) CheckAchieves(achieve);
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

        private void CheckAchieves(Achieve achieve)
        {
            var isAchieve = false;

            switch (achieve)
            {
                case Achieve.UnlockBob:
                    isAchieve = GameManager.Instance.kill >= 10;
                    break;
            }

            if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
            {
                PlayerPrefs.SetInt(achieve.ToString(), 1);

                for (var index = 0; index < UiNotice.transform.childCount; index++)
                {
                    var isActive = index == (int)achieve;
                    UiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
                }

                StartCoroutine(NoticeRoutine());
            }
        }

        private IEnumerator NoticeRoutine()
        {
            UiNotice.SetActive(true);
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);

            yield return wait;

            UiNotice.SetActive(false);
        }
    }
}