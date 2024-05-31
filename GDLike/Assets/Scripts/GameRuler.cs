using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����� ���������� ����������� ���������� ���� 
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
    /// ����� �������� ������������ ������
    /// </summary>
    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        // ��������� ��, ��� ��������
        camScript.StopWork();
        cam.position = initCam;
        coloristic.Black();
        audio.clip = music;
        audio.Play();
        // ���������� ��� �� ����� ������
        cube.transform.position = initCubePos;
        cube.transform.rotation = initCubeRot;
        cube.SetActive(true);
        cube.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        // �������� ������ ��������
        //Vector2 vel = cube.GetComponent<Rigidbody2D>().velocity;
        //vel.x = 0f;
        //cube.GetComponent<Rigidbody2D>().velocity = vel;
        counter.GetComponent<TMP_Text>().text = $"�������: {count}";
    }

    /// <summary>
    /// ����� �������� ������
    /// </summary>
    public void Restart()
    {
        // ���� ������
        audio.clip = death;
        audio.Play();
        // ������� �������
        count++;
        cube.SetActive(false);
        StartCoroutine(Waiter());
    }

    /// <summary>
    /// ����� ����������� ���� ����� �����
    /// </summary>
    public void Continue()
    {
        Time.timeScale = 1f;
        continueEvent.Invoke();
        audio.Play();
    }

    /// <summary>
    /// ����� ��������� ���� �� �����
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseEvent.Invoke();
        audio.Pause();
    }
}
