using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public GameObject player;
    public string trigger;
    public GameObject shield;
    private Timer cooldown;
    

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        shield = Instantiate((GameObject)Resources.Load("Powers/Shield", typeof(GameObject)), this.transform.position, this.transform.rotation);
        shield.active = false;

        cooldown = new Timer(2f);
        cooldown.Start();

		trigger = "TriggerL";
		
		GameObject overlay = GameObject.Find("Transition");
        overlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		shield.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		shield.gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
        cooldown.Update();

        if (Input.GetAxis(trigger) > 0.0f)
        {
            if (cooldown.Complete())
            {
                shield.SetActive(true);
                //Play shield noise
                GameObject.Find("otherSounds").transform.Find("shield").GetComponent<AudioSource>().Play();

                // shield.GetComponent<Renderer>().enabled = true;
                shield.transform.position = this.transform.position + Vector3.Normalize(this.transform.forward) * 0.75f;
                shield.transform.rotation = this.transform.rotation;
                cooldown.Restart();
            }
        }

        if (cooldown.Complete())
        {
            shield.SetActive(false);
        }
    }
}
