using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerParticleScript : MonoBehaviour {
    public ParticleSystem myParticles;
	// Use this for initialization
	void Start () {
        myParticles = this.gameObject.GetComponent<ParticleSystem>();
        if (myParticles.isPlaying) {
            myParticles.Stop(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (betweenScenes.hasDefense && betweenScenes.hasOffense) {
            if (myParticles.isPlaying == false) {
                myParticles.Play(true);
            }
        } else {
            if (myParticles.isPlaying) {
                myParticles.Stop(true);
            }
        }
	}
}
