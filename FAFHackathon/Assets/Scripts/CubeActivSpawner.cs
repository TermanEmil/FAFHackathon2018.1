using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CubeActivSpawner : NetworkBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    private CubeActivator spawnedCube;

    public void Spawn()
    {
        if (!isServer)
            return;
        
        if (spawnedCube != null)
            spawnedCube.RpcDesotryMe();

        spawnedCube = Instantiate(prefab).GetComponent<CubeActivator>();
        NetworkServer.Spawn(spawnedCube.gameObject);
        spawnedCube.transform.position = spawnPoint.position;
    }
}
