using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour {

	public float risingSpeedAfterWin = 0.5f;
	public float textFadeTime = 2f;

	bool win = false;

	public Text[] FMTMUICoin;
	public Text[] FMTMUIScore;

	public Text Staff;
	public Text FinCoin;
	public Text FinScore;
	public Text HighScore;

	void OnTriggerStay2D (Collider2D collider)
	{
		if (collider.CompareTag ("Player") && Input.GetButtonDown ("Jump") && !win) {
			win = true;

			GameManager.gm.DisableScoreUICanvas();

			FinCoin.text = GameManager.gm.coin.ToString();
			FinScore.text = GameManager.gm.score.ToString();

			if (GameManager.gm.score >= PlayerPrefsManager.GetHighestScore()) {
				PlayerPrefsManager.SetHighestScore(GameManager.gm.score);
			}

			HighScore.text = PlayerPrefsManager.GetHighestScore().ToString();

			collider.GetComponent<PlayerPlatformerController> ().BlowUpSpeedModifier = risingSpeedAfterWin;
			collider.GetComponent<PlayerPlatformerController> ().playerCanMove = false;
			collider.GetComponent<PlayerPlatformerController> ().turnUmbrellaOn = true;

			MusicManager _musicManager = FindObjectOfType<MusicManager> ();
			if (_musicManager) {
				_musicManager.FlyMeToTheMoon();
			}

			StartCoroutine(ScoreScene());
		}
	}

	IEnumerator FadeIn (Text i)
	{
		i.color = new Color (i.color.r, i.color.g, i.color.b, 0);

		while (i.color.a < 1.0f) {
			i.color = new Color (i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / textFadeTime));
			yield return null;
		}
	}

	IEnumerator FadeOut (Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);

		while (i.color.a > 0.0f) {
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / textFadeTime));
			yield return null;
		}
	}

	IEnumerator ScoreScene ()
	{
		yield return new WaitForSeconds (12.0f);
		foreach (Text i in FMTMUICoin) {
			yield return FadeIn (i);
		}

		yield return new WaitForSeconds (5.0f);
		foreach (Text i in FMTMUICoin) {
			yield return FadeOut (i);
		}

		yield return new WaitForSeconds (6.0f);
		foreach (Text i in FMTMUIScore) {
			yield return FadeIn (i);
		}

		yield return new WaitForSeconds (3.0f);
		foreach (Text i in FMTMUIScore) {
			yield return StartCoroutine(FadeOut(i));
		}

		yield return new WaitForSeconds(5.0f);
		yield return FadeIn (Staff);

		yield return new WaitForSeconds(5.0f);
		yield return FadeOut(Staff);
	}
}
