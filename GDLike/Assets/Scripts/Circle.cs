using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ����������� ��� ������������ �������
/// </summary>
public class Circle : MonoBehaviour
{
    private bool state = false;

    public void Awake()
    {
        state = false;
    }

    /// <summary>
    /// ����� ��������� ��������� ����������
    /// </summary>
    /// <returns> ������������� ���� ��������� � ���-������ ��� ��� </returns>
    public bool GetState()
    {
        return state;
    }

    /// <summary>
    /// ����� ��������� ������ ������
    /// </summary>
    /// <returns> ������ ������ � ������ ������ ������� </returns>
    public float GetHeight()
    {
        return gameObject.transform.position.y;
    }

    /// <summary>
    /// ����� ������������ �� ��� ��������, ����� ��������� ��� ���������������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Trigger"))
        {
            state = true;
        }
    }

    /// <summary>
    /// ����� ������������ �� ��� ��������, ����� ��������� ��� ���������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Trigger"))
        {
            state = false;
        }
    }
}
