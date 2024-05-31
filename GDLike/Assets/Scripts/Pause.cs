using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс управления паузой
/// </summary>
public class Pause : MonoBehaviour
{
    /// <summary>
    /// Метод активации игры после паузы
    /// </summary>
    public void Activate()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Метод остановки игры на паузу
    /// </summary>
    public void Diactivate()
    {
        gameObject.SetActive(true);
    }
}
