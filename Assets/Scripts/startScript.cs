using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScript : MonoBehaviour {
    //Somehow run this script when the game begins. It initializes the global static variables.
    public bool def;
    public bool att;
    public string wep;
    public string ofh; 

    void Start() {
        betweenScenes.hasDefense = false;
        betweenScenes.hasOffense = false;
        betweenScenes.defenseChosen = "none";
        betweenScenes.offenseChosen = "none";
        betweenScenes.tutorialOver = false;
    }
    /*
    //DEBUG: Using to make sure the static variables are updating correctly
    private void Update() {
        def = betweenScenes.hasDefense;
        att = betweenScenes.hasOffense;
        wep = betweenScenes.offenseChosen;
        ofh = betweenScenes.defenseChosen;
    }
    */
}
