using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public GameObject player;
	public string trigger;
	public GameObject sword;
	private Timer triggerTimer;

    private bool unique;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
		
		sword = Instantiate((GameObject)Resources.Load ("Powers/Sword", typeof(GameObject)), this.transform.position + new Vector3(0.001f, 0.071f, 0.177f), Quaternion.Euler(30f, 0f, 80f));
		sword.transform.parent = this.transform;
		sword.transform.position += Vector3.Normalize(this.transform.forward)*3/4;
		sword.active = false;

        triggerTimer = new Timer(0.6f);
        triggerTimer.Start();

		trigger = "TriggerR";

        unique = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis (trigger) > 0.0f) {
			sword.active = true;
			sword.GetComponent<Renderer>().enabled = false;
			triggerTimer.Update ();
			if (triggerTimer.Complete ()) {
				sword.GetComponent<Renderer>().enabled = true;

                //Play sword activation noise
                if (unique) {
                    GameObject.Find("otherSounds").transform.Find("sword").GetComponent<AudioSource>().Play();
                    unique = false;
                }
            }
		} else {
			triggerTimer.Restart();
			sword.active = false;
            unique = true;
		}
	}
}
