using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    public Vector3 direction;
    public Timer lifetime;

	// Use this for initialization
	public void Start () {
        lifetime = new Timer(5f);
        lifetime.Start();

        //Play fwoosh noise
        GameObject.Find("otherSounds").transform.Find("fireball").GetComponent<AudioSource>().Play();

    }
	
	// Update is called once per frame
	void Update () {
        lifetime.Update();

        this.transform.position += this.direction * Time.timeScale * Time.deltaTime;


        if (lifetime.Complete())
        {
            Destroy(this.transform.gameObject);
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        Turret t = other.gameObject.GetComponent<Turret>();

        if(t != null)
        {
            t.Hit(1f);

              //Destroy
            Destroy(this.transform.gameObject);
        }

      
    }
}
