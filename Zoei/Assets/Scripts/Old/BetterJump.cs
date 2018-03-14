using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour {

	public float fallMultiplier;
	public float lowMultiplier;

	Rigidbody2D _rigidbody2D;
	CharacterController2D cc;

	void Awake(){
		_rigidbody2D = GetComponent<Rigidbody2D>();
		cc = GetComponent<CharacterController2D>();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		/*
		if (_rigidbody2D.velocity.	y < 0) {
			_rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
		} else if (_rigidbody2D.velocity.y > 1 && !Input.GetButton ("Jump")) {
			_rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowMultiplier - 1) * Time.fixedDeltaTime;
		}

		if (cc._openUmbrella) {
			_rigidbody2D.velocity = 
		}
		*/

	}
}
