using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScript : MonoBehaviour {
    public GameObject Liquid;
    public liquidColor controlScript;
    public ParticleSystem myParticles;
    public bool shouldBePlaying;
	// Use this for initialization
	void Start () {
        controlScript = Liquid.GetComponent<liquidColor>();
        myParticles = this.gameObject.GetComponent<ParticleSystem>();
        if (myParticles.isPlaying) {
            myParticles.Stop(true);
        }
        shouldBePlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (controlScript.keepAccepting) {
            shouldBePlaying = false;
            if (myParticles.isPlaying) {
                myParticles.Stop(true);
            }
        } else {
           shouldBePlaying = true;
            if (!myParticles.isPlaying) {
                myParticles.Play(true);
            }
        }
	}
}
