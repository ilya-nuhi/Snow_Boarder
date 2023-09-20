using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float torqueAmount = 15f;
    [SerializeField] float boostRate = 0.1f;
    SurfaceEffector2D surfaceEffector2D;

    bool faceForward = true;
    [SerializeField] GameObject currentObj;

    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = currentObj.GetComponent<SurfaceEffector2D>();
    }
    void FixedUpdate() {
        if(canMove){
            RotatePlayer();
            RespondeToBoost();
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(1);
        }    
    }

    public void DisableControls(){
        canMove = false;
    }

    void RespondeToBoost()
    {
        if(Input.GetKey(KeyCode.DownArrow)){
            surfaceEffector2D.speed-=boostRate;
            if(faceForward && rb2d.velocity.x > 0){
                Vector3 newEulerAngles = transform.eulerAngles;
                newEulerAngles.x = 0;
                newEulerAngles.y = 45;
                transform.eulerAngles = newEulerAngles;
                faceForward = false;
            }
            else if (!faceForward && rb2d.velocity.x < 0){
                Vector3 newEulerAngles = transform.eulerAngles;
                newEulerAngles.x = 0;
                newEulerAngles.y = 0;
                transform.eulerAngles = newEulerAngles;
                faceForward = true;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow)){
            surfaceEffector2D.speed+=boostRate;
            if(!faceForward && rb2d.velocity.x > 0){
                Vector3 newEulerAngles = transform.eulerAngles;
                newEulerAngles.x = 0;
                newEulerAngles.y = 0;
                transform.eulerAngles = newEulerAngles;
                faceForward = true;
            }
            else if (faceForward && rb2d.velocity.x < 0){
                Vector3 newEulerAngles = transform.eulerAngles;
                newEulerAngles.x = 0;
                newEulerAngles.y = 45;
                transform.eulerAngles = newEulerAngles;
                faceForward = false;
            }
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
