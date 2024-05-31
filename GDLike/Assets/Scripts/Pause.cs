using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ���������� ������
/// </summary>
public class Pause : MonoBehaviour
{
    /// <summary>
    /// ����� ��������� ���� ����� �����
    /// </summary>
    public void Activate()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ����� ��������� ���� �� �����
    /// </summary>
    public void Diactivate()
    {
        gameObject.SetActive(true);
    }
}
