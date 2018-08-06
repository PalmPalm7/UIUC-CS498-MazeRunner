using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : Turret
{

    // Use this for initialization
    public float num_projectiles;
    public GameObject obj;  

    public override void Start()
    {
        base.Start();
        base.projectile = (GameObject)Resources.Load("Turrets/Projectile", typeof(GameObject));
        base.health = 10f;
        this.num_projectiles = 12f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.cycle.Update();
        this.cycle.Update();
        
        //Should we shoot
        if (cycle.Complete())
        {
            //Shoot a projectile at the target
            if (base.target != null && base.projectile != null)
            {
				//Play shot noise at current position
				AudioSource.PlayClipAtPoint(base.shotNoise, this.transform.position);
				
                Vector3 v1 = Vector3.Normalize(base.target.transform.position - this.transform.position);
                Vector3 v2 = Vector3.Normalize(new Vector3(1.0f, 1.0f, (-v1.x * - v1.y)/v1.z));
                //this.obj.transform.position = this.transform.position - (this.transform.forward * 1.4f);
                this.obj.transform.LookAt(v2);
                this.obj.transform.Rotate(new Vector3(0f, -90f, 0f));
                float rot = 360f / num_projectiles;

                for (int i = 0; i < num_projectiles; i++)
                {
                    float projSpeed = base.projectileSpeed;
                    if (Random.value < 0.05f)
                    {
                        projectile = (GameObject)Resources.Load("Turrets/TrackingProjectile", typeof(GameObject));
                        projSpeed *= 0.4f;
                    }
                    else
                    {
                        projectile = (GameObject)Resources.Load("Turrets/Projectile", typeof(GameObject));
                        projSpeed *= 0.8f;
                    }

                    //Create a projectile at our current position
                    Vector3 offset = (Vector3.Normalize(this.obj.transform.forward) * 1.8f);
                    GameObject proj = Instantiate(base.projectile, this.obj.transform.position + offset, this.transform.rotation);

                    //Determine direction and velocity to shoot at
                    Vector3 dir = Vector3.Normalize(base.target.transform.position - proj.transform.position - offset);
                    proj.GetComponent<Projectile>().direction = dir;
                    proj.GetComponent<Projectile>().speed = projSpeed;
                    proj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                    this.obj.transform.RotateAround(this.obj.transform.position, v1, rot);
                }
            }

            cycle.Restart();
        }

        //Rotate towards player
        if (base.target != null)
            this.transform.LookAt(base.target.transform.position);
        this.transform.Rotate(0, 90, 0, Space.Self);

    }
}
