using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

	//player controls
	[Range(0.0f, 10.0f)]
	public float moveSpeed = 3.0f;
	[Range(0.0f, 5.0f)]
	public float flySpeed = 1.3f;
	[Range(0.0f, 10.0f)]
	public float maxRisingSpeed = 2.0f;
	public float windForce = 600.0f;
	[Range(0f, 5f)]
	public float FallingSpeed;

	//hold player motion in this timestep
	float _vx;
	float _vy;
	
	//Attach Gameobject's Components
	Animator _animator;
	Rigidbody2D _rigidbody2D;

	// Ground Dection
	public Transform groundCheck;
	public LayerMask ground;


	//Player Tracking
	bool _isMoving = false;
	bool _facingRight = true;
	public bool _isGrounded = false;
	public bool _withUmbrella = false;
	public bool _isRising = false;

	void Awake(){
		_animator = GetComponent<Animator>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Move ()
	{
		_vx = Input.GetAxisRaw ("Horizontal");

		// Determine if running based on the horizontal movement
		if (_vx != 0) {
			_isMoving = true;
		} else {
			_isMoving = false;
		}

		// set the running animation state
		_animator.SetBool("_isMoving", _isMoving);
	}

	public void BlowUp ()
	{
		if (_vy <= maxRisingSpeed) {
			_rigidbody2D.AddForce(new Vector2(0, windForce));
		}
	}

	void UpdateGravity ()
	{
		if (_withUmbrella) {
			if (!_isRising) {
				_rigidbody2D.velocity = Vector3.down * FallingSpeed;
			}
		} else {
		}
	}

	void UpdateMove ()
	{
		Vector3 characterVelocity = new Vector3(_vx, _vy);
		if (_isGrounded) {
			characterVelocity.x *= moveSpeed;
		} else {
			characterVelocity.x *= flySpeed;
		}

		// Change the actual velocity on the rigidbody
		_rigidbody2D.velocity = characterVelocity;
	}

	#region check conditions of the Character
	void CheckGround ()
	{
		RaycastHit2D GroundChecker = Physics2D.Linecast (transform.position, groundCheck.position, ground);
		if (GroundChecker.collider == null) {
			_isGrounded = false;
		} else {
			_isGrounded = true;
		}

		_animator.SetBool ("_isGrounded", _isGrounded);
	}

	void CheckUmbrella ()
	{
		if (Input.GetButtonDown ("Jump")) {
			_withUmbrella = true;
			_animator.SetBool("_withUmbrella", _withUmbrella);
		} else if (Input.GetButtonUp ("Jump")) {
			_withUmbrella = false;
			_animator.SetBool("_withUmbrella", _withUmbrella);
		}
	}

	void CheckRising ()
	{
		if (_vy > 0) {
			_isRising = true;
		} else {
			_isRising = false;
		}

		_animator.SetBool("_isRising", _isRising);

		//Debug statement
		//Debug.Log("Rising Check:" + _isRising);
	}

	void FacingDirection ()
	{
		if (_vx > 0) {
			_facingRight = true;
		} else if (_vx < 0) {
			_facingRight = false;
		}

		// using comtempoary variable to store it
		Vector3 localScale = transform.localScale;

		if ((_facingRight && localScale.x < 0) || (!_facingRight && localScale.x > 0))
		{
			localScale.x *= -1;
		}

		// update the scale
		transform.localScale = localScale;
	}
	#endregion

	#region Update
	void FixedUpdate ()
	{
		Move();

		// Get current vertical velocity from the rigidbody component
		_vy = _rigidbody2D.velocity.y;

		UpdateMove();

		//Debug.Log("Velocity: " + _rigidbody2D.velocity);
		//Debug.Log("_vy: " + _vy);
	}

	void Update ()
	{
		// Check conditions of the character
		CheckGround ();
		CheckUmbrella ();
		CheckRising ();
		UpdateGravity();
	}

	void LateUpdate ()
	{
		FacingDirection();

	}
	#endregion
}
