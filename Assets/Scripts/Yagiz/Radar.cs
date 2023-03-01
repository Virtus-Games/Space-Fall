using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField]
    [Range(0.02f,0.5f)]
    private float radarRefreshFrequency;

    [SerializeField]
    [Range(0,500)]
    private float radarSize;

     [SerializeField]   
    private Vector3 radarOffet;

  [SerializeField]
  LayerMask targetLayer;

    private Shooting shooting;
    private CrosshairManagement crosshair;

    private void Start()
     {
        shooting = GetComponentInParent<Shooting>();
        crosshair = FindObjectOfType<CrosshairManagement>();
        StartCoroutine(RadarSystem());
    }
     
        
     private IEnumerator RadarSystem()
     {
       while(true)
       {
        Collider[] targets = Physics.OverlapSphere(transform.position + radarOffet,radarSize,targetLayer);
         
         
            for (int i = 0; i < targets.Length; i++)
            {
               
              Shooting.Target = targets[i].transform;
                            
            }

            if(targets.Length ==0)
            {
              Shooting.Target = null;
              crosshair.SetCrosshairDefault();
            }
        
       

        yield return null;
       }
     }
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + radarOffet,radarSize);
    }
    
}
