using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScarySoundMover : MonoBehaviour
{
    private GameObject Player;
    public float Delta = 0.01f;
    Vector3 initialPosition;
    Vector3 finalPosition;
    public GameObject test;
    private float progress;
    private bool isForward = true;
    private int counter = 0;
    public AudioSource waiting;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        initialPosition = transform.position;
        finalPosition = test.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(initialPosition, finalPosition);
       
        if (isForward)
        {
            Delta = 0.25f;
            progress += Delta / distance * Time.deltaTime;
        }

        else
        {
            Delta = 0.15f;
            progress -= Delta / distance * Time.deltaTime;
        }

        if (progress >= 1.0f)
        {
            isForward = false;
            progress = 1.0f;
        }

        else if (progress <= 0.0f)
        {
            isForward = true;
            progress = 0.0f;
            counter++;
        }

        Vector3 posizioneIntermedia = Vector3.Lerp(initialPosition, finalPosition, progress);
        transform.position = posizioneIntermedia;
        //Debug.Log(counter);
        if (counter >= 1)
        {
            waiting.enabled = true;
            waiting.Pause();
        }

        if (counter >= 2)
        {
            waiting.Play();
        }

    }
}
