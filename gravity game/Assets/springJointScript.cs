using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springJointScript : MonoBehaviour
{

    GameObject[] grapplePoints;

    private SpringJoint2D sj;
    private Rigidbody2D rb;

    [SerializeField] float speed;

    private Vector2 mousePosition;
    void Start()
    {
        sj = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();

        grapplePoints = GameObject.FindGameObjectsWithTag("point");
    }

    
    void Update()
    {
        movement();

        if (closestGrapplePoint(grapplePoints) != null && Input.GetMouseButton(0))
        {
            mousePosition = closestGrapplePoint(grapplePoints).transform.position;
            sj.enabled = true;
            sj.connectedBody = closestGrapplePoint(grapplePoints).GetComponent<Rigidbody2D>();
            sj.distance = 3;
        }
        else
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sj.enabled = false;
        }
        
       
        




    }




    void movement()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime, ForceMode2D.Impulse);
    }




    GameObject closestGrapplePoint(GameObject[] points)
    {
        GameObject closestPoint = null;

        float minDist = 1f;
        foreach (GameObject point in grapplePoints)
        {
            float dist = Vector2.Distance(point.transform.position, mousePosition);
            if (dist < minDist)
            {
                closestPoint = point;
                minDist = dist;
            }
        }
        return closestPoint;

    }
}
