using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {



    private SteamVR_TrackedObject trackedObj;
	public bool isGrabbing;
    private GameObject collidingObject; //Stores the GameObject that the trigger is colliding with, so you can grab it
    private GameObject objectInHand; //Reference to the GameObject the player is currently grabbings

	private Matrix4x4 handToObject;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
		isGrabbing = false;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    //This method accepts a collider as a parameter and uses its GameObject as the collidingObject for grabbing and releasing
    private void SetCollidingObject(Collider col)
    {
        //Doesn't make the GameObject a potential grab target if the player is already holding something or the object has no rigid body
        //Assigns the object as a potential grab target

        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

		//GET OBJECT CLOSESET TO THE CENTER
        collidingObject = col.gameObject;
    }

    //When the tirgger collider enters another, this sets up the other collider as a potential grab target
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    //Similar to OnTriggerEnter, but it ensures that the target is set when the player holds a controller over an object for a while
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    //When the collider exits an object, abandoning an ungrabbed target, this code removes its target by setting it to null
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }


		//dictionary.remove(other.id);

        collidingObject = null;
    }

    private void GrabObject()
    {
        //1 Move the GameObject inside the player's hand and remove it from the collidingObject variable
        objectInHand = collidingObject;

	

		//handToObject = objectInHand.transform.worldToLocalMatrix * this.transform.localToWorldMatrix;

        collidingObject = null;

        //2 Add a new joint that connects the controller to the object using the AddFixedJoint method
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

    }

    private FixedJoint AddFixedJoint()
    {
        //3 Make a new fixed joint, add it to the controller, and then set it up so that it doesn't break easily
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 2000000;
        fx.breakTorque = 2000000;
        return fx;
    }

    private void ReleaseObject()
    {
        //1 Make sure there's a fixed joint attached to the controller
        if(GetComponent<FixedJoint>())
        {
            //2 Remove the connection to the object held by the joint and destroy the joint
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            //3 Add the speed and rotation of the controller when the player releases the object, so the result is a realistic arc
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        //4 Remove the reference to the formerly attached object
        objectInHand = null;

    }

    // Update is called once per frame
    void Update () {





		//1 When the player squeezes the trigger and there's a potential grab target, this grabs it
		//AND not already grabbing something
		if(Controller.GetHairTriggerDown())
        {

			//if grabbing an inserted pin or knife release it
			if (collidingObject) {
				if (collidingObject.tag == "pin_inserted" || collidingObject.tag == "knife_inserted") {
					ReleaseObject ();
				}


				//MIRROR RELEASE CODE
				//IF colliding object is the mirror
				/*if (collidingObject.tag == "mirror")
					collidingObject.GetComponent<Collider> ().isTrigger = true; */
			}
				


			//check if its not an inserted pin or knife
			if(collidingObject && (collidingObject.tag != "pin_inserted" && collidingObject.tag != "knife_inserted" && collidingObject.tag != "mirrorFlying"))
			{
				//reOrient knife
				if (collidingObject.tag == "knife_main") {
					collidingObject.transform.rotation = Quaternion.Euler (-40.439f, 0.371f, -90.48601f);
				} 
					

                GrabObject();
				isGrabbing = true;
            }
        }

        //2 If the player erleases the trigger and there's an object attached to the controller, this releases it
        if(Controller.GetHairTriggerUp())
        {
            if(objectInHand)
            {
                ReleaseObject();
				isGrabbing = false;
            }
        }

		//Prevent crazy flinging stuff
		if (objectInHand != null) {


			if (objectInHand.tag != "staff" && objectInHand.tag != "mallet" && objectInHand.tag != "mirror") {
				if (Vector3.Distance (objectInHand.transform.position, transform.position) > 0.3)
					ReleaseObject (); 
			}

			//if(objectInHand != null)
				//objectInHand.transform.position = this.transform.localToWorldMatrix.MultiplyPoint (handToObject.MultiplyPoint (Vector3.zero));


		} 

		//make sure isGrabbing is off when not holding on to anything
		if (GetComponent<FixedJoint> () == null)
			isGrabbing = false;

	}
}
