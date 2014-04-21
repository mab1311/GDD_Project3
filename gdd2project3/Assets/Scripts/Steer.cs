using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class Steer : MonoBehaviour {

	Vector3 dv = Vector3.zero; 	// desired velocity, used in calculations
	SteeringAttributes attr; 	// attr holds several variables needed for steering calculations
	CharacterController characterController;

	// Use this for initialization
	void Start () {
		GameObject main = GameObject.Find("MainGO");
		characterController = gameObject.GetComponent<CharacterController> ();	
		attr = main.GetComponent<SteeringAttributes> ();
	}
	
	///
	/// functions that will return steering forces for SteeringVehicle
	/// 

	//Seeks the target
	public Vector3 Seek (Vector3 targetPos)
	{
		//find dv, desired velocity
		dv = targetPos - transform.position;		
		dv = dv.normalized * attr.maxSpeed; 	//scale by maxSpeed
		dv -= characterController.velocity;
		dv.y = 0;								// only steer in the x/z plane
		return dv;
	}

	//uncomment if we will use obstacles for enemy to avoid
	/*
	public Vector3 AvoidObstacle (GameObject obst, float safeDistance)
	{ 
		dv = Vector3.zero;
		float obRadius = obst.GetComponent<ObstacleScript> ().Radius;
		
		//vector from vehicle to center of obstacle
		Vector3 vecToCenter = obst.transform.position - transform.position;
		//eliminate y component so we have a 2D vector in the x, z plane
		vecToCenter.y = 0;
		float dist = vecToCenter.magnitude;
		
		// if too far to worry about, out of here
		if (dist > safeDistance + obRadius + attr.radius)
			return Vector3.zero;
		
		//if behind us, out of here
		if (Vector3.Dot (vecToCenter, transform.forward) < 0)
			return Vector3.zero;
		
		float rightDotVTC = Vector3.Dot (vecToCenter, transform.right);
		
		//if we can pass safely, out of here
		if (Mathf.Abs (rightDotVTC) > attr.radius + obRadius)
			return Vector3.zero;
		
		//obstacle on right so we steer to left
		if (rightDotVTC > 0)
			dv += transform.right * -attr.maxSpeed * safeDistance / dist;
		else
			//obstacle on left so we steer to right
			dv += transform.right * attr.maxSpeed * safeDistance / dist;
		
		return dv;	
	}
	*/

}
