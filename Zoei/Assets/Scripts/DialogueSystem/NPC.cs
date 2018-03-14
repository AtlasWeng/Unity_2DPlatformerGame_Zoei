using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public Dialogue dialogue;

	bool meetPlayer = false;

	void Update ()
	{
		if (meetPlayer && Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("space key down");
			FindObjectOfType<DialogueManager>().DisplayNextSentence();
		}
	}

	void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.CompareTag ("Player")) {
			TriggerDialogue();
			meetPlayer = true;
		}

	}

	void OnTriggerExit2D (Collider2D collider)
	{
		if (collider.CompareTag ("Player")) {
			FindObjectOfType<DialogueManager>().EndDialogue();
			meetPlayer = false;
		}

	}
}
