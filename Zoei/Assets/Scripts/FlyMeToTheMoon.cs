using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyMeToTheMoon : MonoBehaviour {

	public Button replayButton;
	public Button menuButton;

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.CompareTag ("Player")) {
			FindObjectOfType<DeadzoneCamera>().target = null;
			replayButton.gameObject.SetActive(true);
			menuButton.gameObject.SetActive(true);
		}
	}
}
