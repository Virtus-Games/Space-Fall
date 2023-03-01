using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
     public static Vector2 InputAxis;
     private float inputX;
     private float inputY;

     private void Start()
     {

     }
     private void Update()
     {
          if (UIManager.Instance.isGameStart)
          {
               Cursor.lockState = CursorLockMode.Locked;
               Cursor.visible = false;
          }else{
               Cursor.lockState = CursorLockMode.None;
               Cursor.visible = true;
          }

          inputX += Input.GetAxis("Mouse X");
          inputY += Input.GetAxis("Mouse Y");

          inputX = Mathf.Clamp(inputX, -45, 45);
          inputY = Mathf.Clamp(inputY, -15, 15);
          InputAxis = new Vector2(inputX, inputY);
     }

}
