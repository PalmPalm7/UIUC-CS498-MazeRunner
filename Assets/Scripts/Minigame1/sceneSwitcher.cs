using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneSwitcher : MonoBehaviour {
    public float time;
    public AudioSource pyramidSound;
    public GameObject gController;

    private void Start() {
        time = 0;
    }
    // Update is called once per frame
    void Update () {
        if (betweenScenes.hasDefense && betweenScenes.hasOffense) {
            gController.GetComponent<potionGame>().explode = true;
            if (!pyramidSound.isPlaying) {
                pyramidSound.Play();
            }
            if (time < 5) {
                time += Time.deltaTime;
            } else {
                SceneManager.LoadScene("Minigame2");
            }
        }
    }
}
