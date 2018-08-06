using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralTurret : Turret {
    // Use this for initialization
    public override void Start () {
		base.Start ();
		base.projectile = (GameObject)Resources.Load ("Turrets/Projectile", typeof(GameObject));
        base.projectileSpeed *= 0.7f;
		base.health = 2f;

		cycle.SetDuration(0.25f);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.cycle.Update ();
         this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0f, this.transform.rotation.eulerAngles.y + 20f, 0f), 0.1f);

		//Should we shoot
		if (cycle.Complete ()) {
			
			//Shoot a projectile at the target
			if (base.projectile != null) {
				//Create a projectile at our current position
				GameObject proj = Instantiate(base.projectile, this.transform.position, this.transform.rotation);
                //Play shot noise at current position
                AudioSource.PlayClipAtPoint(shotNoise, this.transform.position);

                //Determine direction and velocity to shoot at
                Vector3 dir = Vector3.Normalize(this.transform.forward);
				proj.GetComponent<Projectile>().direction = dir;
				proj.GetComponent<Projectile>().speed = projectileSpeed;
				proj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
				
				//random scale
				float randSize = Random.Range(0.4f, 0.7f);
				proj.transform.localScale = new Vector3(randSize, randSize, randSize);
			   
				//this.transform.RotateAround(this.transform.position, Vector3.up, 20f);
			}

			cycle.Restart ();
		}

	}
}
