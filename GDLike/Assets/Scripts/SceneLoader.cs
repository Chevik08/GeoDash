using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����� ��� ��������� ����
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneIdx;

    /// <summary>
    /// ����� �������� �����
    /// </summary>
    /// TODO: �������� ��������
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneIdx);
    }
}
