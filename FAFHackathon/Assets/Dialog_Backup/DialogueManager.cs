using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    public UnityEvent onDialogEnd;

    private Queue<Dialogue> dialogues;
    private Queue<string> sentences;

	void Start () {
        sentences = new Queue<string>();
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space))
            DisplayNextSentence();
	}

	public void StartDialogues(Queue<Dialogue> dialogues)
    {
        this.dialogues = dialogues;
        StartNextDialogue();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
            
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
	
    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        onDialogEnd.Invoke();
        if (dialogues.Count > 0)
            StartNextDialogue();
    }

    private void StartNextDialogue()
    {
        if (dialogues.Any())
        {
            StartDialogue(dialogues.First());
            dialogues.Dequeue();
        }
    }
}
