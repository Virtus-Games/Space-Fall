using UnityEngine;


[System.Serializable]
public enum TypeCollectionType
{
     HEALTH,
     SHIELD,
     LAZERBULLET
}


/// <summary>
/// Collectible data Use Singleton Pattern @EXAM: CollectibeData.Instance...
/// </summary>
public class CollectibeData : Singleton<CollectibeData>
{

     private int _healthCount, _shieldCount = 1, _lazerBulletCount = 10;
     public delegate void OnCollectionDataEvent(bool val);
     public static event OnCollectionDataEvent OnUpdateCollectionDataEvent;

     public void OnUpdateDataEvent(bool val) => OnUpdateCollectionDataEvent?.Invoke(val);

     #region Collection Controller



     /// <summary>
     /// Add the collectible data @FURKAN 
     /// </summary>
     internal void AddCollectData(TypeCollectionType collectionType, int count = 1)
     {
          switch (collectionType)
          {
               case TypeCollectionType.HEALTH:
                    _healthCount += count;
                    _healthCount = Mathf.Min(10, _healthCount);
                    UIManager.Instance.HealthController(true);
                    break;
               case TypeCollectionType.SHIELD:
                    _shieldCount += count;
                    _shieldCount = Mathf.Min(5, _shieldCount);
                    UIManager.Instance.ShieldController(true);
                    break;

               case TypeCollectionType.LAZERBULLET:
                    _lazerBulletCount += count;
                    _lazerBulletCount = Mathf.Min(10, _lazerBulletCount);
                    UIManager.Instance.LazerBulletController(true);
                    break;
               default:
                    break;
          }

          OnUpdateDataEvent(true);
     }

     // summary>
     // Remove the collectible data @YAĞIZ YELEGEN
     // </summary>
     internal void RemoveCollectData(TypeCollectionType collectionType, int specialCount = 1)
     {
          switch (collectionType)
          {
               case TypeCollectionType.HEALTH:
                    _healthCount -= specialCount;
                    _healthCount = Mathf.Clamp(_healthCount, 0, 100);
                    UIManager.Instance.HealthController(false);
                    break;
               case TypeCollectionType.SHIELD:
                    _shieldCount -= specialCount;
                    _shieldCount = Mathf.Max(0, _shieldCount);
                    UIManager.Instance.ShieldController(false);
                    break;
               case TypeCollectionType.LAZERBULLET:
                    _lazerBulletCount -= specialCount;
                    _lazerBulletCount = Mathf.Max(0, _lazerBulletCount);
                    UIManager.Instance.LazerBulletController(false);
                    break;
               default:
                    break;
          }

          OnUpdateDataEvent(false);
     }




     /// <summary>
     /// Get the count of the collectible data @YAĞIZ  @Mehmet
     /// </summary>
     internal int GetCountWithEnumData(TypeCollectionType collectionType)
     {
          switch (collectionType)
          {
               case TypeCollectionType.HEALTH:
                    return _healthCount;
               case TypeCollectionType.SHIELD:
                    return _shieldCount;
               case TypeCollectionType.LAZERBULLET:
                    return _lazerBulletCount;
               default:
                    return 0;
          }
     }

     #endregion
}
