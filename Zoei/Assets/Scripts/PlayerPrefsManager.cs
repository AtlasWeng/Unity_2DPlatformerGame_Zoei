using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string HIGHEST_SCORE = "highest_score";

	public static void SetMasterVolume (float volume)
	{
		if (0 <= volume && volume <= 1) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.Log("volume out of range!");
		}
	}

	public static float GetMasterVolume (){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}

	public static void SetHighestScore(int score){
		PlayerPrefs.SetInt (HIGHEST_SCORE, score);
	}

	public static int GetHighestScore ()
	{
		return PlayerPrefs.GetInt(HIGHEST_SCORE);
	}
}
