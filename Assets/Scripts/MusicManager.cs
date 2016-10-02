using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

    [Tooltip("Music to play on instantiation of this class, if any")]
    public AudioClip startMusic;

    [Tooltip("Music volume; overrides audiosource value")]
    public float musicVolume = 0.8f;

    [Tooltip("If selected, music loops; overrides audiosource value")]
    public bool musicLoops = true;

    private AudioSource audioSource;

    private float fromVolume;
    private float toVolume;
    private float fadeTime = 0f;
    private float fadeStartTime;

    private float fv1, tv1, ft1;
    private float fv2, tv2, ft2;

    private AudioClip nextMusic;


    private static MusicManager _instance;

    public static MusicManager instance {
        get {
            return _instance;
        }
    }

    public void NewMusic(AudioClip audioClip, float fadeOut = 1.0f, float gap = 0f, float fadeIn = 1.0f) {
        fv1 = musicVolume;
        tv1 = 0;
        ft1 = fadeOut;
        fv2 = 0;
        tv2 = musicVolume;
        ft2 = fadeIn;
        nextMusic = audioClip;
        Fade1();
        Invoke("SetClip", fadeOut);
        Invoke("Fade2", fadeOut + gap);
        SendMessage("OnMusicStopping", SendMessageOptions.DontRequireReceiver);
    }

    public void StopMusic(float fadeOut = 1.0f) {
        fv1 = musicVolume;
        tv1 = 0;
        ft1 = fadeOut;
        fv2 = 0;
        tv2 = musicVolume;
        ft2 = 0.1f;
        Fade1();
        nextMusic = null;
        Invoke("SetClip", fadeOut);
        Invoke("Fade2", fadeOut);
        SendMessage("OnMusicStopping", SendMessageOptions.DontRequireReceiver);
    }

    public void StartMusic(AudioClip audioClip, float fadeIn = 1.0f) {
        fv1 = 0;
        tv1 = musicVolume;
        ft1 = fadeIn;
        musicVolume = 0;
        SetClip(audioClip);
        Fade1();
        SendMessage("OnMusicStarting", SendMessageOptions.DontRequireReceiver);
    }


    private void SetClip() {
        SetClip(nextMusic);
    }

    private void SetClip(AudioClip nextMusic) {
        audioSource.Stop();
        audioSource.clip = nextMusic;
        audioSource.Play();
        SendMessage("OnClipChanged", SendMessageOptions.DontRequireReceiver);
        if (nextMusic) {
            SendMessage("OnMusicStarting", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Fade1() {
        Fade(fv1, tv1, ft1);
    }

    private void Fade2() {
        Fade(fv2, tv2, ft2);
    }

    private void Fade(float fromVolume, float toVolume, float fadeTime) {
        this.fromVolume = fromVolume;
        this.toVolume = toVolume;
        this.fadeTime = fadeTime;
        this.fadeStartTime = Time.time;
    }

    void Awake() {
        MusicManager[] mm = FindObjectsOfType<MusicManager>();
        if (mm.Length > 1) {
            throw new UnityException("More than one music manager found.  This is meant to be a Singleton class.  Found: " + mm.ToList().ToString());
        }
        DontDestroyOnLoad(gameObject);
        _instance = this;
        SendMessage("OnMusicManagerAwake", SendMessageOptions.DontRequireReceiver);
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.loop = musicLoops;
        audioSource.volume = musicVolume;
        if (startMusic) {
            audioSource.clip = startMusic;
            audioSource.Play();
            SendMessage("OnMusicStarted", SendMessageOptions.DontRequireReceiver);
        }
    }

    void Update() {

        if (fadeTime > 0) {
            float t = (Time.time - fadeStartTime) / fadeTime;
            if (t >= 1) {
                musicVolume = toVolume;
                fadeTime = 0;
                if (musicVolume == 0) {
                    SendMessage("OnMusicStopped", SendMessageOptions.DontRequireReceiver);
                } else {
                    SendMessage("OnMusicStarted", SendMessageOptions.DontRequireReceiver);
                }
            } else {
                musicVolume = fromVolume * (1 - t) + toVolume * t;
            }
        }

        audioSource.loop = musicLoops;
        audioSource.volume = musicVolume;
    }




}
