using UnityEngine;
using System.Collections;
using System;

public class Racket : MonoBehaviour {
	//movement vars
	private static float positionZ;
	private float speed = 0.1f;
	static string currentButton = "";
	private Transform originalTransform;
	private float[] axisInput = new float[7];

	// here are the keys that we will set dynamically later
	public KeyCode key_up;
	public KeyCode key_down;
	public KeyCode joy_up;
	public KeyCode joy_down;

	//booleans
	private static bool mousePressed;


	// Use this for initialization
	void Start () {

		for(int i = 0; i < axisInput.Length; i ++)
			axisInput[i] = 0.0f;
	}

	// Getters
	static public float getZ()
	{
		return positionZ;
	}

	static public string getCurrentButton()
	{
		return currentButton;
	}
	// setters
	static public void clearCurrentButton()
	{
		currentButton = "";
	}

	// Update is called once per frame
	void Update () {
		positionZ = transform.position.z;
		// Get the Gamepad Analog stick's axis data
		//axisInput[5] = Input.GetAxisRaw("Axis 6");
		axisInput[6] = Input.GetAxisRaw("Axis 7");
		axisInput[0] = Input.GetAxisRaw("LeftJoystick1");

		// Get the currently pressed Gamepad Button name
		var values = Enum.GetValues(typeof(KeyCode));
		for(int x = 0; x < values.Length; x++) {
			if(Input.GetKeyDown((KeyCode)values.GetValue(x))){
				currentButton = values.GetValue(x).ToString();
			}
		}
		//dpad
		//only move if you are within the stage
		if (transform.position.z > 0.3 && axisInput[6] < 0) {
			transform.Translate (0,0,axisInput[6] * speed);
		}
		else if (transform.position.z < 4.2 && axisInput[6] > 0) {
			transform.Translate (0,0,axisInput[6] * speed);
		}
		//joystick 
		if (transform.position.z > 0.3 && axisInput[0] > 0) {
			transform.Translate (0,0, -1 * axisInput[0] * speed);
		}
		else if (transform.position.z < 4.2 && axisInput[0] < 0) {
			transform.Translate (0,0, -1 * axisInput[0] * speed);
		}
		//transform.Rotate(axisInput[5],axisInput[5],axisInput[5]);
		if (Input.GetMouseButtonUp (0))
			mousePressed = false;
			
		if (Input.GetMouseButtonDown(0))
			mousePressed = true;

		if(mousePressed) {
			//Debug.Log (Input.mousePosition.y + " " + transform.position.z + " mouse gen " + Input.mousePosition.y);
			//if (transform.position.z < Input.mousePosition.y && transform.position.z < 4) {
			if (Input.mousePosition.y > 285 && transform.position.z < 4.2) {
				transform.position = new Vector3(transform.position.x, 
			                                 	transform.position.y, 
			                                 	transform.position.z + speed);
			//} else if (transform.position.z > Input.mousePosition.y && transform.position.z > 0.5) {
			} else if (Input.mousePosition.y < 285 && Input.mousePosition.y > 25 && transform.position.z > 0.3) {
				transform.position = new Vector3(transform.position.x, 
				                                 transform.position.y, 
				                                 transform.position.z - speed);
			}
		}
			//transform.Translate (0,0,Input.mousePosition.z);
	}

	void FixedUpdate() {
		// get the current position
		Vector3 pos = transform.position;
		if (Input.GetKey(key_up) && transform.position.z < 4) 
		{
			// player wants to move the racket upwards
			transform.position = new Vector3(pos.x, 
			pos.y, 
			pos.z + speed);
		} 
		else if (Input.GetKey(key_down) && transform.position.z > 0.5) 
		{			
			// player wants to move the racket downwards
			transform.position = new Vector3(pos.x, 
			                                 pos.y, 
			                                 pos.z - speed);
		}
	}


}
