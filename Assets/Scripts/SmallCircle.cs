using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCircle : MonoBehaviour
{
    LineRenderer line;
    bool isStopped;
    Rigidbody2D smallRb;
    Transform bigCircle;
    Transform bigCircleCollider;
    GameManager gm;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        smallRb = GetComponent<Rigidbody2D>();
        bigCircle = GameObject.Find("Big Circle").transform;
        bigCircleCollider = GameObject.Find("Big Circle Collider").transform;
        gm = GameObject.FindObjectOfType<GameManager>();
        bigCircle.localScale = transform.localScale * 3;
    }

    void Update()
    {
        if (isStopped)
        {
            line.SetPosition(0, bigCircle.position);
            line.SetPosition(1, transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "smallCircle")
        {
            gm.Restart();
        }
        else if(collision.gameObject.name == bigCircleCollider.name)
        {
            smallRb.velocity = Vector2.zero;
            Vector2 newPos = transform.position;
            newPos.y = 0 - bigCircleCollider.GetComponent<CircleCollider2D>().bounds.size.y/2;
            transform.position = newPos;
            transform.SetParent(bigCircle);
            isStopped = true;
            gm.UpdateSmallCircles();
        }
 
    }
}
