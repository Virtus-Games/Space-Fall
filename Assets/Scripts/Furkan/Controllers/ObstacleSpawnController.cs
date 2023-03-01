using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Furkan.Controllers
{
    public class ObstacleSpawnController : MonoBehaviour
    {
        [SerializeField] private ObjectPool objectPoolsController;
        [SerializeField] [CanBeNull] private Transform player;
        private int _collectableCount;
        private GameObject _obj;
        private int _objectPoolCount;
        private Vector3 _pos;
        private int lastPosZ = 0;

        private void Start()
        {
            StartCoroutine(Spawner());
        }

        void CallSpawner()
        {
            StopCoroutine(Spawner());
        }


        IEnumerator Spawner()
        {
            while (player)
            {
                if (Random.value > 0.1f)
                {
                    if (Random.value > 0.05f)
                    {
                        var rnd = Random.Range(1f, 3f);
                        _obj = objectPoolsController.GetPooledObject(0);
                        _obj.transform.localScale = new Vector3(rnd, rnd, rnd);
                        _pos = _obj.transform.position;
                    }
                    else
                    {
                        _obj = objectPoolsController.GetPooledObject(1);
                        _pos = _obj.transform.position;
                    }

                    _pos.x = Random.Range(-100, 100);
                    _pos.y = Random.Range(player.transform.position.y - 50, player.transform.position.y + 50);
                }
                else
                {
                    if (Random.value > 0.4)
                    {
                        _obj = objectPoolsController.GetPooledObject(5);
                        _pos = _obj.transform.position;
                    }
                    else
                    {
                        _obj = objectPoolsController.GetPooledObject(Random.Range(2, 5));
                        _pos = _obj.transform.position;
                    }

                    _pos.x = Random.Range(-30, 30);
                    _pos.y = Random.Range(player.transform.position.y - 20, player.transform.position.y + 20);
                }

                _pos.z = player.position.z + 50 + lastPosZ;
                _obj.transform.position = _pos;
                lastPosZ += 5;

                if (lastPosZ == 100)
                    lastPosZ = 0;


                yield return new WaitForSeconds(0.125f);
            }
        }
    }
}