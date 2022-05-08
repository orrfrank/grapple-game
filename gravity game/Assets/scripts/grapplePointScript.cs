using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapplePointScript : MonoBehaviour
{

    public GameObject outline;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) < 0.5f)
        {
            outline.SetActive(true);
        }
        else
        {
            outline.SetActive(false);
        }






    }
}
