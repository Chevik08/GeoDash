using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Colors : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    public void First()
    {
        tilemap.gameObject.GetComponent<Tilemap>().color = new Color(132, 0, 106, 255);
    }

    public void Black()
    {
        tilemap.gameObject.GetComponent<Tilemap>().color = Color.black;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        First();
    }
}
