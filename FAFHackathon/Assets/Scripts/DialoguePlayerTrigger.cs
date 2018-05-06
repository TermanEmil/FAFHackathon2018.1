using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialoguePlayerTrigger : MonoBehaviour
{
    public int requiredPlayers = 2;
    public UnityEvent onPlayersCrossed;

    public List<Dialogue> dialogues = new List<Dialogue>();
    private List<GameObject> crossedPlayers = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && !crossedPlayers.Contains(collision.gameObject))
        {
            crossedPlayers.Add(collision.gameObject);
            if (crossedPlayers.Count >= requiredPlayers)
            {
                FindObjectOfType<DialogueManager>().StartDialogues(new Queue<Dialogue>(dialogues));
                onPlayersCrossed.Invoke();
            }
        }
	}
}
