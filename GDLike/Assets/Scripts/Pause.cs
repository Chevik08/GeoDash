using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public void Activate()
    {
        gameObject.SetActive(false);
    }

    public void Diactivate()
    {
        gameObject.SetActive(true);
    }
}
