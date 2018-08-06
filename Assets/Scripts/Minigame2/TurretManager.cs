using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TurretManager : MonoBehaviour {

	public List<GameObject> turrets;
	public GameObject target;
	public  Dictionary<String, GameObject> turret_types;
    public AudioClip turretSpawn;

	// Use this for initialization
	void Start () {
		turrets = new List<GameObject> ();

		//Basic turret type
		GameObject basicTurret = (GameObject)Resources.Load ("Turrets/BasicTurret", typeof(GameObject));
		GameObject spiralTurret = (GameObject)Resources.Load ("Turrets/SpiralTurret", typeof(GameObject));
        GameObject trackingTurret = (GameObject)Resources.Load("Turrets/TrackingTurret", typeof(GameObject));
        GameObject bossTurret = (GameObject)Resources.Load("Turrets/BossTurret", typeof(GameObject));

        turret_types = new Dictionary<String, GameObject>();

		//Update dictionary mappings
		turret_types["basic"] = basicTurret;
		turret_types["spiral"] = spiralTurret;
        turret_types["tracking"] = trackingTurret;
        turret_types["boss"] = bossTurret;
    }
	
	// Update is called once per frame
	void Update () {
		//Disable dead turrets
		for(int i = 0; i < turrets.Count; i++){
			if(!turrets[i].GetComponent<Turret>().IsAlive()){
				turrets[i].SetActive(false);
			}
		}
	}

	/**
	 * Places the given turret type at the given position
	 **/
	public void PlaceTurret(GameObject turret, Vector3 position){
		//Create a turret
		GameObject turr = Instantiate (turret, position, Quaternion.identity);

        //Play the spawn sound at this location
        AudioSource.PlayClipAtPoint(turretSpawn, position);

        //Give the turrets a target to follow
		turr.GetComponent<Turret>().target = this.target;
		turr.GetComponent<Turret>().cycleTime = 1.0f;
		turr.GetComponent<Turret>().projectileSpeed = 6f;
		turr.SetActive(true);
		turr.GetComponent<Turret>().Start();
		//print(turr.GetComponent<Turret>().health);
		//Add turret to list
		turrets.Add (turr);
	}

	public int NumAlive(){
		int total = 0;
		for (int i = 0; i < turrets.Count; i++){
			if(turrets[i].GetComponent<Turret>().IsAlive()){
				total ++;
			}
		}
		return total;
	}

    public void ClearSpawned()
    {
        this.turrets.Clear();
    }

    public IEnumerator KillAll() {
        float time = 0f;
        while (time < 0.1f) {
            time += Time.deltaTime * Time.timeScale;
            yield return new WaitForSeconds(0.1f);

        }

        for (int i = 0; i < turrets.Count; i++) {
            turrets[i].GetComponent<Turret>().Hit(10000f);
        }
    }
}
