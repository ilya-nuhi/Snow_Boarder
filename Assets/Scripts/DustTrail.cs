using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustParticles;

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag =="ground"){
            dustParticles.Play();
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag =="ground"){
            dustParticles.Stop();
        }
    }
}
