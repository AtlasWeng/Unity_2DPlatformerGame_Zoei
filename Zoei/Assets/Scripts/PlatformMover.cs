using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {

	public GameObject platform; // reference to the platform to move

	public GameObject[] waypoints; // array of all the waypoints 

	[Range(0f, 10f)]
	public float moveSpeed = 5f;
	public float waitAtWaypointTime = 0.5f;

	public bool loop = true; //should it loop throught the waypoint;

	//private 
	Transform _transform;
	int _waypointsIndex = 0;
	float _moveTime;
	bool _moving = true;

	// Use this for initialization
	void Start () {
		_transform = platform.transform;
		_moveTime = 0f;
		_moving = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > _moveTime) {
			Movement();
		}
	}

	void Movement ()
	{
		if ((waypoints.Length != 0) && (_moving)) {

			
			// move toward waypoint
			_transform.position = Vector3.MoveTowards (_transform.position, waypoints[_waypointsIndex].transform.position, moveSpeed * Time.deltaTime);
			//Vector3 direction = (waypoints[_waypointsIndex].transform.position - _transform.position).normalized;
			//_rigidbody.MovePosition(_transform.position + direction * moveSpeed * Time.deltaTime);

			// if the platform is close enough to waypoint, make it's new target the new waypoint
			if (Vector3.Distance (_transform.position, waypoints[_waypointsIndex].transform.position) <= .1f) {
				_waypointsIndex++;
				_moveTime = Time.deltaTime + waitAtWaypointTime;

			}

			// reset the waypoint index back to 0 for looping
			if (_waypointsIndex >= waypoints.Length) {
				if (loop) {
					_waypointsIndex = 0;
				} else {
					_moving = false;
				}
			}
		}
	}
}
