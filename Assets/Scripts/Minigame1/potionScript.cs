using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionScript : MonoBehaviour {
    public string myColor;
    public Vector3 startingPos;
    public Quaternion startingRotation;
    public Vector3 originalSize;
    public int reset;
    public GameObject gameController;
    public potionGame controlScript;
    public Collider myCollider;
    public AudioSource clink;

    private void Start() {
        myColor = this.gameObject.name;
        startingPos = this.transform.position;
        gameController = this.transform.parent.gameObject;
        controlScript = gameController.GetComponent<potionGame>();
        startingRotation = this.transform.rotation;
        originalSize = this.transform.localScale;
        myCollider = this.GetComponent<Collider>();
    }

    private void Update() {
        reset = controlScript.reset;
        if (reset == 1) {
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().transform.parent = null;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            if(this.transform.position != startingPos){
                this.transform.position = startingPos;
            }
            if (this.transform.rotation != startingRotation) {
                this.transform.rotation = startingRotation;
            }
            if (this.transform.localScale != originalSize) {
                this.transform.localScale = originalSize;
            }
            if (myCollider.enabled == false) {
                myCollider.enabled = true;
            }
        }

        if (controlScript.explode) {
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.transform.parent = null;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(5.0f, 5.0f, 5.0f));
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "brewingPot" && collision.transform.name != "Shelf" && collision.transform.name != "Side table" && !(betweenScenes.hasDefense && betweenScenes.hasOffense)) {
            clink.time = 1.32f;
            clink.Play();
        }
        
        if (collision.gameObject.tag == "brewingPot") {
            //"disable" When the potion touches the pot
            this.transform.localScale = new Vector3(0, 0, 0);
            myCollider.enabled = false;
        }
    }
}
