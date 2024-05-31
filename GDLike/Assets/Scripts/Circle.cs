using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс коллайдеров для отслеживания кубиков
/// </summary>
public class Circle : MonoBehaviour
{
    private bool state = false;

    public void Awake()
    {
        state = false;
    }

    /// <summary>
    /// Метод получения состояния коллайдера
    /// </summary>
    /// <returns> Соприкасается этот коллайдер с чем-нибудь или нет </returns>
    public bool GetState()
    {
        return state;
    }

    /// <summary>
    /// Метод получения высоты кубика
    /// </summary>
    /// <returns> Высота кубика в данный момент времени </returns>
    public float GetHeight()
    {
        return gameObject.transform.position.y;
    }

    /// <summary>
    /// Метод реагирования на все коллизии, кроме триггеров при соприкосновении
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
    /// Метод реагирования на все коллизии, кроме триггеров при отдалении
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
