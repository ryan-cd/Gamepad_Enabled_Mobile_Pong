using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	//score
	private float playerScore;
	private float AIScore;

	//physics
	private float reflectionAngle = new float();
	private float maxReflectionAngle = 3f;
	private float speed = 8f;
	private float acceleration = 3f;
	private float maxAcceleration = 4.5f;

	private static float xSpeed;
	private static float zSpeed;
	private static float B; //from y = mx +b
	private float startX = 3;
	private float startZ = 0.3f;

	//error checking
	private static int errorCount;

	//public variables
	static float positionZ = new float();
	static float positionX = new float();
	// Use this for initialization
	void Start () {


	}

	//Getters
	public static float getZ(){return positionZ;}
	public static float getZSpeed(){return zSpeed;}
	public static float getB(){return positionZ - zSpeed * positionX;}
	
	// Update is called once per frame
	void Update () {
		//print (zSpeed/xSpeed);

		// if zSpeed is much greater than xSpeed, set the speed as if it reflected realistically.
		if (Mathf.Abs(zSpeed) / Mathf.Abs(xSpeed) > 1)
		{
			errorCount += 1;
			print ("breakpoint: "+errorCount);
			if(transform.position.x > 4)
				transform.Translate (-2,0, 0);
			else
				transform.Translate (2,0, 0);
			zSpeed = (zSpeed > 0) ? -0.5f : 0.5f;
			xSpeed = (xSpeed > 0) ? -8 : 8;
		}

		if (acceleration < maxAcceleration)
			acceleration += 0.1f;
		// keep the speed constant
		xSpeed = (acceleration + speed) * rigidbody.velocity.normalized.x;
		zSpeed = (acceleration + speed) * rigidbody.velocity.normalized.z;
		rigidbody.velocity = new Vector3(xSpeed, 0, zSpeed);
		//rigidbody.velocity = Mathf.Round(speed * rigidbody.velocity.normalized);

		positionZ = transform.position.z;
		positionX = transform.position.x;
		//Debug.Log("racketleft: " + RacketAI.getZ() +" ball: " + transform.position.z);
		//print ("velocities. x: " + rigidbody.velocity.x+" y: "+rigidbody.velocity.y+" z: "+rigidbody.velocity.z + "math " + -1*rigidbody.velocity.x);

	}
	//sounds
	public AudioClip breakoutHi;
	//[RequireComponent(typeof(AudioSource))]
	void OnCollisionEnter(Collision collision) {
		audio.PlayOneShot(breakoutHi);
		// Invert the velocity
		if (collision.collider.name == "BorderLeft")
		{//AI scored
			AIScore += 1;
			reset ();
		}
		else if (collision.collider.name == "BorderRight")
		{//player scored
			playerScore += 1;
			reset ();
		}
		else if (collision.collider.name == "BorderTop")
		{
			speed += 0.2f;
			// change vertical direction
			rigidbody.velocity = new Vector3(xSpeed, 
			                                 0, 
			                                 zSpeed * -1.0f);
		}
		else if (collision.collider.name == "BorderBottom")
		{
			speed += 0.2f;
			// change vertical direction
			rigidbody.velocity = new Vector3(xSpeed, 
			                                 0, 
			                                 zSpeed * -1.0f);
		}		
		else if (collision.collider.name == "RacketLeft")
		{
			acceleration = 2f;
			reflectionAngle = -1 * (Racket.getZ() - transform.position.z)*5 - 0.0001f;
			//maxReflectionAngle = maxReflectionAngle > 6 ? maxReflectionAngle : maxReflectionAngle+0.1f;
			// make sure reflection isn't higher than max
			if (reflectionAngle > maxReflectionAngle)
				reflectionAngle = maxReflectionAngle;
			if(reflectionAngle < -1*maxReflectionAngle)
				reflectionAngle = -1*maxReflectionAngle;
			// change vertical direction
			rigidbody.velocity = new Vector3(-1.0f * xSpeed, 
			                                 rigidbody.velocity.y, 
			                                 reflectionAngle);
		}
		else if (collision.collider.name == "RacketRight")
		{
			audio.PlayOneShot(breakoutHi);
			RacketAI.flipHitOffset();
			acceleration = 2f;
			//reflectionAngle = rigidbody.velocity.z;
			reflectionAngle = -1 * (RacketAI.getZ() - transform.position.z)*5 - 0.0001f;
			//maxReflectionAngle = maxReflectionAngle > 6 ? maxReflectionAngle : maxReflectionAngle+0.1f;
			// make sure reflection isn't higher than max
			if (reflectionAngle > maxReflectionAngle)
				reflectionAngle = maxReflectionAngle;
			if(reflectionAngle < -1f*maxReflectionAngle)
				reflectionAngle = -1f*maxReflectionAngle;
			// change vertical direction
			rigidbody.velocity = new Vector3(-1.0f * xSpeed, 
			                                 rigidbody.velocity.y, 
			                                 reflectionAngle);
		}
	}

	void reset() {
		speed = 0;
		acceleration = 1.5f;
		rigidbody.velocity = new Vector3(startX, 0, startZ);
		rigidbody.position = new Vector3(3.9f,0.6f,2.2f);
	}

	void OnGUI () {
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 35;
		myStyle.normal.textColor = Color.red;//Color.gray;
		//reset button
		if (GUI.Button (new Rect (5,5,100,100), "New Game") || Racket.getCurrentButton() == "Joystick1Button0") {
			Racket.clearCurrentButton();
			playerScore = 0;
			AIScore = 0;
			reset ();
		}
		//GUI.Label (new Rect(500, 10, 150, 100), "Current Button: " + Racket.getCurrentButton()); 
		GUI.Label (new Rect(550, 10, 150, 100), "Push A to Start", myStyle);
		GUI.Label (new Rect(200, 10, 150, 100), "Player Score: " + playerScore, myStyle);
		GUI.Label (new Rect(900, 10, 150, 100), "AI Score: " + AIScore, myStyle);
	}

}
