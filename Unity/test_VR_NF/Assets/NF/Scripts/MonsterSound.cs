using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
