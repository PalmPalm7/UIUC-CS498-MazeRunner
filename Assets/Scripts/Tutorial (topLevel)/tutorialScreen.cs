using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScreen : MonoBehaviour {
    public int state;
    public AudioSource ping;
	// Use this for initialization
	void Start () {
        state = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (state <= 7) {
            this.GetComponent<Renderer>().material = Resources.Load("tutorialLevel/Screens/Screen" + state) as Material;
        } else {
            betweenScenes.tutorialOver = true;
        }
	}
}
