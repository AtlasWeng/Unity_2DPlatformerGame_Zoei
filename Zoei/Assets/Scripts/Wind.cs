using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {
	PlayerPlatformerController _playerController;


	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.CompareTag ("Player")) {
			_playerController = collider.GetComponent<PlayerPlatformerController>();
		}
	}

	void OnTriggerStay2D (Collider2D collider)
	{
	}

	void OnTriggerExit2D (Collider2D collider)
	{
	}
}
