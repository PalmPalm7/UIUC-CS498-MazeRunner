using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playPenscript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (betweenScenes.hasDefense && betweenScenes.hasOffense) {
            /*
            foreach (Transform child in this.transform) {
                Collider myCollider = child.gameObject.GetComponent<Collider>();
                if (myCollider.enabled) {
                    myCollider.enabled = !myCollider.enabled;
                }
            }
            */
            this.enabled = false;
        }
	}
}
