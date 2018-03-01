using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : CustomPhysics {

	[Range(0f, 10f)]
	public float maxSpeed = 7f;
	[Range(0f, 10f)]
	public float BlowUpSpeedModifier = 1f;
	[Range(0f, 10f)]
	public float maxFallingDownSpeed = 1f;

	float BlowUpSpeed = 1f;
	bool _isFacingRight;
	bool _inWindyArea;


	// Attach the components
	Animator _animator;
	public Collider2D capsuleCollider;

	// Use this for initialization
	void Awake () {
		_animator = GetComponent<Animator>();
		capsuleCollider = GetComponent<CapsuleCollider2D>();
	}

	void Start (){
	}

	protected override void ComputeVelocity ()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		// whether character turn umbrella on or not
		bool turnUmbrellaOn = Input.GetButton ("Jump") ? true : false;

		// Blow Up
		_inWindyArea = Physics2D.IsTouchingLayers (capsuleCollider, LayerMask.GetMask ("Wind"));

		if (_inWindyArea && turnUmbrellaOn) {
			gravityModifier = 0f;
			velocity.y = BlowUpSpeed * BlowUpSpeedModifier;
		} else {
			gravityModifier = 1f;
		}

		if (turnUmbrellaOn && velocity.y < -maxFallingDownSpeed) {
			velocity = Vector2.down * maxFallingDownSpeed;
		}

		// Flip sprite when character turn arount
		Vector3 localScale = transform.localScale;
		if (move.x > 0) {
			_isFacingRight = true;
		} else if (move.x < 0) {
			_isFacingRight = false;
		}

		if (_isFacingRight && localScale.x < 0 || !_isFacingRight && localScale.x > 0) {
			localScale.x *= -1;
		}

		transform.localScale = localScale;

		// Update the animator conditions
		_animator.SetBool("_isGrounded", _isGrounded);
		_animator.SetBool("_withUmbrella", turnUmbrellaOn);
		_animator.SetFloat("_velocityX", Mathf.Abs(velocity.x) / maxSpeed);	
		_animator.SetFloat("_velocityY", velocity.y);

		// Update the velocity
		targetVelocity = move * maxSpeed;


		// Debug statements
		//Debug.Log("In Windy Area: " + _inWindyArea);
	}
}
