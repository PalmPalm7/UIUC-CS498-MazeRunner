using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPower : MonoBehaviour {

    public GameObject player;
    public Timer cooldown;
    private GameObject fireball;
    public Timer triggerTime;
    public string trigger;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        
        cooldown = new Timer(2f)
        {
            currentTime = 6f
        };

        cooldown.Start();

        triggerTime = new Timer(1f);
        triggerTime.Start();

		trigger = "TriggerR";
    }

    // Update is called once per frame
    void Update()
    {
        cooldown.Update();

        

        if (Input.GetAxis(trigger) > 0.0f)
        {
            triggerTime.Update();

            //If we are off CD and trigger timer is met
            if(triggerTime.Complete() && cooldown.Complete())
            {
                //Shoot the fireball forward
                GameObject fireball = Instantiate((GameObject)Resources.Load("Powers/Fireball", typeof(GameObject)), this.transform.position, Quaternion.identity);
                fireball.GetComponent<Fireball>().Start();
                fireball.SetActive(true);
                fireball.GetComponent<Fireball>().direction = Vector3.Normalize(this.transform.forward) * 5.0f;

                //Reset the corresponding timers
                triggerTime.Restart();
                cooldown.Restart();
            }
        }
        else
        {
            //Reset the trigger timer
            triggerTime.Restart();
        }
 
        

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
