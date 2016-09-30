using UnityEngine;
using System.Collections;

public class Gutter : MonoBehaviour {
    private GameManager gameManager;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log("Gutter entered by " + collider.gameObject);
        if (collider.gameObject.transform.parent.GetComponent<Ball>()) {
            gameManager.Gutter();
        }
    }
}
