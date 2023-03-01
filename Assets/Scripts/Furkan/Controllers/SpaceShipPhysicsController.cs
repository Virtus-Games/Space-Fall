using System;
using UnityEngine;

namespace Furkan.Controllers
{
     public class SpaceShipPhysicsController : Singleton<SpaceShipPhysicsController>
     {
          [SerializeField] private ShipController shipController;
          [SerializeField] private ParticleSystem shieldEffect;
          private void OnControllerColliderHit(ControllerColliderHit hit)
          {
               if (hit.collider.gameObject.CompareTag("Target"))
               {

                    if (shipController.shield)
                    {
                         CollectibeData.Instance.RemoveCollectData(TypeCollectionType.SHIELD);

                         if (CollectibeData.Instance.GetCountWithEnumData(TypeCollectionType.SHIELD) == 0)
                         {
                              shipController.shield = false;
                              shieldEffect.Stop();

                         }

                         hit.gameObject.SetActive(false);
                    }
                    else
                    {
                         hit.gameObject.SetActive(false);
                        
                         CanvasController.Instance.FinishGame();
                         Destroy(gameObject);
                    }
               }
               if (hit.collider.gameObject.CompareTag("Armory"))
               {

                    CollectibeData.Instance.AddCollectData(TypeCollectionType.LAZERBULLET, 10);
                    Destroy(hit.gameObject);
               }

               if (hit.collider.gameObject.CompareTag("Shield"))
               {
                    CollectibeData.Instance.AddCollectData(TypeCollectionType.SHIELD);
                    hit.gameObject.SetActive(false);

               }
               if(hit.collider.gameObject.CompareTag("StopPoint"))
               {
                    shipController.isDown = true;
                    CanvasController.Instance.FinishGame();
               }

               if (hit.collider.TryGetComponent(out TypeObjects typeObjects))
               {
                    CollectibeData.Instance.AddCollectData(typeObjects.TypeCollectionType());
                    hit.gameObject.SetActive(false);
               }
          }
          public Vector3 GetTransform()
          {
               return transform.position;
          }
     }



}
