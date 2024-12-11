using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersController : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Player.gameObject.transform);
    }
}
