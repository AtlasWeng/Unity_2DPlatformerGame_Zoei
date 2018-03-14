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

	public GameObject groundCheck;
	public LayerMask movingPlatform;

	float BlowUpSpeed = 1f;
	bool _isFacingRight;
	bool _inWindyArea;

	#region Status
	[HideInInspector]
	public bool dead = false;
	public bool playerCanMove = true;
	public bool turnUmbrellaOn = false;
	#endregion

	#region SFX
	public AudioClip coinSFX;
	public AudioClip deathSFX;
	public AudioClip fallSFX;
	public AudioClip openUmbrellaSFX;
	public AudioClip GetWindPowerSFX;
	#endregion

	// Attach the components
	Animator _animator;
	AudioSource _audio;
	public Collider2D capsuleCollider;

	// private parameters
	int _deadLayer;

	// Use this for initialization
	void Awake () {
		_animator = GetComponent<Animator>();
		_audio = GetComponent<AudioSource>();
		capsuleCollider = GetComponent<CapsuleCollider2D>();
		_deadLayer = LayerMask.NameToLayer("DeadObjects");
	}

	void Start (){
	}

	protected override void ComputeVelocity ()
	{
		//Debug.Log("can move?:" + playerCanMove);

		Vector2 move = Vector2.zero;

		if (playerCanMove) {
			move.x = Input.GetAxis ("Horizontal");

			// whether character turn umbrella on or not
			//turnUmbrellaOn = Input.GetButton ("Jump") ? true : false;
			if (Input.GetButton("Jump") && !turnUmbrellaOn) {
				if (_inWindyArea || !_isGrounded) {
					turnUmbrellaOn = true;
					PlaySound (openUmbrellaSFX);
				}
			} else if (Input.GetButtonUp("Jump")) {
				turnUmbrellaOn = false;
			}
		}


		// Check Windy Area
		_inWindyArea = Physics2D.IsTouchingLayers (capsuleCollider, LayerMask.GetMask ("Wind"));

		// Rasing
		Rasing();

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

	public void Rasing(){
		if (_inWindyArea && turnUmbrellaOn) {
			gravityModifier = 0f;
			velocity.y = BlowUpSpeed * BlowUpSpeedModifier;
		} else {
			gravityModifier = 1f;
		}
	}

	public void DoJump(){
		velocity.y = 5;
	}

	public void FallDeath ()
	{
		if (playerCanMove) {
			Debug.Log("u dead");
			StartCoroutine(KillPlayer());
		}
	}

	IEnumerator KillPlayer ()
	{
		// play death sound effect
		if (deathSFX) {
			PlaySound(deathSFX);
		}

		// freeze the player
		FreezeMotion();
		turnUmbrellaOn = false;

		// play the death animation
		_animator.SetTrigger("_dead");

		velocity.y = 4.0f;

		// let player doesn't collider with anyother objects in the scene
		this.gameObject.layer = _deadLayer;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));

		// let death zone camera doesn't follow player anymore
		FindObjectOfType<DeadzoneCamera>().target = null;

		yield return new WaitForSeconds(3f);
		LevelManager.lm.LoadLevel("03a EndScene");
	}

	public void FreezeMotion(){
		playerCanMove = false;
	}

	public void UnFreezeMotion(){
		playerCanMove = true;
	}

	protected override void CheckMovingPlatform ()
	{
		RaycastHit2D movingPlatformChecker = Physics2D.Linecast (transform.position, groundCheck.transform.position, movingPlatform);
		if (movingPlatformChecker.collider == null) {
			this.transform.parent = null;
		} else {
			this.transform.parent = movingPlatformChecker.collider.transform;
		}
	}

	void PlaySound (AudioClip clip)
	{
		_audio.PlayOneShot(clip);
	}

	public void CollectingCoin (int value)
	{
		PlaySound(coinSFX);

		GameManager.gm.AddPoints(value);
	}

	public void Winning ()
	{
		LevelManager.lm.LoadLevel("03b WinScene");
	}
}
