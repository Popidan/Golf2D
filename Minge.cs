using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bila : MonoBehaviour
{
    public Rigidbody2D MingeRB;
    public float Mag_max;
    public float Cst_frc;
    Vector2 startPos;
    bool directionChosen;
    Vector2 direction;
    
    void Lansare()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float timer = Time.time;
            float magnitude;
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    timer = Time.time;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    magnitude = Time.time - timer;
                    Mathf.Clamp(magnitude, 0 ,Mag_max);
                    MingeRB.velocity = new Vector2(-direction.x/10 ,-direction.y/10);
                    break;
            }

        }
    }
    void Frecare()
    {
        if (MingeRB.velocity.x > 0 || MingeRB.velocity.y > 0)
        {
            MingeRB.velocity = new Vector2(MingeRB.velocity.x / Cst_frc, MingeRB.velocity.y / Cst_frc);

        }
        else if (MingeRB.velocity.x <= 0.3 && MingeRB.velocity.y <=0.3)
        {
            MingeRB.velocity = new Vector2(0, 0);
        }
    }
    //void Lansare()
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Lansare();
        Frecare();
    }
}
