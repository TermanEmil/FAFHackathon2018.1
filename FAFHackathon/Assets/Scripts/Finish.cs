using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Finish : NetworkBehaviour
{
    public string nextLevelName;
    public int requiredPlayers = 2;
    private List<GameObject> crossedPlayers = new List<GameObject>();
    public bool disabledAfterCross;
    public UnityEvent onCross;
    public UnityEvent onFinish;
    public UnityEvent onEndGame;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        
        var playerMv = collision.GetComponent<PlayerMovement>();
        if (playerMv != null && !crossedPlayers.Contains(collision.gameObject))
        {
            crossedPlayers.Add(collision.gameObject);
            onCross.Invoke();

            if (disabledAfterCross)
            {
                if (playerMv != null)
                {
                    playerMv.enabled = false;
                }
            }

            if (crossedPlayers.Count >= requiredPlayers)
            {
                onFinish.Invoke();
                Debug.Log(crossedPlayers.Count);
                LoadNextLevel();
            }
        }
	}

    public void LoadNextLevel()
    {
        if (nextLevelName == null)
            return;

        if (nextLevelName == "ItWasFinish")
            onEndGame.Invoke();
        else
        {
            if (GetComponent<NetworkIdentity>().isServer)
                GameController.instance.networkManager.ServerChangeScene(nextLevelName);
        }
    }
}
