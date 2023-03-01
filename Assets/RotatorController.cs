using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Furkan.Controllers;

public class RotatorController : MonoBehaviour
{
     public float rotateSpeed = 5;
     public bool isRotate = false;
     public float speed;


     void Update()
     {
          if (!UIManager.Instance.isGameStart) return;
          if (isRotate) transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
          else
          {
               Vector3 b = SpaceShipPhysicsController.Instance.GetTransform();
               float x = transform.position.x;
               float y = transform.position.y;
               transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, b.z), speed * Time.deltaTime);
               transform.Rotate(.4f, 0, 0, Space.World);
          }
     }
}
