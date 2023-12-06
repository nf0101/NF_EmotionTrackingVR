using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairController : MonoBehaviour
{
    public GameObject chair;
    public Camera eyes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chair.gameObject.transform.rotation = Quaternion.Euler(0, eyes.transform.eulerAngles.y, 0);
    }
}
