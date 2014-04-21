using UnityEngine;
using System.Collections;

public class SteeringAttributes : MonoBehaviour {

	//these are common attributes required for steering calculations 
	public float maxSpeed = 12.0f;
	public float maxForce = 12.0f;
	public float mass = 1.0f;
	public float radius = 1.0f;

	//weights that can be edited
	public float seekWt = 20.0f;
}
