using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialPointer : MonoBehaviour {
    public string buttonName;
    public bool haveClicked;
    public AudioSource ping;
    public tutorialScreen tutScreen;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        Vector3 start = this.transform.position;
        Vector3 end = start + (this.transform.forward) * 5f;
        DrawLine(start, end, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 100f)){
            if (Input.GetAxis(buttonName) > 0.95 && !haveClicked) {
                if (hit.collider.gameObject.transform.name == "CONTINUE") {
                    tutScreen.state++;
                    ping.Play();
                } else if (hit.collider.gameObject.transform.name == "SKIP") {
                    tutScreen.state = 7;
                    ping.Play();
                }
                haveClicked = true;
            }
        }
        if (Input.GetAxis(buttonName) < 0.10 && haveClicked) {
            haveClicked = false;
        }
	}

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.02f) {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
