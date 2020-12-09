using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoboController : MonoBehaviour
{
    bool canJump;
    bool sleep = false;
    // Start is called before the first frame update
    void Start()
    { }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey("right"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey("up"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000f * Time.deltaTime));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
           // gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey("down"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1000f * Time.deltaTime));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.G))
        {
            gameObject.GetComponent<Animator>().SetBool("golpear", true);
        }
        if (!Input.GetKey(KeyCode.G))
        {
            gameObject.GetComponent<Animator>().SetBool("golpear", false);
        }

        if (Input.GetKey(KeyCode.M))
        {
            gameObject.GetComponent<Animator>().SetBool("morder", true);
        }
        if (!Input.GetKey(KeyCode.M))
        {
            gameObject.GetComponent<Animator>().SetBool("morder", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            sleep = !sleep;
            gameObject.GetComponent<Animator>().SetBool("sleep", sleep);
        }


        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Animator>().SetBool("aullar", true);
        }
        if (!Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Animator>().SetBool("aullar", false);
        }

        if (!Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("down") && !Input.GetKey("up"))
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }

        if (Input.GetKeyDown("up") && canJump)
        {
            canJump = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100f));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            canJump = true;
        }
    }
}

