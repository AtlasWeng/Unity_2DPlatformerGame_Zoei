using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider)
	{
//		if (collider.CompareTag ("Player")) {
//			FindObjectOfType<LevelManager>().LoadLevel("03a EndScene");
//		}
		if (!collider.CompareTag ("Player")) {
			Destroy(collider.gameObject);
			Debug.Log("Something Enter");
		}
	}
}
