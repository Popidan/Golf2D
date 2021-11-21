using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bila : MonoBehaviour
{
    public float power = 10f;
    //de folosit 0.01
    public float frc = 0.01f;
    public float maxDrag = 5f;
    public bool seMisca = false;
    //public float minDrag = 0.3f;
    public Rigidbody2D rb;
    public LineRenderer lr;
    Vector3 dragStartPos;
    Touch touch;

    void Update()
    {
        if (Input.touchCount > 0 && seMisca)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                
                    DragStart();
                
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                DragRelease();
                seMisca = false;
            }
        }
        if(rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            seMisca = true;
        }
        Frecare();
    }

    void Frecare()
    {
        if (rb.velocity.x >= frc || rb.velocity.x <= -frc)
        {
            rb.velocity = new Vector2(rb.velocity.x - frc, rb.velocity.y);
           // frc += 0.00001f;
        }
        if (rb.velocity.y >= frc || rb.velocity.y <= -frc)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - frc);
            //frc += 0.0001f;
        }
        
        if (rb.velocity.x < frc && rb.velocity.x > -frc)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (rb.velocity.y < frc && rb.velocity.y > -frc)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }


    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }
    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }
    void DragRelease()
    {
        lr.positionCount = 0;
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;
        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        seMisca = true;
    }
}