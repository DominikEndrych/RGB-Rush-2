using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private Animation _animation;
    private void Awake()
    {
        _animation = GetComponent<Animation>();     // Get reference to Animation component
    }

    public void PlayAnimation(string name)
    {
        if(_animation)
        {
            _animation.Play(name);
        }
        // Log error if Animation component is missing
        else
        {
            Debug.LogError("No Animation component on " + gameObject.name);
        }
    }
}
