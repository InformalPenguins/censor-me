using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip coinSound;
    [SerializeField]
    private AudioClip shootSound;
    [SerializeField]
    private AudioClip deathMusic;
    [SerializeField]
    private AudioClip mainMenuMusic;
    [SerializeField]
    private AudioClip menuSound;
    [SerializeField]
    private AudioClip[] gameMusic;

    private AudioSource myMusicPlayer;
    private List<AudioSource> effects;

    public bool HasSound { get; set; }

    void LateUpdate() {
        effects.ForEach(x => {
            if (!x.isPlaying) {
                Destroy(x);
                effects.Remove(x);
            }
        });
    }

    public static AudioManager Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            myMusicPlayer = gameObject.AddComponent<AudioSource>();
            effects = new List<AudioSource>();
            HasSound = true;
        } else {
            Destroy(gameObject);
        }
    }

    private void PlaySFX(AudioClip sfx) {
        if (!HasSound) {
            return;
        }

        AudioSource tmpAudioSource = gameObject.AddComponent<AudioSource>();
        tmpAudioSource.clip = sfx;
        tmpAudioSource.Play();

        effects.Add(tmpAudioSource);
    }

    public void PlayCoinSFX() {
        PlaySFX(coinSound);
    }

    public void PlayShootSFX() {
        PlaySFX(shootSound);
    }

    public void PlayDeathSFX() {
        PlaySFX(deathSound);
    }

    public void PlayMenuSFX() {
        PlaySFX(menuSound);
    }

    public void PlayMenuMusic() {
		// we do not want to restart the the music again
		if (myMusicPlayer.clip == null || !myMusicPlayer.clip.Equals (mainMenuMusic)) {
			PlayMusic(mainMenuMusic);
		}
    }

    public void PlayGameMusic() {
        int index = Random.Range(0, gameMusic.Length);
        AudioClip myMusic = gameMusic[index];
        PlayMusic(myMusic);
    }

    public void PlayMusic(AudioClip music) {
        myMusicPlayer.Stop();
        myMusicPlayer.clip = music;
        myMusicPlayer.volume = HasSound ? 0.5f : 0f;
        myMusicPlayer.Play();
        myMusicPlayer.loop = true;
    }

    public void StopGameMusic() {
        if (myMusicPlayer) {
            myMusicPlayer.Stop();
        }
    }

    public void PauseGameMusic() {
        if (myMusicPlayer) {
            myMusicPlayer.Pause();
        }
    }

    public void ResumeGameMusic() {
        if (myMusicPlayer) {
            myMusicPlayer.UnPause();
        }
    }

    public void Silence() {
        if (effects.Count > 0) {
            LateUpdate();
        }
        ToggleMute();
    }

    public void ToggleMute() {
        if (HasSound) {
            myMusicPlayer.volume = 0f;
            HasSound = false;
        } else {
            myMusicPlayer.volume = 0.5f;
            HasSound = true;
        }
    }
}
