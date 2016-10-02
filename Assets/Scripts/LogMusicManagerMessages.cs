using UnityEngine;
using System.Collections;

public class LogMusicManagerMessages : MonoBehaviour {
    void OnMusicStopping() {
        Debug.Log("Music stopping...");
    }

    void OnMusicStarting() {
        Debug.Log("Music starting...");
    }

    void OnMusicStopped() {
        Debug.Log("Music stopped.");
    }

    void OnMusicStarted() {
        Debug.Log("Music started.");
    }

    void OnClipChanged() {
        Debug.Log("Clip changed.");
    }

    void OnMusicManagerAwake() {
        Debug.Log("Music manager awake.");
    }
}
