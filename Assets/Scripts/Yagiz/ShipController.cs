using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ShipController : MonoBehaviour
{
     #region Serialized Fields

     [SerializeField][Range(0, 50)] private float forwardSpeed;

     [SerializeField][Range(0, 20)] private float sidewaySpeed;

     [SerializeField][Range(0, 90)] private float zAxisMaxRotation;

     [SerializeField][Range(0, 90)] private float xAxisMaxRotation;

     [SerializeField][Range(0, 10)] private float rotationSpeed;


     [SerializeField][Range(0, 20)] private float increaseSpeed;

     [SerializeField][Range(0, 100)] private float bound;


     #endregion

     #region Private Fields

     private Vector3 moveVector;
     private CharacterController cc;
     private Transform tr;
     public bool shield;
     public bool isDown;
     private float defaultSidewaySpeed;
     #endregion

     #region Script References

     #endregion


     private void Start()
     {
          tr = this.transform;
          cc = GetComponent<CharacterController>();
          defaultSidewaySpeed = sidewaySpeed;
          shield = true;
          StartCoroutine(UpForwardSpeedWithTime());
     }

     private void Update()
     {
          if (!UIManager.Instance.isGameStart) return;
          if ((tr.position.x <= -bound && InputManager.InputAxis.x < 0) || (tr.position.x >= bound && InputManager.InputAxis.x > 0))
               sidewaySpeed = 0;
          else
               sidewaySpeed = defaultSidewaySpeed;



          Vector3 motion = new Vector3(InputManager.InputAxis.x * sidewaySpeed, InputManager.InputAxis.y * increaseSpeed, forwardSpeed) * Time.deltaTime;

          cc.Move(motion);

          Quaternion rotationAngle = Quaternion.Euler(InputManager.InputAxis.y, 0, -InputManager.InputAxis.x);
          Quaternion lerpRotation = Quaternion.Lerp(tr.rotation, rotationAngle, Time.deltaTime * rotationSpeed);
          tr.rotation = lerpRotation;
     }


     IEnumerator UpForwardSpeedWithTime()
     {
          yield return new WaitForSeconds(6);
          forwardSpeed += 2;
          StartCoroutine(UpForwardSpeedWithTime());
     }


}