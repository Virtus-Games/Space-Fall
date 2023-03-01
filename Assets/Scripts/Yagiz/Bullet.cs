using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private ParticleSystem particle;
    void Start()
    {
      Destroy(gameObject,8f) ;
      particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    
    }

    private void OnTriggerEnter(Collider other) {
      
      if(other.gameObject.CompareTag("Target"))
      {  particle.Play();
         other.GetComponent<Target>().enabled = true;
         other.gameObject.SetActive(false);
         Destroy( gameObject,.5f);

      }
    }

     
}
