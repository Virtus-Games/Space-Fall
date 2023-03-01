using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairManagement : MonoBehaviour
{
 
 public RectTransform crosshair;
 
  public float maxSize;
  public float minSize;
  public float interpolateSpeed;
  
   private Vector2 defaultsize;
   public List<GameObject> uis;

   private float currentSize;
   

   private void Start()
    {
    
     defaultsize = new Vector2(maxSize,maxSize);
     currentSize= defaultsize.x;
     StartCoroutine(CrosshairSystem()); 
     StartCoroutine(CrosshairColorChange());
     crosshair.gameObject.SetActive(false);
           

    }

    private IEnumerator CrosshairSystem()
    {
      while(true)
      {
          if(Shooting.Target !=null  )
          {

            foreach (var item in uis)
            {
              item.GetComponent<Image>().color = Color.red;
            }
            crosshair.gameObject.SetActive(true);

          Vector3 screenPoint = Camera.main.WorldToScreenPoint(Shooting.Target.position);
         
         Vector2 crossHairpos = new Vector2(screenPoint.x,screenPoint.y);
         crosshair.position = crossHairpos;    

         currentSize = Mathf.Lerp(currentSize,minSize,Time.deltaTime*interpolateSpeed);
         if(currentSize <minSize +1)
          currentSize = maxSize;
         
         crosshair.sizeDelta = new Vector2(currentSize,currentSize);         
        }

         yield return null;
      }
    }

    private IEnumerator CrosshairColorChange()
    {

      while(true)
      {           

        foreach (var item in uis)
        {
           item.GetComponent<Image>().color = Color.red;
        }

        yield return new WaitForSeconds(0.5f);

          foreach (var item in uis)
        {
           item.GetComponent<Image>().color = Color.green;
        }
        yield return new WaitForSeconds(0.5f);
    
         
      }
    }

    public void SetCrosshairDefault()
    {
      crosshair.sizeDelta = defaultsize;
      currentSize = defaultsize.x;
         
      crosshair.gameObject.SetActive(false);
    }
    

     

    

     
}
