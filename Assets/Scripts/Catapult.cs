using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Catapult Class
/// A "Item" can be shoot when the method Shoot is called and the Animation of the catapult is done (This can be changed with the animationIsFinished param or with ChangeAnimationStatus method)
/// </summary>
public class Catapult : MonoBehaviour {

	
	public float maxPressedTime = 2;
	public float maxForce;
	public float minForce;
	[Range(0,90)]
	public float shootAngle = 45;
	[Tooltip("If there is an animation, it could wait for it to finish or shoot right away")]
	public bool animationIsFinished = false;
	public GameObject itemToShoot;
	private float timePressedBackup = 0;
	
	
	void Start () {
		
	}
	
	void Update () {
		
	}

	/// <summary>
	/// Shoot with a specificic force based on time holding a button or something
	/// </summary>
	/// <param name="timePressed">Time being charged, but if time is higher than maxPressedTime it becomes maxPressedTime</param>
	public void Shoot(float timePressed) {
		
		// Shoot
		if (animationIsFinished) {
			itemToShoot.GetComponent<Rigidbody>().AddForce(CalculateForce(timePressed));
			timePressedBackup = 0;
		}
		// If the animation isn't done yet, save the time and wait for a animation finish call
		else {
			timePressedBackup = timePressed;
		}
	}

	/// <summary>
	/// Changes the status of the animation.
	/// True: Animation finished
	/// False: Not
	/// </summary>
	/// <param name="b">Boolean status</param>
	public void ChangeAnimationStatus(bool b) {
		
		animationIsFinished = b;
		
		if (timePressedBackup > 0)
			Shoot(timePressedBackup);
	}

	/// <summary>
	/// Force in Z axis 
	/// </summary>
	/// <param name="timePressed">Charge time so it can calculate force accordingly</param>
	/// <returns></returns>
	Vector3 CalculateForce(float timePressed) {
		
		// Unit vector, looking to Z axis
		Vector3 force = new Vector3() {
			y = Mathf.Sin(shootAngle),
			z = Mathf.Cos(shootAngle)
		};
		
		if (timePressed > maxPressedTime) {
			timePressed = maxPressedTime;
		}
		
		// Linear calculation of the force that will be applied
		float m = ((maxForce - minForce) / maxPressedTime);
		force *= ((m * timePressed) + minForce);
		
		return force;
	}
	
}
