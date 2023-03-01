using System;
using System.Collections;
using UnityEngine;

namespace Furkan.Controllers
{
     public class MeteorRotateController : MonoBehaviour
     {
          private void OnEnable()
          {
               StartCoroutine(Rotator());
          }

          private void OnDisable()
          {
               StopCoroutine(Rotator());
          }

          IEnumerator Rotator()
          {
               while (true)
               {
                    transform.Rotate(5, 5, 5, Space.World);
                    yield return new WaitForSeconds(.5f);
               }
          }
     }
}
