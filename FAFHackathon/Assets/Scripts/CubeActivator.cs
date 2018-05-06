using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CubeActivator : NetworkBehaviour
{
    [ClientRpc]
	public void RpcDesotryMe()
    {
        GetComponent<Animator>().SetTrigger("Destroy");
        Invoke("DestroyObj", 2);
    }


    public void DestroyObj()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);

        if (isServer)
            NetworkServer.Destroy(gameObject);
    }
}
