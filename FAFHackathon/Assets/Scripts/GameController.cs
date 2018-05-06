using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{
    public static GameController instance;
    public NetworkManager networkManager;

	private void Start()
	{
        if (instance != null && this != instance)
            Destroy(gameObject);
        else if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
            networkManager = GetComponent<NetworkManager>();
        }
	}
}
