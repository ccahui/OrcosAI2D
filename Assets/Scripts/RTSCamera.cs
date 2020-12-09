using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{

    public float speed = 20f;
    public float border = 30f;
    public Vector2 limite;
    Camera myCam;
    private void Start()
    {
        myCam = GetComponent<Camera>();
    }

    void Update()
    {

        Vector3 pos = transform.position;
        if (Input.GetKey("w") )
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s") )
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("a") )
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d") )
        {
            pos.x += speed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -limite.x, limite.x);
        pos.y = Mathf.Clamp(pos.y, -limite.y, limite.y);

        transform.position = pos;
    }

    /* void Update()
     {

         Vector3 pos = transform.position;
         if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height-border)
         {
             pos.y += speed * Time.deltaTime;
         }
         if (Input.GetKey("s") || Input.mousePosition.y <= border)
         {
             pos.y -= speed * Time.deltaTime;
         }
         if (Input.GetKey("a") || Input.mousePosition.x <= border)
         {
             pos.x -= speed * Time.deltaTime;
         }
         if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - border)
         {
             pos.x += speed * Time.deltaTime;
         }

         pos.x = Mathf.Clamp(pos.x, -limite.x, limite.x);
         pos.y = Mathf.Clamp(pos.y, -limite.y, limite.y);

         transform.position = pos;
     }
     */

}