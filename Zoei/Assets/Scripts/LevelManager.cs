using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public static LevelManager lm;

	void Start ()
	{
		if (lm == null) {
			lm = this.GetComponent<LevelManager>();
		}
	}

	public void LoadLevel (string level)
	{
		SceneManager.LoadScene(level);
	}

	public void ResetTheGame(){
		Debug.Log("reset the game");
		LoadLevel("03a EndScene");
	}

	public void Exit(){
		Application.Quit();
	}
}
