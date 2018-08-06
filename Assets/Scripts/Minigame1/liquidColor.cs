using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liquidColor : MonoBehaviour {
    public GameObject gController;
    potionGame controlScript;
    public string currentcolor;
    public string previousColor;
    public bool keepAccepting;
    public bool resetField;
    public GameObject child;
    public AudioSource splash;
    public AudioSource potionGood;
    public AudioSource potionBad;
    public AudioSource drone;

    // Use this for initialization
    void Start() {
        controlScript = gController.GetComponent<potionGame>();
        currentcolor = "Clear";
        previousColor = "Clear";
        GetComponent<Renderer>().material = Resources.Load("Liquids/Clear") as Material;
        keepAccepting = true;
        resetField = false;
    }
    private void Update() {
        if (resetField) {
            resetFieldFunc();
        }
    }

    private void OnCollisionEnter(Collision collision) {

        if (keepAccepting == true) {
            previousColor = currentcolor;
            currentcolor = collision.transform.name;

            if (currentcolor == "Red" && (previousColor == "Clear" || previousColor == "Green" || previousColor == "Blue")) {
                //Debug.Log("Handling red");
                //Red only loads if previous color was clear, blue, or green
                if (previousColor == "Clear") {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Red") as Material;
                    splash.Play();
                } else if (previousColor == "Blue") {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Purple") as Material;
                    currentcolor = "Purple";
                    keepAccepting = false;
                    splash.Play();

                } else {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Yellow") as Material;
                    currentcolor = "Yellow";
                    keepAccepting = false;
                    splash.Play();

                }
            } else if (currentcolor == "Green" && (previousColor == "Clear" || previousColor == "Red")) {
                //Green only loads if previous color was clear or red
                //Debug.Log("Handling Green");
                if (previousColor == "Clear") {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Green") as Material;
                    splash.Play();
                } else {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Yellow") as Material;
                    currentcolor = "Yellow";
                    keepAccepting = false;
                    splash.Play();
                }
            } else if (currentcolor == "Blue" && (previousColor == "Clear" || previousColor == "Red" || previousColor == "White")) {
                //Debug.Log("Handling Blue");
                //Blue only loads if previous color was clear, red, or white
                if (previousColor == "Clear") {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Blue") as Material;
                    splash.Play();
                } else if (previousColor == "White") {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/lightBlue") as Material;
                    currentcolor = "lightBlue";
                    keepAccepting = false;
                    splash.Play();
                } else {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Purple") as Material;
                    currentcolor = "Purple";
                    keepAccepting = false;
                    splash.Play();

                }
            } else if (currentcolor == "White" && (previousColor == "Clear" || previousColor == "Orange" || previousColor == "Blue")) {
                //White only loads if previous color was clear, orange, or blue
                //Debug.Log("Handling White");
                if (previousColor == "Clear") {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/White") as Material;
                    splash.Play();
                } else if (previousColor == "Orange") {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Peach") as Material;
                    currentcolor = "Peach";
                    keepAccepting = false;
                    splash.Play();

                } else {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/lightBlue") as Material;
                    currentcolor = "lightBlue";
                    keepAccepting = false;
                    splash.Play();
                }
            } else if (currentcolor == "Orange" && (previousColor == "Clear" || previousColor == "White")) {
                //Orange only loads if previous color was clear or white
                //Debug.Log("Handling Orange");
                if (previousColor == "Clear") {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Orange") as Material;
                    splash.Play();
                } else {
                    GetComponent<Renderer>().material = Resources.Load("Liquids/Peach") as Material;
                    currentcolor = "Peach";
                    keepAccepting = false;
                    splash.Play();
                }
            } else {
                //Not a valid color combination
                controlScript.reset = 1;
                currentcolor = "Clear";
                previousColor = "Clear";
                GetComponent<Renderer>().material = Resources.Load("Liquids/Clear") as Material;
                resetField = true;
                potionBad.Play();
            }
        }
        if (!keepAccepting){
            if (!drone.isPlaying) {
                drone.Play();
            }
            //Reach here when we need to actually create a potion
            if(collision.transform.name == "CenterEyeAnchor") {
                if (drone.isPlaying) {
                    drone.Stop();
                }
                currentcolor = "Clear";
                previousColor = "Clear";
                GetComponent<Renderer>().material = Resources.Load("Liquids/Clear") as Material;
                keepAccepting = true;
                resetField = false;
                
            }
        }
    }
    void resetFieldFunc() {
        //When called, reset
        controlScript.reset = 1;
        if (currentcolor != "Peach" && currentcolor != "lightBlue" && currentcolor != "Yellow" && currentcolor != "Purple") {
            child.GetComponent<smokeScript>().playSmoke = true;
        } else {
            potionGood.Play();
        }
        if (drone.isPlaying) {
            drone.Stop();
        }
        currentcolor = "Clear";
        previousColor = "Clear";
        GetComponent<Renderer>().material = Resources.Load("Liquids/Clear") as Material;
        keepAccepting = true;
        
        resetField = false;
    }
}



//controlScript.reset = 1;

/*
Yellow - Red + Green
Purple - Red + Blue
Peach - White + Orange
Light blue - White + Blue
*/







