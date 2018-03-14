using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

	[Header("enemy feature")]
	[Range(0f, 10f)]
	public float moveSpeed = 5f;
	public int score = 300;

	public GameObject[] waypoints; // to define the movement waypoint
	public bool loop = true;

	public float waitAtWaypointTime = 1f;

	[Header("Player Checker")]
	public float distanceFromCenter = .4f;
	public float deadCheckerWidth = .6f;
	public float deadCheckerHeight = .1f;
	public float attackCheckerRadius = .35f;

	// SFXs
	public AudioClip deathSFX;

	// Store reference to the components on the gameobject
	Rigidbody2D _rigidbody;
	AudioSource _audio;

	// private parameters
	LayerMask isPlayer;
	int _deadLayer;
	Vector2 velocity;


	// Movement Tracking 
	int _waypointIndex = 0;
	float _moveTime;
	float _vx = 0f;
	bool _moving = true;

	void Awake(){
		_rigidbody = GetComponent<Rigidbody2D>();
		_audio = GetComponent<AudioSource>();

		isPlayer = 1 << 9;
		_deadLayer = LayerMask.NameToLayer("DeadObjects");

		// setup moving default
		_moveTime = 0f;
		_moving = true;

	}

	void Update(){
		CheckPlayerCollider ();
	}

	void FixedUpdate ()
	{
		if (Time.time > _moveTime) {
			EnemyMovement ();
		}

		if (this.gameObject.layer == _deadLayer) {
			velocity += Physics2D.gravity * Time.deltaTime;
			Vector2 deltaPos = velocity * Time.deltaTime;
			_rigidbody.position += deltaPos;
		}
	}

	void EnemyMovement ()
	{
		if (waypoints.Length != 0 && (_moving)) {
			// make sure the enemie is facing the waypoint.
			Flip (_vx);

			Vector3 direction = (waypoints[_waypointIndex].transform.position - transform.position).normalized;

			// determine distance between waypoints and enemy
			float distance = Vector3.Distance (transform.position, waypoints [_waypointIndex].transform.position);

			Flip(direction.x);

			if (distance <= .1f) {
				_rigidbody.velocity = new Vector2 (0, 0); // stop moving
				_waypointIndex++; // change to the next waypoint
				_moveTime = Time.time + waitAtWaypointTime;

				if (_waypointIndex >= waypoints.Length) {
					if (loop) {
						_waypointIndex = 0;
					} else {
						_moving = false;
					}
				}
			}

			// move the enemy
			_rigidbody.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
		}
	}

	void CheckPlayerCollider ()
	{
		RaycastHit2D deadChecker = Physics2D.BoxCast (transform.position, new Vector2 (deadCheckerWidth, deadCheckerHeight), 0, Vector2.up, distanceFromCenter, isPlayer);
		if (deadChecker.collider && this.gameObject.layer != _deadLayer) {
			EnemyDeath();

			deadChecker.collider.GetComponent<PlayerPlatformerController>().DoJump();
			GameManager.gm.AddScore(score);
			Debug.Log("Enemy dead");
		}

		RaycastHit2D attackChecker = Physics2D.CircleCast (transform.position, attackCheckerRadius, Vector2.zero, 0f, isPlayer);
		if (attackChecker.collider && this.gameObject.layer != _deadLayer) {
			attackChecker.collider.GetComponent<PlayerPlatformerController>().FallDeath();
			Debug.Log("player dead");
		}
	}

	// debug the radius
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Vector3 newPos = transform.position;
		newPos.y += distanceFromCenter;
		Gizmos.DrawWireCube(newPos, new Vector2(deadCheckerWidth, deadCheckerHeight));
		Gizmos.DrawWireSphere(transform.position, attackCheckerRadius);
	}

//	void OnTriggerEnter2D (Collider2D collider)
//	{
//		if (collider.CompareTag("Player") && this.gameObject.layer != _deadLayer) {
//			collider.GetComponent<PlayerPlatformerController>().FallDeath();
//		}
//	}

	void EnemyDeath(){
		this.gameObject.layer = _deadLayer;
		_moving = false;

		_audio.PlayOneShot(deathSFX);
	}

	void Flip (float direction)
	{
		Vector3 localScale = transform.localScale;

		if ((direction > 0f) && (localScale.x > 0)) {
			localScale.x *= -1f;
		} else if ((direction < 0f) && (localScale.x < 0)){
			localScale.x *= -1f;
		}

		// update the scale
		transform.localScale = localScale;
	}
}
