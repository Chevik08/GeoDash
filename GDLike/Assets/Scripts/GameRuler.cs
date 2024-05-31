using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuler : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject cameraRuler;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Colors coloristic;

    private Vector3 initCam;
    private Vector3 initCubePos;
    private Quaternion initCubeRot;
    Camera camScript;

    private void Awake()
    {
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
        audio.Play();
    }

    public void Restart()
    {
        camScript.StopWork();
        cam.position = initCam;
        coloristic.Black();
        cube.transform.position = initCubePos;
        cube.transform.rotation = initCubeRot;
        cube.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        cube.GetComponent<Rigidbody2D>().AddForce(Vector2.zero, ForceMode2D.Force);
        audio.Play();
    }
}
