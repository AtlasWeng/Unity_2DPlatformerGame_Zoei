using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {
	
	static MusicManager instance = null;

	public AudioClip[] levelMusicChangeArray;

	AudioSource _audio;


	void Awake(){
		_audio = GetComponent<AudioSource>();
	}

	void Start ()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void ChangeVolume (float volume){

	}

	public void FlyMeToTheMoon()
	{
		_audio.clip = levelMusicChangeArray[0];
		_audio.Play();
	}

	void OnEnable(){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		AudioClip thisLevelMusic = levelMusicChangeArray [scene.buildIndex];

		if (thisLevelMusic) {
			_audio.clip = thisLevelMusic;
			_audio.loop = true;
			_audio.Play();
		}
	}

	void OnDisable ()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
