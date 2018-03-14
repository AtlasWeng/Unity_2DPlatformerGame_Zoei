using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaAnimationController : MonoBehaviour {
	public GameObject Umbrella;

	public void EnableUmbrella(){
		Umbrella.SetActive(true);
	}

	public void DisableUmbrella(){
		Umbrella.SetActive(false);
	}
}
