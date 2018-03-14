using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	public void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.CompareTag ("Player")) {
			collider.GetComponent<PlayerPlatformerController>().FallDeath();
		}
	}
}
