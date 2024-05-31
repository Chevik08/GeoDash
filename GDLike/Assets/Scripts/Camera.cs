using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ���������� �������
/// </summary>
public class Camera : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject cubeObj;
    private bool work = false;
    private float slideLevel;

    readonly private float height = 2.5f;

    public void Awake()
    {
        slideLevel = cubeObj.gameObject.transform.position.y;
    }

    /// <summary>
    /// ����� ������� �������� ������
    /// </summary>
    public void StartWork()
    {
        work = true;
    }

    /// <summary>
    /// ����� ��������� �������� ������
    /// </summary>
    public void StopWork()
    {
        work = false; 
    }

    /// <summary>
    /// ����� ����������� ����������� ������ ����� �� �������
    /// </summary>
    /// TODO: ���� ����������� ��� ������ ����������� �� ������� ����������
    void Update()
    {
        if (work)
        {
            Vector2 cube = cubeObj.transform.position;
            if (cube.y >= slideLevel + height | cube.y <= slideLevel - height)
            {
                slideLevel = cube.y;
                StartCoroutine(Smoother());
            }
            else
            {
                Vector2 new_pos = Vector2.Lerp(cam.position, cube, 1f);
                cam.position = new Vector3(new_pos.x, cam.position.y, -10);
            }
        }
    }

    /// <summary>
    /// ���������� ��� �������� �������� ������
    /// </summary>
    private IEnumerator Smoother()
    {
        while (Vector2.Distance(cubeObj.transform.position, cam.position) > 0.5)
        {
            if (!work) { break; }
            Vector2 new_pos = Vector2.Lerp(cam.position, cubeObj.transform.position, 0.05f);
            cam.position = new Vector3(new_pos.x, new_pos.y, -10);
            yield return null;
        }
    }

    /// <summary>
    /// �����, ��� ������� ������ ����������� � ������� ���������� ��������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            work = true;
        }
    }
}
