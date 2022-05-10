using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{

    public Transform grapplePosition;
    public Rigidbody2D rb;
    public LineRenderer lineRenderer;

    [SerializeField] float grappleDistance;
    [SerializeField] float maxGrappleDistance;


    public static Vector2 mousePos;

    GameObject[] pointsObject;
    Transform[] points;


    [SerializeField] float speed;
    [SerializeField] float grappleSpeed;
    [SerializeField] float maxSpeed;
    void Start()
    {


        pointsObject = GameObject.FindGameObjectsWithTag("point");
        points = new Transform[pointsObject.Length];
        for (int i = 0; i < pointsObject.Length; i++)
        {
            points[i] = pointsObject[i].transform;
        }






    }


    void Update()
    {
        movement();
        grappleCode();


    }





    void movement()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime, ForceMode2D.Impulse);
    }


    Transform closestGrapplePoint(Transform[] points)
    {
        Transform closestPoint = null;

        float minDist = 1f;
        foreach (Transform point in points)
        {
            float dist = Vector2.Distance(point.position, mousePos);
            if (dist < minDist)
            {
                closestPoint = point;
                minDist = dist;
            }
        }
        return closestPoint;

    }


    void grappleCode()
    {
        grapplePosition = closestGrapplePoint(points);


        if (Input.GetMouseButton(0) && grapplePosition != null)
        {
           
            mousePos = grapplePosition.position;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePosition.position);


            transform.LookAt(grapplePosition, Vector3.left);
            Vector3 ForceDir = grapplePosition.position - transform.position;

            float rotationZ = Mathf.Atan2(ForceDir.y, ForceDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);


            

            if (Vector2.Distance(grapplePosition.position, transform.position) > grappleDistance)
            {
                rb.AddForce(ForceDir.normalized * grappleSpeed, ForceMode2D.Force);
                limitPlayerSpeed(maxSpeed);
            }
        }
        else
        {
            lineRenderer.enabled = false;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 1f);
        }
    }



    void limitPlayerSpeed(float speed)
    {
        if (rb.velocity.x > speed)
        {
            rb.velocity = new Vector3(speed, rb.velocity.y);
        }
        if (rb.velocity.x < -speed)
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y);
        }

        if (rb.velocity.y > speed)
        {
            rb.velocity = new Vector3(rb.velocity.x, speed);
        }
        if (rb.velocity.y < -speed)
        {
            rb.velocity = new Vector3(rb.velocity.x, -speed);
        }
    }
    



}
