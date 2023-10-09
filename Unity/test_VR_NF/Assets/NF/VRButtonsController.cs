using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VRButtonsController : MonoBehaviour
{
    public GameObject HappyButton, AngerButton, SadButton;
    SceneController controller;
    public TMP_Text text1;
    public GameObject eventSys;
    // Start is called before the first frame update
    void Start()
    {
        controller = eventSys.GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        text1.SetText(other.gameObject.name);
        if (other.gameObject == HappyButton)
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            controller.TaskOnClick(1);
        }

        if (other.gameObject == AngerButton)
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            controller.TaskOnClick(2);
        }

        if (other.gameObject == SadButton)
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            controller.TaskOnClick(3);
        }

    }
}
