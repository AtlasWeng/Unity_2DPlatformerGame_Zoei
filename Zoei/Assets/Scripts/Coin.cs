using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

	protected int coinValue = 1;
	public bool taken = false;

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.CompareTag ("Player") && !taken && collider.GetComponent<PlayerPlatformerController> ().playerCanMove) {

			// mark as taken so dosent get taken multiple times
			taken = true;

			// do the player collect the coin thing
			collider.GetComponent<PlayerPlatformerController>().CollectingCoin(coinValue);

			// destroy the coin
			Destroy(this.gameObject);
		}
	}

}
