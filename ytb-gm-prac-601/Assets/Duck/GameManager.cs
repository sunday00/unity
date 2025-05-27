using System.Collections;
using System.Collections.Generic;
using Duck.Player;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Duck
{
    public class GameManager : MonoBehaviour
    {
        public enum Sfx
        {
            LevelUp,
            Next,
            Attach,
            Button,
            Over
        }

        public Dongle lastDongle;
        public GameObject donglePrefab;
        public Transform dongleGroup;

        public GameObject effectPrefab;
        public Transform effectGroup;

        public int maxLevel = 2;

        public int score;

        public bool isOver;

        public AudioSource bgmPlayer;
        public AudioSource[] sfxPlayers;
        public int sfxCursor;
        public AudioClip[] sfxClips;

        public List<Dongle> dongles;
        public List<ParticleSystem> particles;

        [Range(1, 30)] public int poolSize;
        public int poolCursor;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            dongles = new List<Dongle>();
            particles = new List<ParticleSystem>();

            for (var i = 0; i < poolSize; i++) MakeDongle();
        }

        private void Start()
        {
            bgmPlayer.Play();
            NextDongle();
        }

        private Dongle MakeDongle()
        {
            var effectObject = Instantiate(effectPrefab, effectGroup);
            effectObject.name = "Effect " + particles.Count;
            var effect = effectObject.GetComponent<ParticleSystem>();
            particles.Add(effect);

            var dongleObject = Instantiate(donglePrefab, dongleGroup);
            dongleObject.name = "Dongle " + dongles.Count;
            var dongle = dongleObject.GetComponent<Dongle>();

            dongle.Manager = this;
            dongle.effect = effect;

            dongles.Add(dongle);

            return dongle;
        }

        private Dongle GetDongle()
        {
            return null;
        }

        private void NextDongle()
        {
            if (isOver) return;

            lastDongle = GetDongle();
            // lastDongle.level = Random.Range(0, 8);
            // INFO: dev
            lastDongle.level = Random.Range(0, maxLevel);
            lastDongle.gameObject.SetActive(true);

            SfxPlay(Sfx.Next);
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
            // print("GameOver");

            StartCoroutine(GameOverRoutine());
        }

        private IEnumerator GameOverRoutine()
        {
            var dongles = FindObjectsByType<Dongle>(FindObjectsSortMode.None);

            foreach (var dongle in dongles) dongle.rb.simulated = false;

            foreach (var dongle in dongles)
            {
                dongle.Hide(Vector2.up * 100);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(1f);

            SfxPlay(Sfx.Over);
        }

        public void SfxPlay(Sfx type)
        {
            switch (type)
            {
                case Sfx.LevelUp:
                    sfxPlayers[sfxCursor].clip = sfxClips[Random.Range(0, 3)];
                    break;
                case Sfx.Next:
                    sfxPlayers[sfxCursor].clip = sfxClips[3];
                    break;
                case Sfx.Attach:
                    sfxPlayers[sfxCursor].clip = sfxClips[4];
                    break;
                case Sfx.Button:
                    sfxPlayers[sfxCursor].clip = sfxClips[5];
                    break;
                case Sfx.Over:
                    sfxPlayers[sfxCursor].clip = sfxClips[6];
                    break;
            }

            sfxPlayers[sfxCursor].Play();
            sfxCursor = (sfxCursor + 1) % sfxPlayers.Length;
        }
    }
}