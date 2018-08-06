using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public GameObject target;
	public float cycleTime;
	public float projectileSpeed;
	protected Timer cycle;
	public GameObject projectile;
	protected List<GameObject> projectiles;

	public float health;

    public AudioClip shotNoise;
    public AudioClip hurtNoise;
    public AudioClip dieNoise;

	// Use this for initialization
	public virtual void Start () {
		cycle = new Timer (cycleTime);

		cycle.DelayedStart(3.0f);
		projectiles = new List<GameObject> ();
		health = 1f;

        shotNoise = Resources.Load("Sounds/projectileFire") as AudioClip;
        hurtNoise = Resources.Load("Sounds/enemy damage") as AudioClip;
        dieNoise = Resources.Load("Sounds/enemyDeath") as AudioClip;
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		this.cycle.Update ();

		//Should we shoot
		if (cycle.Complete ()) {
			//Shoot a projectile at the target
			if (target != null && projectile != null) {
				ShootProjectileAtTarget ();
			}

			cycle.Restart ();
		}

		//Rotate towards player
		this.transform.LookAt (target.transform.position);
		
	}

	protected virtual void ShootProjectileAtTarget(){
		if (target == null)
			return;
		ShootProjectileAtPosition (target.transform.position);
	}

	protected virtual void ShootProjectileAtPosition(Vector3 position){
		//Create a projectile at our current position
		GameObject proj = Instantiate(projectile, this.transform.position, this.transform.rotation);
        //Play shot noise at current position
        

        //Determine direction and velocity to shoot at
        Vector3 dir = Vector3.Normalize(position - this.transform.position);
		proj.GetComponent<Projectile>().direction = dir;
		proj.GetComponent<Projectile>().speed = projectileSpeed;

		projectiles.Add (proj);
	}

	public void Hit(float dmg){
		this.health -= dmg;

        MeshRenderer renderer = GetComponent<MeshRenderer>();
		StartCoroutine(this.FlashObject(renderer, renderer.material.GetColor("_Color"), Color.white, 0.2f, 0.1f));

        if (!this.IsAlive()) {
            Instantiate((GameObject)Resources.Load("Powers/Explosion", typeof(GameObject)), this.transform.position, this.transform.rotation);
            //Play death noise
            AudioSource.PlayClipAtPoint(dieNoise, this.transform.position);
        } else {
            //Play hurt noise
            AudioSource.PlayClipAtPoint(hurtNoise, this.transform.position);
        }
	}

	public bool IsAlive(){
		return health > 0;
	}

	protected virtual void OnTriggerEnter(Collider other) {
		if (other.tag == "sword" && other.GetComponent<Renderer>().enabled == true) {
			this.Hit (1f);
		}
	}
	
	public IEnumerator FlashObject(MeshRenderer toFlash, Color originalColor, Color flashColor, float flashTime, float flashSpeed)
	{
		 float flashingFor = 0f;
		 Color newColor = flashColor;
		 while(flashingFor < flashTime)
		 {
			  toFlash.material.SetColor("_Color", newColor);
			  flashingFor += Time.deltaTime;
			  yield return new WaitForSeconds(flashSpeed);
			  flashingFor += flashSpeed;
			  if(newColor == flashColor)
			  {
					newColor = originalColor;
			  }
			  else
			  {
					newColor = flashColor;
			  }
		 }
		 
		 toFlash.material.SetColor("_Color", originalColor);
		 
	}
}
