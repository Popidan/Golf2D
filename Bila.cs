using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bila : MonoBehaviour
{   
    /// <summary>
    /// #implementat#
    /// -Drag
    /// -frecare
    /// -rand
    /// -sa facem menu
    /// 
    /// #de implementat#
    /// -sa se scaleze pe orice ecran
    /// -sa punem gaura
    /// -sa facem nivele
    /// -sistem de selectat nivele
    /// 
    /// -animatii
    /// ...
    /// 
    /// </summary>
    public float power = 30f;
    public float TimpStart;
    public float TimpActual;
    private float panta;
    private Vector2 sgtof;
    public bool Lvlcompl = false;
    public float frc = 0.01f;
    
    public float maxDrag = 5f;
    private bool seMisca = false;
    private Vector3 vertStart;
    public GameObject Gaura;
    public Rigidbody2D rb;
    public SpriteRenderer sageata;
    public LineRenderer lr;
    Vector3 dragStartPos;
    Touch touch;

    void Start()
    {
        sgtof.x = 0.0f;
        sgtof.y = 0.5f;
        sageata.enabled = false;
    }
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
        Reset_pos();
        verificare();
    }

    void Frecare()
    {
        if (rb.velocity.x >= frc )
        {
            rb.velocity = new Vector2(rb.velocity.x - frc, rb.velocity.y);
           // frc += 0.00001f;
        }
        if (rb.velocity.x <= -frc)
        {
            rb.velocity = new Vector2(rb.velocity.x + frc, rb.velocity.y);
            // frc += 0.00001f;
        }
        if (rb.velocity.y >= frc )
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - frc);
            //frc += 0.0001f;
        }
        if (rb.velocity.y <= -frc)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + frc);
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
    void Reset_pos()
    {
        if (!(transform.position.x > -30 && transform.position.x < 30) || !(transform.position.y > -50 && transform.position.x < 50)) {
            transform.position = new Vector2(0, 0);
            rb.velocity = new Vector2(0, 0);
        }
    }

    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        vertStart = new Vector3(dragStartPos.x , dragStartPos.y, dragStartPos.z);
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
        sageata.size = new Vector2(1,1);
        sageata.enabled = true;
        sageata.transform.position = new Vector2(rb.position.x + sgtof.x, rb.position.y + sgtof.y);
    }
    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
        
        sageata.transform.rotation = Quaternion.FromToRotation(Vector3.up, dragStartPos - draggingPos);
        Vector3 force = dragStartPos - draggingPos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
               

    }
    void DragRelease()
    {
        sageata.enabled = false;
        lr.positionCount = 0;
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;
        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        seMisca = true;
        sageata.size = new Vector2(0, 0);

    }
    void verificare()
    {
        if(transform.position.x <= Gaura.transform.position.x +2 && transform.position.x >= Gaura.transform.position.x + -2 && transform.position.y <= Gaura.transform.position.y + 2 && transform.position.y >= Gaura.transform.position.y + -2)
        {
            Lvlcompl = true;
            for (int i = 0; i < 10; i++) {
                transform.localScale = new Vector2 (1 - i/10, 1 - i/10);
                //new WaitForSeconds(1);
                
                
            }
            transform.localScale = new Vector2(0, 0);
            rb.velocity = new Vector2(0, 0);
        }
    }
    

}