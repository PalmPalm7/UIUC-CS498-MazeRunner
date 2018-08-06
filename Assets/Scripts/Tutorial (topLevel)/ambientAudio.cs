using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambientAudio : MonoBehaviour {
    public AudioSource ambientSound;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!ambientSound.isPlaying) {
            ambientSound.time = 0.3f;
            ambientSound.Play();
        }

	}
}
