using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : Projectile
{

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        //Set direction then update
        base.direction = Vector3.Normalize(Camera.main.transform.position - this.transform.position);
        base.Update();
		
	}
	
	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player>())
		{
			other.gameObject.GetComponent<Player>().Hit(this.damage);
			Instantiate((GameObject)Resources.Load("Powers/ProjectileExplosion", typeof(GameObject)), this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
		}
		else if (other.tag == "sword" || other.tag == "fireball")
		{
			Instantiate((GameObject)Resources.Load("Powers/ProjectileExplosion", typeof(GameObject)), this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
		}
    }
}
