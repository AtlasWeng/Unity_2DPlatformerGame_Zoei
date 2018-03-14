using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	// game performance
	public int coin = 0;
	public int score = 0;

	// UI elements to control
	public Canvas UIScoreCanvas;
	public Text UICoin;


	void Awake ()
	{
		if (gm == null) {
			gm = this.GetComponent<GameManager>();
		}
	}

	public void AddPoints(int value){
		coin += value;
		AddScore(value * 100);
		UICoin.text = coin.ToString();
	}

	public void AddScore (int amount)
	{
		score += amount;
	}

	public void DisableScoreUICanvas ()
	{
		UIScoreCanvas.gameObject.SetActive(false);
	}
}
