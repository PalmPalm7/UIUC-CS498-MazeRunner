using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambientNoise : MonoBehaviour {
    public AudioSource ambientSound;
    public AudioSource gerudo;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!ambientSound.isPlaying) {
            ambientSound.time = 0.3f;
            ambientSound.Play();
        }
        if (gerudo.time >= 158.2f) {
            gerudo.Stop();
            gerudo.time = 14.3f;
            gerudo.Play();
        }
    }
}
