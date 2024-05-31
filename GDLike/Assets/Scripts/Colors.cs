using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// ����� ����� ����� ����
/// </summary>
public class Colors : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    /// <summary>
    /// ����� ����������� ����
    /// </summary>
    public void First()
    {
        tilemap.gameObject.GetComponent<Tilemap>().color = new Color(132, 0, 106, 255);
    }

    /// <summary>
    /// ����� ���������� ����
    /// </summary>
    public void Black()
    {
        tilemap.gameObject.GetComponent<Tilemap>().color = Color.black;
    }

    /// <summary>
    /// ����� ������������ �� �������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        First();
    }
}
