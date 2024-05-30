using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controls : MonoBehaviour
{
    [SerializeField] GameObject cube;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cube.GetComponent<CubeLogic>().Control(true);
    }
}
