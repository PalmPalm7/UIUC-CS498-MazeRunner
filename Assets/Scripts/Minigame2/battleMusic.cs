using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleMusic : MonoBehaviour {

    public AudioSource molduga;
    public AudioSource verge;
    public GameObject player;
	// Use this for initialization
	void Start () {
        
        molduga.Stop();
        verge.Stop();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<Player>().IsAlive()) {
            if (!molduga.isPlaying && !verge.isPlaying) {
                int rand = Random.Range(1, 100);
                if (rand < 50) {
                    molduga.Play();
                } else {
                    verge.Play();
                }
            }
        } else {
            molduga.Stop();
            verge.Stop();
        }
	}
}
