using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class betweenScenes{
    //remember to use startScript in the first scene to initialize

    //Will keep track of whether or not we shuld move the player into the potion stage
    public static bool tutorialOver;
    public static bool hasDefense;
    public static bool hasOffense;
    //After chosen, defenseChosen will either be "shield" or "teleport" no caps
    public static string defenseChosen;
    //After chosen, offsenceChosen will either be "sword" or "fireball" no caps
    public static string offenseChosen;

}
