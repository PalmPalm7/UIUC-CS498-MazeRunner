using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabScript : MonoBehaviour {
    private bool grabbing;
    private GameObject grabbedObject;
    public float grabRadius;
    public LayerMask grabMask;
    public OVRInput.Controller controller;
    public string buttonName;
    private Vector3 lastPosition; 
    // Update is called once per frame

    void GrabObject(){
        grabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0){
            //We've hit something, let's grab the object
            int closestObject = 0;
            for (int i = 0; i < hits.Length; i++){
                if (hits[i].distance > hits[closestObject].distance){
                    closestObject = i;
                }
            }

            //now hits[closestObject] is the closest obj in our spherecast, this is what we want to grab
            //Store into our grabbedobject
            grabbedObject = hits[closestObject].transform.gameObject;
            //Set object as kinematic
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            //And move its position into our hands
            grabbedObject.transform.position = transform.position;
            //And make it follow us by becoming its parent
            grabbedObject.transform.parent = transform;

        }
    }

    void DropObject(){
        grabbing = false;

        //First, check that we actually have an object grabbed
        if (grabbedObject != null){
            //Undo what we've done for the grabbing
            
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            /*
            Vector3 velocity = OVRInput.GetLocalControllerVelocity(controller);
            velocity = Vector3.RotateTowards(velocity, Player.transform.forward, 0f, 0f);
            grabbedObject.GetComponent<Rigidbody>().velocity = velocity;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
            grabbedObject = null;
            */
            grabbedObject.GetComponent<Rigidbody>().velocity = (this.transform.position - lastPosition) / Time.deltaTime;
            //print(grabbedObject.GetComponent<Rigidbody>().velocity
        }
    }
	void Update () {
        if (!grabbing && Input.GetAxis(buttonName) > 0.90){
            GrabObject();
        }
        if (grabbing && Input.GetAxis(buttonName) < 0.20){
            DropObject();
        }
        lastPosition = this.transform.position;
    }
}
