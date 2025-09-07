using UnityEngine;

namespace Ducks
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("#BGM")] //
        public AudioClip bgmClip;

        public float bgmVolume;

        [Header("#SFX")] //
        public AudioClip[] sfxClips;

        public float sfxVolume;

        public int channels;

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

            var sfxObject = new GameObject("sfxPlayer");
            sfxObject.transform.parent = transform;

            sfxPlayers = new AudioSource[channels];
            for (var i = 0; i < channels; i++)
            {
                sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
                sfxPlayers[i].playOnAwake = false;
                sfxPlayers[i].volume = sfxVolume;
            }
        }
    }
}