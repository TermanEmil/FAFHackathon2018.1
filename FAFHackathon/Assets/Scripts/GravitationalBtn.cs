using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GravitationalBtn : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onActivate;
    public UnityEvent onDesactivate;

    public Transform mainPart;
    public Transform activePos;
    public Transform disabledPos;

    private List<Transform> sittingObjs = new List<Transform>();
    private bool lastState = false;

	private void Start()
	{
        onActivate.AddListener(SetActivePos);
        onDesactivate.AddListener(SetPassivePos);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        onTriggerEnter.Invoke();
        sittingObjs.Add(collision.transform);
        CheckForSittingObjs();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        onTriggerExit.Invoke();
        sittingObjs.Remove(collision.transform);
        CheckForSittingObjs();
	}

    private void CheckForSittingObjs()
    {
        sittingObjs.RemoveAll(x => x.gameObject == null);
        sittingObjs.RemoveAll(x => x.GetComponent<Collider2D>().enabled == false);

        if (sittingObjs.Count == 0)
        {
            if (lastState != false)
            {
                onDesactivate.Invoke();
                lastState = false;
            }
        }
        else
        {
            if (lastState != true)
            {
                onActivate.Invoke();
                lastState = true;
            }
        }
    }

    private void SetActivePos()
    {
        mainPart.position = activePos.position;
    }

    private void SetPassivePos()
    {
        mainPart.position = disabledPos.position;
    }
}
