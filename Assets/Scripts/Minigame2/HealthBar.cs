using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public GameObject player;
    public GameObject ground;
    LineRenderer lr;
    int totalCount = 50;

    // Use this for initialization
    void Start () {
        lr = this.GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = totalCount + 1;
	}
	
	// Update is called once per frame
	void Update () {
        float x;
        float y = ground.transform.lossyScale.y / 2 + 0.05f ;
        float z;

        lr.positionCount = (int)((totalCount) * Mathf.Max(0f, player.GetComponent<Player>().health / 25f)) + 1;

        float angle = 20f;

        for (int i = 0; i < (lr.positionCount); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * 0.5f + this.player.transform.position.x;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * 0.5f + this.player.transform.position.z;

            lr.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / totalCount);
        }


    }
}
