using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HelperScript : MonoBehaviour
{
public static HelperScript Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DelayedExecution(float delay, Action onExecute)
    {
        if (delay < 0) return;

        StartCoroutine(DelayRoutine(delay, onExecute));
    }

    private IEnumerator DelayRoutine(float delay, Action onExecute)
    {
        yield return new WaitForSeconds(delay);
        onExecute?.Invoke();
    }
}
