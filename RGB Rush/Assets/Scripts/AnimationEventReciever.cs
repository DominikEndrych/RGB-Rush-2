using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventReciever : MonoBehaviour
{
    public UnityEvent OnAnimationEnd;

    public void OnAnimationEnd_Invoke()
    {
        Debug.Log("Animation end invoked");
        OnAnimationEnd.Invoke();
    }
}
