using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text npcNameText;
	public Text dialogueText;

	private Queue<string> sentences;

	public Animator _animator;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		_animator.SetBool("_isOpen", true);
		npcNameText.text = dialogue.npcName;

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0) {
			EndDialogue();
			return;
		}

		string displaySentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(displaySentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue(){
		_animator.SetBool("_isOpen", false);
	}
}
