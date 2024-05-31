using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneIdx;

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneIdx);
    }
}
