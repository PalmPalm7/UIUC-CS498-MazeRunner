using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class screenLeave : MonoBehaviour {
    public GameObject theFloor;
    public float timer;
    public AudioSource pyramidSound;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (betweenScenes.tutorialOver) {
            this.transform.localPosition += new Vector3(0.0f, 0.05f, 0.0f);
            theFloor.transform.localPosition -= new Vector3(0.0f, 0.01f, 0.0f);
            if (!pyramidSound.isPlaying) {
                pyramidSound.Play();
            }
            timer += Time.deltaTime;
            if (timer > 5) {
                SceneManager.LoadScene("Minigame 1");
            }
        }
	}
}
