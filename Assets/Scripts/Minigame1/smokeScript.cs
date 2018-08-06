using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeScript : MonoBehaviour {
    public liquidColor script;
    public float timer;
    public bool playSmoke;
    public ParticleSystem myParticles;
	// Use this for initialization
	void Start () {
        script = this.transform.parent.GetComponent<liquidColor>();
        playSmoke = false;
        myParticles = this.gameObject.GetComponent<ParticleSystem>();
        timer = 0;
        if (myParticles.isPlaying) {
            myParticles.Stop(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (playSmoke) {
            timer += Time.deltaTime;
            if (!myParticles.isPlaying) {
                myParticles.Play(true);
            }

            if (timer > 0.2) {
                if (myParticles.isPlaying) {
                    myParticles.Stop(true);

                }
                playSmoke = false;
                timer = 0;
            }
        }
	}
}
