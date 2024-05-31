using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс для подгрузки сцен
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneIdx;

    /// <summary>
    /// Метод загрузки сцены
    /// </summary>
    /// TODO: Добавить отгрузку
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneIdx);
    }
}
