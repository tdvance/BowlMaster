using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectMusic : MonoBehaviour {

    public AudioClip[] clips;
    public float[] volumes;
    float volume = 0;

    public void Start() {
        Select();
    }
    public void Select() {
        Dropdown dropdown = GetComponent<Dropdown>();
        AudioClip clip = clips[dropdown.value];
        MusicManager.instance.NewMusic(clip);
        if (dropdown.value < volumes.Length) {
            volume = volumes[dropdown.value];
        }else {
            volume = 0.25f;
        }
        Invoke("SetVolume", 2.1f);
    }

    void SetVolume() {
        MusicManager.instance.musicVolume = volume;
    }
}
