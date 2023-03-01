using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForward : MonoBehaviour
{
   public float speed = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Vector3.forward* Time.deltaTime * speed;
    }
}
