using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Класс управления глобальными процессами игры 
/// </summary>
public class GameRuler : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject cameraRuler;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip death;
    [SerializeField] private Colors coloristic;
    [SerializeField] private GameObject counter;

    public UnityEvent pauseEvent;
    public UnityEvent continueEvent;

    private Camera camScript;
    private Vector3 initCam;
    private Vector3 initCubePos;
    private Quaternion initCubeRot;
    private new AudioSource audio;
    private int count = 1;

    private void Awake()
    {
        audio = gameObject.AddComponent<AudioSource>();
        initCam = cam.position;
        initCubePos = cube.transform.position;
        initCubeRot = cube.transform.rotation;
        camScript = cameraRuler.GetComponent<Camera>();
        audio = GetComponent<AudioSource>();
        coloristic.Black();
        Debug.Log(initCam);
    }

    public void Start()
    {
        audio.clip = music;
        audio.Play();
    }

    /// <summary>
    /// Метод ожидания перерождения игрока
    /// </summary>
    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        // Выключаем всё, что работает
        camScript.StopWork();
        cam.position = initCam;
        coloristic.Black();
        audio.clip = music;
        audio.Play();
        // Возвращаем куб на место спавна
        cube.transform.position = initCubePos;
        cube.transform.rotation = initCubeRot;
        cube.SetActive(true);
        cube.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        // Обнуляем вектор скорости
        //Vector2 vel = cube.GetComponent<Rigidbody2D>().velocity;
        //vel.x = 0f;
        //cube.GetComponent<Rigidbody2D>().velocity = vel;
        counter.GetComponent<TMP_Text>().text = $"Попытка: {count}";
    }

    /// <summary>
    /// Метод Рестарта уровня
    /// </summary>
    public void Restart()
    {
        // Звук смерти
        audio.clip = death;
        audio.Play();
        // Счётчик смертей
        count++;
        cube.SetActive(false);
        StartCoroutine(Waiter());
    }

    /// <summary>
    /// Метод продолжения игры после паузы
    /// </summary>
    public void Continue()
    {
        Time.timeScale = 1f;
        continueEvent.Invoke();
        audio.Play();
    }

    /// <summary>
    /// Метод остановки игры на паузу
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseEvent.Invoke();
        audio.Pause();
    }
}
