using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuler : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject cameraRuler;
    [SerializeField] private AudioSource audio;

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
    }

    public void Start()
    {
        audio.Play();
    }

    public void Restart()
    {
        cam.position = initCam;
        cube.transform.position = initCubePos;
        cube.transform.rotation = initCubeRot;
        camScript.StopWork();
        cube.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        cube.GetComponent<Rigidbody2D>().AddForce(Vector2.zero, ForceMode2D.Force);
        audio.Play();
    }
}
