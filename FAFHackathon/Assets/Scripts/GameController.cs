using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

	private void Start()
	{
        if (instance != null && this != instance)
            Destroy(gameObject);
        else if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
	}
}
