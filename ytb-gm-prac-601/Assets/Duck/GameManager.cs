using System.Collections;
using Duck.Player;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Duck
{
    public class GameManager : MonoBehaviour
    {
        public Dongle lastDongle;
        public GameObject donglePrefab;
        public Transform dongleGroup;

        public GameObject effectPrefab;
        public Transform effectGroup;

        public int maxLevel = 2;

        public int score;

        public bool isOver;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            NextDongle();
        }

        private Dongle GetDongle()
        {
            var effectObject = Instantiate(effectPrefab, effectGroup);
            var effect = effectObject.GetComponent<ParticleSystem>();

            var dongleObject = Instantiate(donglePrefab, dongleGroup);
            var dongle = dongleObject.GetComponent<Dongle>();

            dongle.effect = effect;

            return dongle;
        }

        private void NextDongle()
        {
            lastDongle = GetDongle();
            lastDongle.Manager = this;
            // lastDongle.level = Random.Range(0, 8);
            // INFO: dev
            lastDongle.level = Random.Range(0, maxLevel);
            lastDongle.gameObject.SetActive(true);

            StartCoroutine(WaitNext());
        }

        private IEnumerator WaitNext()
        {
            while (!lastDongle.IsUnityNull()) yield return null;

            yield return new WaitForSeconds(2.5f);

            NextDongle();
        }

        public void TouchDown()
        {
            if (lastDongle.IsUnityNull()) return;

            lastDongle.Drag();
        }

        public void TouchUp()
        {
            if (lastDongle.IsUnityNull()) return;

            lastDongle.Drop();
            lastDongle = null;
        }

        public void GameOver()
        {
            if (isOver) return;

            isOver = true;
            print("GameOver");
        }
    }
}