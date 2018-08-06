using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPotions : MonoBehaviour {
    public GameObject liquid;
    public GameObject myLiquid;
    liquidColor colorScript;
    public bool beenFilled;
    public string myColor;
    public AudioSource clink;
    public AudioSource consume;

    public Vector3 startingPos;
    public Quaternion startingRot;
    // Use this for initialization
    void Start() {
        colorScript = liquid.GetComponent<liquidColor>();
        myLiquid = transform.GetChild(0).gameObject;
        startingPos = this.transform.position;
        startingRot = this.transform.rotation;
        beenFilled = false;
        myColor = "";
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "brewingPot" && collision.transform.name != "Shelf" && collision.transform.name != "Side table" && !(betweenScenes.hasDefense && betweenScenes.hasOffense)) {
            clink.time = 1.32f;
            clink.Play();
        }
        if (collision.transform.name == "Liquid" && !beenFilled) {
            if (colorScript.keepAccepting == false) {
                myLiquid.GetComponent<Renderer>().material = Resources.Load("Liquids/" + colorScript.currentcolor) as Material;
                myColor = colorScript.currentcolor;
                colorScript.resetField = true;
                beenFilled = true;
            } else {
                this.transform.position = startingPos;
                this.transform.rotation = startingRot;
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider trigger) {
        if (beenFilled) {
            if (myColor == "lightBlue") {
                if (!betweenScenes.hasDefense) {
                    betweenScenes.hasDefense = true;
                    betweenScenes.defenseChosen = "Shield";
                    this.gameObject.SetActive(false);
                    consume.Play();
                }

            } else if (myColor == "Purple") {
                if (!betweenScenes.hasDefense) {
                    this.enabled = false;
                    betweenScenes.hasDefense = true;
                    betweenScenes.defenseChosen = "Teleport";
                    this.gameObject.SetActive(false);
                    consume.Play();
                }
            } else if (myColor == "Peach") {
                if (!betweenScenes.hasOffense) {
                    this.enabled = false;
                    betweenScenes.hasOffense = true;
                    betweenScenes.offenseChosen = "Sword";
                    this.gameObject.SetActive(false);
                    consume.Play();
                }
            } else if (myColor == "Yellow") {
                if (!betweenScenes.hasOffense) {
                    this.enabled = false;
                    betweenScenes.hasOffense = true;
                    betweenScenes.offenseChosen = "FireballPower";
                    this.gameObject.SetActive(false);
                    consume.Play();
                }
            }
        }
    }
}
