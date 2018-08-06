using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	//The current speed of the projectile
	public float speed;
	public Vector3 direction;
	public float lifetime;
    public float damage;
	private Timer timer;

	// Use this for initialization
	public virtual void Start () {
        damage = 5f;
        lifetime = 25f;
        timer = new Timer (lifetime);
		timer.Start ();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		//Update timer
		this.timer.Update();

		//Check if we are expired
		if (timer.Complete ()) {
			//Kill ourselves
			Destroy(this.gameObject);
			Destroy (this);
		}

		//Update projectiles position
		this.transform.position += direction * speed * Time.deltaTime * Time.timeScale;
	}

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            other.gameObject.GetComponent<Player>().Hit(this.damage);
			Instantiate((GameObject)Resources.Load("Powers/ProjectileExplosion", typeof(GameObject)), this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
        }
		else if (other.tag == "shield") {
			Instantiate((GameObject)Resources.Load("Powers/ProjectileExplosion", typeof(GameObject)), this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
		}
    }
}
