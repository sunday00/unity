using UnityEngine;

namespace Ducks
{
    public class AudioManager : MonoBehaviour
    {
        public enum Sfx
        {
            Dead,
            Hit,
            LevelUp = 3,
            Lose,
            Melee,
            Range = 7,
            Select,
            Win
        }

        public static AudioManager Instance;

        [Header("#BGM")] //
        public AudioClip bgmClip;

        public float bgmVolume;

        [Header("#SFX")] //
        public AudioClip[] sfxClips;

        public float sfxVolume;

        public int channels;
        private AudioHighPassFilter bgmEffect;

        // private bgms
        private AudioSource bgmPlayer;

        // private sfx
        private int channelIndex;
        private AudioSource[] sfxPlayers;

        private void Awake()
        {
            Instance = this;

            Inint();
        }

        private void Inint()
        {
            var bgmObject = new GameObject("BgmPlayer");
            bgmObject.transform.parent = transform;
            bgmPlayer = bgmObject.AddComponent<AudioSource>();

            bgmPlayer.playOnAwake = false;
            bgmPlayer.loop = true;
            bgmPlayer.volume = bgmVolume;
            bgmPlayer.clip = bgmClip;
            bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

            var sfxObject = new GameObject("sfxPlayer");
            sfxObject.transform.parent = transform;

            sfxPlayers = new AudioSource[channels];
            for (var i = 0; i < channels; i++)
            {
                sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
                sfxPlayers[i].playOnAwake = false;
                sfxPlayers[i].bypassListenerEffects = true;
                sfxPlayers[i].volume = sfxVolume;
            }
        }

        public void PlayBgm(bool isPlay)
        {
            if (isPlay) bgmPlayer.Play();
            else bgmPlayer.Stop();
        }

        public void EffectBgm(bool isPlay)
        {
            bgmEffect.enabled = isPlay;
        }

        public void PlaySfx(Sfx sfx)
        {
            for (var i = 0; i < channels; i++)
            {
                var loopIndex = (i + channelIndex) % channels;

                if (sfxPlayers[loopIndex].isPlaying) continue;

                var randIndex = 0;
                if (sfx == Sfx.Hit || sfx == Sfx.Melee) randIndex = Random.Range(0, 2);

                sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + randIndex];
                sfxPlayers[loopIndex].Play();

                break;
            }
        }
    }
}