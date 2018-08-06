using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTurret : Turret
{

    // Use this for initialization

    public override void Start()
    {
        base.Start();
        base.projectile = (GameObject)Resources.Load("Turrets/TrackingProjectile", typeof(GameObject));
        base.projectileSpeed *= 0.75f;
		base.health = 2f;

		cycle.SetDuration(3f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.cycle.Update();

        //Should we shoot
        if (cycle.Complete())
        {

            //Shoot a projectile at the target
            if (base.projectile != null)
            {
                //Create a projectile at our current position
                GameObject proj = Instantiate(base.projectile, this.transform.position, this.transform.rotation);
                //Play shot noise at current position
                AudioSource.PlayClipAtPoint(shotNoise, this.transform.position);

                //Determine direction and velocity to shoot at
                Vector3 dir = Vector3.Normalize(this.transform.forward);
                proj.GetComponent<Projectile>().direction = dir;
                proj.GetComponent<Projectile>().speed = base.projectileSpeed;
                proj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
				float randSize = Random.Range(0.4f, 0.7f);
				proj.transform.localScale = new Vector3(randSize, randSize, randSize);
            }

            cycle.Restart();
        }

    }
}
