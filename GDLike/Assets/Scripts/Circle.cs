using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private bool state = false;

    public void Awake()
    {
        state = false;
    }

    public bool GetState()
    {
        return state;
    }

    public float GetHeight()
    {
        return gameObject.transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        state= true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        state = false;
    }
}
