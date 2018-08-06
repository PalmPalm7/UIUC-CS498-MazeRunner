using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    // Use this for initialization
    public float health;
    public GameObject centerEye;
    public GameObject overlay;
    public GameObject words;
    public GameObject winText;
    public GameObject ground;
    public bool won;

    private GameObject player;
    private List<float> deltaRXs;
    private List<float> deltaLXs;
    private bool turned;
    private Timer turnCD;
	void Start () {
        this.health = 25f;
		if(overlay != null)
			overlay.SetActive(false);
        won = false;

        player = GameObject.Find("Player");
        deltaRXs = new List<float>();
        deltaLXs = new List<float>();

        turned = false;
        turnCD = new Timer(0.5f);
    }

    // Update is called once per frame

    private Vector3 pLPos;
    private Vector3 pRPos;
	void Update () {
        if (turned) {
            turnCD.Update();

            if (turnCD.Complete()) {
                turnCD.Restart();
                turnCD.Stop();

                turned = false;
            }
        }

        Vector3 rPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote);
        Vector3 lPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTrackedRemote);

        if (pLPos != null && !turned && (Input.GetAxis("TriggerR") <= 0.0f)) {
            Vector3 rVel = (rPos - pRPos) / Time.deltaTime;
            Vector3 lVel = (lPos - pLPos) / Time.deltaTime;

            float changeX = rVel.x + lVel.x;

            if (deltaLXs.Count < 15) {
                deltaLXs.Add(lVel.x);
                deltaRXs.Add(rVel.x);
            } else  {
                deltaLXs.RemoveAt(0);
                deltaLXs.Add(lVel.x);
                deltaRXs.RemoveAt(0);
                deltaRXs.Add(rVel.x);

                float ravg = 0f;

                foreach (float x in deltaRXs) {
                    ravg += x;
                }

                float lavg = 0f;

                foreach (float x in deltaLXs) {
                    lavg += x;
                }

                ravg /= 15;
                lavg /= 15;

                if (lavg > 0.8f) {
                    //Rotate right
                    //player.transform.Rotate(0f, 90f, 0f);
                    StartCoroutine(this.Turn(1f));
                    deltaRXs.Clear();
                    deltaLXs.Clear();
                    turned = true;
                    turnCD.Restart();
                    turnCD.Start();
                } else if (ravg < -0.8f) {
                    //rotate left
                    //player.transform.Rotate(0f, -90f, 0f);
                    StartCoroutine(this.Turn(-1f));
                    deltaRXs.Clear();
                    deltaLXs.Clear();
                    turned = true;
                    turnCD.Restart();
                    turnCD.Start();
                } 
            }


        }


        pLPos = lPos;
        pRPos = rPos;

    }

    public IEnumerator Turn(float turn) {
      

        float time = 0f;
        while (time < 0.1f) {
            player.transform.Rotate(0f,60f * (0.01f/ 0.1f) * turn, 0f);

            time += Time.deltaTime * Time.timeScale;
            yield return new WaitForSeconds(0.01f);

        }

    }

    public void Hit(float damage)
    {
        if (won)
            return;

        //Update player health
        this.health -= damage;

        //Play hurt noise
        if (this.IsAlive()) {
            GameObject.Find("otherSounds").transform.Find("playerHurt").GetComponent<AudioSource>().Play();
        }


        if (!this.IsAlive())
            return;

        //Flash screen red
        StartCoroutine(this.FadeOut());

        //Vibrate
        byte[] noise = { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255};
        OVRHaptics.Channels[0].Preempt(new OVRHapticsClip(noise, noise.Length));
        OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(noise, noise.Length));
        OVRHaptics.Process();
    }

    public Vector3 getPosition()
    {
        return centerEye.transform.position;
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }

    public IEnumerator FadeOut()
    {
        overlay.SetActive(true);
        overlay.GetComponent<Image>().color = new Color(1.0f, 0f, 0f, 0.5f);

        float time = 0f;
        while (time < 0.15f)
        {
            time += Time.deltaTime * Time.timeScale;
            overlay.GetComponent<Image>().color = new Color(1.0f, 0f, 0f, 0.5f * (1 - (time / 0.15f)));
            yield return new WaitForEndOfFrame();
            
        }

        overlay.SetActive(false);

    }

    public IEnumerator FadeToBlack()
    {
        overlay.SetActive(true);
        overlay.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);

        //Play loss theme
        AudioSource lossTheme = GameObject.Find("bgMusic").transform.Find("lossTheme").GetComponent<AudioSource>();
        lossTheme.Play();

        float time = 0f;
        while (time < 0.3f)
        {
            overlay.SetActive(true);
            time += Time.deltaTime * Time.timeScale;
            overlay.GetComponent<Image>().color = new Color(0f, 0f, 0f, (time / 0.3f));
            yield return new WaitForEndOfFrame();

        }
        words.SetActive(true);
        //overlay.SetActive(false);

    }

    public IEnumerator WinAnimation() {
        float timer = 0f;
        float collapseTimer = 3.5f;
        
        //Play victory theme
		GameObject.Find("bgMusic").transform.Find("verge").GetComponent<AudioSource>().Stop();
		GameObject.Find("bgMusic").transform.Find("moldugaBattle").GetComponent<AudioSource>().Stop();
		GameObject.Find("bgMusic").transform.Find("lossTheme").GetComponent<AudioSource>().Stop();
        AudioSource victoryTheme = GameObject.Find("bgMusic").transform.Find("winTheme").GetComponent<AudioSource>();
        victoryTheme.Play();

        while (true) {
            timer += Time.deltaTime;
            float scaleFactor = (15.0f * (Time.deltaTime / collapseTimer));

            this.ground.transform.localPosition -= new Vector3(0.0f, scaleFactor, 0.0f);

            if (timer >= collapseTimer) {
                ground.SetActive(false);
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }

        overlay.SetActive(true);
        overlay.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);

        float time = 0f;
        while (time < 0.3f) {
            overlay.SetActive(true);
            time += Time.deltaTime * Time.timeScale;
            overlay.GetComponent<Image>().color = new Color(0f, 0f, 0f, (time / 0.3f));
            yield return new WaitForEndOfFrame();
        }
        winText.SetActive(true);
        //overlay.SetActive(false);

    }


}
