using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostRate = 0.1f;
    SurfaceEffector2D surfaceEffector2D;

    [SerializeField] GameObject currentObj;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = currentObj.GetComponent<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        RespondeToBoost();
    }

    void RespondeToBoost()
    {
        if(Input.GetKey(KeyCode.DownArrow)){
            surfaceEffector2D.speed-=boostRate;
            transform.rotation = transform.rotation + new Quaternion (0,65,0);
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject != currentObj && other.gameObject.GetComponent<SurfaceEffector2D>()!=null){
            currentObj = other.gameObject;
            surfaceEffector2D = currentObj.GetComponent<SurfaceEffector2D>();
        }
    }
}
