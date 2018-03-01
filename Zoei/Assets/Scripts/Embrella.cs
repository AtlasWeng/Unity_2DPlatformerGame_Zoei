using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embrella : MonoBehaviour {

	//public parameters
	public float upSpeed;

	//attach components
	Animator _animator;
	Rigidbody2D _rb2D;

	void Start(){
		_animator = GetComponent<Animator>();
		_rb2D = GetComponent<Rigidbody2D>();
		EmbrellaThrowUp();
	}

	void EmbrellaThrowUp(){
		//_rb2D.AddForce(transform.up * upSpeed);
		_rb2D.velocity = transform.up * upSpeed;
	}

	void EmbrellaFallDown(){
		_rb2D.velocity = new Vector2(0,0);
		_rb2D.gravityScale *= 0.1f;
	}
}
