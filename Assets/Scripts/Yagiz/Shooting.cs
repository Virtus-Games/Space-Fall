using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    [Range(0,10000)]
    private float shootingSpeed;

    [SerializeField]
    [Range(0,10)]
    private float shootingFrequency;

    [SerializeField]
    [Range(1,5)]
    private float shootingReactionForce;
   [SerializeField]
   private Transform gunPoint;

   [SerializeField]
   private GameObject bulletPrefab;

   [SerializeField] private ParticleSystem shootingEffect ;
   private float timer;
   private CharacterController cc;

   public static Transform Target { get; set; }

   private void Start() 
   {
     cc = GetComponent<CharacterController>();
    StartCoroutine(ShootingSystem());
   }
   
   private IEnumerator ShootingSystem()
   {
     while(true)
     {

        
       if(Time.time > timer && Input.GetKeyDown(KeyCode.Space))
       {
        timer = shootingFrequency + Time.time;

        if(Target != null && CollectibeData.Instance.GetCountWithEnumData(TypeCollectionType.LAZERBULLET) > 0)
        {
           Vector3 dircetion = (Target.position- gunPoint.position).normalized;
           GameObject bullet = Instantiate(bulletPrefab,gunPoint.position,bulletPrefab.transform.rotation);
           Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
           bulletRB.AddForce(dircetion*shootingSpeed,ForceMode.Impulse);
           CollectibeData.Instance.RemoveCollectData(TypeCollectionType.LAZERBULLET);
           shootingEffect.Play();
           CameraShake.Instance.ShakeCamera(0.5f, 0.5f, 0.5f);
           cc.Move(new Vector3(0,0,-shootingReactionForce));
          }

        
       }

       
         yield return null;
     }
   }


 }
