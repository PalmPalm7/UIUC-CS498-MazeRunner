using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionGame : MonoBehaviour {

    public int reset;
    private float timer;
    public GameObject theFloor;
    public bool offensive;
    public bool defensive;
    public bool explode;
    // Use this for initialization
    void Start() {
        reset = 0;
        timer = 0;
        offensive = false;
        defensive = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Once reset is called, it we will be resetting for a whole second
        if (reset == 1){
            timer += Time.deltaTime;
            if (timer > 0.2) {
                reset = 0;
                timer = 0;
            }
            
        }
        if (explode){
            foreach (Transform child in this.transform) {
                if (child.transform.name != "particles1" && child.transform.name != "particles2" && child.transform.name != "EventSystem") {
                    if (child.transform.name == "Pot") {
                        child.gameObject.AddComponent<Rigidbody>();
                    }
                    Rigidbody childRigid = child.GetComponent<Rigidbody>();
                    childRigid.isKinematic = false;

                    Vector3 explodeForce = new Vector3(Random.Range(10.0f, 50.0f), Random.Range(10.0f, 50.0f), Random.Range(10.0f, 50.0f));
                    childRigid.AddExplosionForce(18.0f, this.transform.position, 500.0f);
                    //childRigid.AddForce(explodeForce);
                }
            }
            explode = false;

        }

        if (betweenScenes.hasDefense && betweenScenes.hasOffense) {
            theFloor.transform.localPosition -= new Vector3(0.0f, 0.01f, 0.0f);
        }
	}
}
