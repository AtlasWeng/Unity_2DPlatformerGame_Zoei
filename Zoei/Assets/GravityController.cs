using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {

	Rigidbody2D rb2D;

	void Awake(){
		rb2D = GetComponent<Rigidbody2D>();
	}

	public void ResetGravity ()
	{
		rb2D.gravityScale = 1f;
	}

	public void ChangeGravity (float value)
	{
		rb2D.gravityScale = value;
	}
}
