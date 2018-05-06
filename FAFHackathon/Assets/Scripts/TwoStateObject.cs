using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class TwoStateObject : NetworkBehaviour
{
    [HideInInspector]
    public bool active = false;

    public Transform targetObj;
    public Transform activePos;
    public Transform disabledPos;

    public UnityEvent onActivate;
    public UnityEvent onDisable;

    public void CmdSetState(bool activeState)
    {
        if (isServer)
        {
            RpcSetState(activeState);
        }
    }

    [ClientRpc]
    public void RpcSetState(bool activeState)
    {
        if (activeState)
        {
            if (activePos != null && targetObj != null)
                targetObj.position = activePos.position;
            onActivate.Invoke();
        }
        else
        {
            if (disabledPos != null && targetObj != null)
                targetObj.position = disabledPos.position;
            onDisable.Invoke();
        }
        active = activeState;
    }
}
