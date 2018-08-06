using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : Turret {

	// Use this for initialization

	public override void Start () {
		base.Start ();
		base.projectile = (GameObject)Resources.Load ("Turrets/Projectile", typeof(GameObject));
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.cycle.Update ();

		//Should we shoot
		if (cycle.Complete ()) {
			//Shoot a projectile at the target
			if (base.target != null && base.projectile != null) {
				//Create a projectile at our current position
				AudioSource.PlayClipAtPoint(base.shotNoise, this.transform.position);
				GameObject proj = Instantiate(base.projectile, this.transform.position, this.transform.rotation);

				//Determine direction and velocity to shoot at
				Vector3 dir = Vector3.Normalize(base.target.transform.position - this.transform.position);
				proj.GetComponent<Projectile>().direction = dir;
				proj.GetComponent<Projectile>().speed = projectileSpeed;
                proj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
				float randSize = Random.Range(0.2f, 0.5f);
				proj.transform.localScale = new Vector3(randSize, randSize, randSize);
			}

			cycle.Restart ();
		}

		//Rotate towards player
		//Vector3.Dot(target.transform.position - this.transform.position, this.transform.LookAt) / (Vector3.Magnitude());
		if(base.target != null)
			this.transform.LookAt (base.target.transform.position);

	}
}
