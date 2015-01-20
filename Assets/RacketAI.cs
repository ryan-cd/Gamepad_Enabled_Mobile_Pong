using UnityEngine;
using System.Collections;

public class RacketAI : MonoBehaviour {
	//movement vars
	private static float positionZ;
	float speed = 0.055f;
	private static float hitOffset = 0.5f;
	// Use this for initialization
	void Start () {
	}

	// Getters
	static public float getZ()
	{
		return positionZ;
	}
	// Setters
	static public void flipHitOffset()
	{
		hitOffset *= -1;
	}
	
	// Update is called once per frame
	void Update () {
		positionZ = transform.position.z;

		/*if(transform.position.z == Ball.getZ () + hitOffset){
			transform.Translate (0, 0, 0);
		}
		if(transform.position.z < Ball.getZ () + hitOffset){
			transform.Translate (0, 0, 1 * speed);
		}
		if(transform.position.z > Ball.getZ () + hitOffset){
			transform.Translate (0, 0, -1 * speed);
		}*/
		if (Ball.getZSpeed() > 0) {
			if(transform.position.z < 4.2 && transform.position.z < Ball.getZSpeed() * 7.5 + Ball.getB()){
				transform.Translate (0, 0, 1 * speed);
			}
			if(transform.position.z > 0.4 && transform.position.z > Ball.getZSpeed() * 7.5 + Ball.getB()){
				transform.Translate (0, 0, -1 * speed);
			}
		} else {
			if(transform.position.z == Ball.getZ () + hitOffset){
				transform.Translate (0, 0, 0);
			}
			if(transform.position.z < Ball.getZ () + hitOffset){
				transform.Translate (0, 0, 1 * speed);
			}
			if(transform.position.z > Ball.getZ () + hitOffset){
				transform.Translate (0, 0, -1 * speed);
			}
		}
	}
}
