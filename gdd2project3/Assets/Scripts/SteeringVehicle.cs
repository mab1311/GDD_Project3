﻿using UnityEngine;
using System.Collections;

public class SteeringVehicle : MonoBehaviour {
	//movement variables - exposed in inspector panel

	//the target for vehicle to seek/pursue
	private GameObject target  = null;
	
	public float gravity = 20.0f; // keep us grounded
	
	// Each vehicle contains a CharacterController which helps to deal with
	// the relationship between movement initiated by the character and the forces
	// generated by contact with the terrain & other game objects.
	private CharacterController characterController;
	
	// the SteeringAttributes holds several variables needed for steering
	private SteeringAttributes attr;
	
	// the Steer component implements the basic steering functions
	private Steer steer;
	
	private Vector3 acceleration;	//change in velocity per second
	private Vector3 velocity;		//change in position per second
	public Vector3 Velocity {
		get { return velocity; }
		set { velocity = value;}
	}
	
	public GameObject Target {
		get { return target; }
		set { target = value;}
	}



	// Use this for initialization
	void Start () {
		acceleration = Vector3.zero;
		velocity = transform.forward;
		
		//get component references
		characterController = gameObject.GetComponent<CharacterController> ();
		steer = gameObject.GetComponent<Steer> (); //gets reference to the steer
		GameObject main = GameObject.Find("MainGO");
		attr = main.GetComponent<SteeringAttributes> ();
	}
	
	// Update is called once per frame
	void Update () {
		CalcSteeringForce ();
		
		//update velocity
		//velocity = Vector3.zero;
		velocity += acceleration * Time.deltaTime;
		velocity.y = 0;	// we are staying in the x/z plane
		velocity = Vector3.ClampMagnitude (velocity, attr.maxSpeed); //similar to limit
		
		//orient the transform to face where we going
		if (velocity != Vector3.zero)
			transform.forward = velocity.normalized;
		
		// keep us grounded
		velocity.y -= gravity * Time.deltaTime;
		
		// the CharacterController moves us subject to physical constraints
		characterController.Move (velocity * Time.deltaTime);
		
		//reset acceleration for next cycle
		acceleration = Vector3.zero;
	}

	//calculate and apply steering forces
	private void CalcSteeringForce ()
	{ 
		Vector3 force = Vector3.zero;
		
		//obstacles
		/*
		for (int i=0; i<obstacles.Length; i++)
		{	
			force += attr.avoidWt * steer.AvoidObstacle (obstacles[i], attr.avoidDist);
		}
		Debug.DrawRay (transform.position, force, Color.cyan);
		*/
		
		//seek target
		force += attr.seekWt * steer.Seek (target.transform.position);
		
		force = Vector3.ClampMagnitude (force, attr.maxForce);
		ApplyForce(force);
	}

	private void ApplyForce (Vector3 steeringForce)
	{
		acceleration += steeringForce/attr.mass;
	}
}
