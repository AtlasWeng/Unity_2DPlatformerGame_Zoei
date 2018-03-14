using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryTile : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.CompareTag ("Player")) {
			collider.GetComponent<PlayerPlatformerController>().FallDeath();
		}
	}
}
