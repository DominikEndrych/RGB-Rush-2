using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerLine : MonoBehaviour
{
    public UnityEvent OnEnemyCollision;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            OnEnemyCollision.Invoke();
        }
    }
}
