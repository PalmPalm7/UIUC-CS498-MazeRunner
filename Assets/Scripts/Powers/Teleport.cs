using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

    public GameObject player;
    public bool showDest;
    public Timer cooldown;
    public Vector3 destination;
    private GameObject teleportIndicator;
    public Timer triggerTime;
	public string trigger;
    private RaycastHit hit;
    private LineRenderer lr;
    public GameObject overlay;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
		
		teleportIndicator = Instantiate((GameObject)Resources.Load ("Powers/Teleport", typeof(GameObject)), this.transform.position, Quaternion.identity);
        cooldown = new Timer(2f)
        {
            currentTime = 6f
        };

        triggerTime = new Timer(1f);
        triggerTime.Start();

        cooldown.Start();
        showDest = true;
        teleportIndicator.SetActive(false);

        trigger = "TriggerL";

        this.gameObject.AddComponent<LineRenderer>();
        lr = this.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = new Color(0f, 0f, 0f, 0.5f);
        lr.endColor = new Color(0f, 0f, 0f, 0.5f);
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);

        overlay = GameObject.Find("Transition");
        overlay.SetActive(false);
    }

    // Update is called once per frame
    
    void Update () {
        cooldown.Update();

        if (showDest && cooldown.Complete())
        {
            Vector3 start = this.transform.position;
            Vector3 end = start + (this.transform.forward) * (100f);
       
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 100f, LayerMask.GetMask("Ground")))
            {

                if(hit.collider.tag == "ground")
                {
                    lr.SetPosition(0, start);
                    lr.SetPosition(1, end);

                    Vector3 pos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    teleportIndicator.transform.position = pos;
                    destination = teleportIndicator.transform.position;

                    teleportIndicator.SetActive(true);
                    triggerTime.Update();
                }
            }
        }

        if (Input.GetAxis(trigger) > 0.0f)
        {
            this.showDest = true;
            
            if (triggerTime.Complete())
            {
                this.TeleportPlayer();
            }
        }
        else
        {
			this.showDest = false;
            teleportIndicator.SetActive(false);
            this.triggerTime.Restart();
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }

    }


    public IEnumerator FadeToBlack() {
        overlay.SetActive(true);
        overlay.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);

        float time = 0f;
        while (time < 0.02f) {
            overlay.SetActive(true);
            time += Time.deltaTime * Time.timeScale;
            overlay.GetComponent<Image>().color = new Color(0f, 0f, 0f, (0.4f * time / 0.02f));
            yield return new WaitForSeconds(0.01f);

        }

        time = 0f;
        while (time < 0.02f) {
            overlay.SetActive(true);
            time += Time.deltaTime * Time.timeScale;
            overlay.GetComponent<Image>().color = new Color(0f, 0f, 0f, (0.4f * (1.0f - time / 0.02f)));
            yield return new WaitForSeconds(0.01f);

        }
        overlay.SetActive(false);
    }


    public void toggleDisplayDest()
    {
        this.showDest = !this.showDest;
    }

    public void TeleportPlayer()
    {
        if (!cooldown.Complete())
        {
            return;
        }

        StartCoroutine(this.FadeToBlack());

        player.transform.position = destination;

        //Play teleport noise
        GameObject.Find("otherSounds").transform.Find("teleport").GetComponent<AudioSource>().Play();

        this.showDest = false;
        teleportIndicator.SetActive(false);
        LineRenderer lr = this.GetComponent<LineRenderer>();
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);

        cooldown.Restart();
        cooldown.Start();
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.05f, 0.05f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

}
