using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidCollapse : MonoBehaviour {

	public int lvlCollapse;
	public float timer;
	public float collapseTimer;
	public GameObject level;


	// Use this for initialization
	void Start () {
		lvlCollapse = -1;
		timer = 0.0f;
		collapseTimer = 5.0f;

		CollapseLevel (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (lvlCollapse != -1) {
			timer += Time.deltaTime;
			float scaleFactor = (15.0f * (Time.deltaTime / collapseTimer));

			this.level.transform.localPosition -= new Vector3(0.0f, scaleFactor, 0.0f);

			if (timer >= collapseTimer) {
				int nextLevel = lvlCollapse + 1;
				this.lvlCollapse = -1;
				timer = 0.0f;
				this.level.SetActive (false);
				if (nextLevel < 4) {
					this.CollapseLevel (nextLevel);
				}
			}
		}
	}

	void CollapseLevel(int lvl){
		//Only start collapse if no other floor is active
		if (lvlCollapse != -1)
			return;

		try{
			this.level = GameObject.Find ("lvl" + lvl.ToString ());
		}
		catch{
			lvlCollapse = -1;
		}

		// Get the gameobject
		this.lvlCollapse = lvl;

		//Reset timers
		this.timer = 0.0f;
	}

}
