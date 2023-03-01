using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
     [SerializeField] private Image[] LazerBulletImage, healthImage, shieldImage;
     [SerializeField] private GameObject EnergyPanel;
     [SerializeField] private RawImage mapRawImage;

     public bool isGameStart = false;
     public float time = 4;
     public float newTime = 4;
     public float refuseCount = .2f;

     private void OnEnable() => CollectibeData.OnUpdateCollectionDataEvent += OnCollectionDataEvent;
     private void OnDisable() => CollectibeData.OnUpdateCollectionDataEvent -= OnCollectionDataEvent;

     private void Awake() => Initialize();



     public void Initialize()
     {
          EnergyPanel.SetActive(true);
          _healthFillAmount = healthImage.Length - 1;
          _lazerBulletFillAmount = LazerBulletImage.Length - 1;
          _shieldFillAmount = 1;
          time = newTime;

          for (int i = 0; i < _healthFillAmount; i++)
          {
               healthImage[i].fillAmount = 1;
          }

          for (int i = 0; i < _lazerBulletFillAmount; i++)
          {
               LazerBulletImage[i].fillAmount = 1;
          }

          for (int i = 0; i < _shieldFillAmount; i++)
          {
               shieldImage[i].fillAmount = 1;
          }

     }


     internal void HealthController(bool val)
     {
          if (val)
          {
               healthImage[_healthFillAmount].fillAmount += 1;
               if (healthImage[_healthFillAmount].fillAmount == 1)
                    _healthFillAmount++;
          }
          else
          {
               healthImage[_healthFillAmount].fillAmount -= 1f;
               if (healthImage[_healthFillAmount].fillAmount == 0)
                    _healthFillAmount--;
          }

          _healthFillAmount = Mathf.Clamp(_healthFillAmount, 0, healthImage.Length - 1);
     }


     internal void ShieldController(bool val)
     {
          if (val)
          {
               shieldImage[_shieldFillAmount].fillAmount += 1;
               if (shieldImage[_shieldFillAmount].fillAmount == 1)
                    _shieldFillAmount++;
          }
          else
          {
               shieldImage[_shieldFillAmount].fillAmount -= 1f;
               if (shieldImage[_shieldFillAmount].fillAmount == 0)
                    _shieldFillAmount--;
          }

          _shieldFillAmount = Mathf.Clamp(_shieldFillAmount, 0, shieldImage.Length - 1);
     }


     internal void LazerBulletController(bool val)
     {
          if (val)
          {
               LazerBulletImage[_lazerBulletFillAmount].fillAmount += 1;
               if (LazerBulletImage[_lazerBulletFillAmount].fillAmount == 1)
                    _lazerBulletFillAmount++;
          }
          else
          {
               LazerBulletImage[_lazerBulletFillAmount].fillAmount -= 1f;
               if (LazerBulletImage[_lazerBulletFillAmount].fillAmount == 0)
                    _lazerBulletFillAmount--;
          }

          _lazerBulletFillAmount = Mathf.Clamp(_lazerBulletFillAmount, 0, LazerBulletImage.Length - 1);
     }



     private int _healthFillAmount, _shieldFillAmount, _lazerBulletFillAmount;
     private void OnCollectionDataEvent(bool val)
     {

     }



     private void Update()
     {
          if (isGameStart)
          {
               time -= Time.deltaTime;
               if (time <= 0)
               {
                    time = newTime;

                    healthImage[_healthFillAmount].fillAmount -= refuseCount;

                    if (healthImage[_healthFillAmount].fillAmount == 0)
                         _healthFillAmount--;

                    _healthFillAmount = Mathf.Clamp(_healthFillAmount, 0, healthImage.Length - 1);

                    if (_healthFillAmount == 0)
                    {
                         isGameStart = false;
                         // ! UI Game Over
                    }
               }
          }
     }
}
