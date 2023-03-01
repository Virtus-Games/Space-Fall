using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeObjects : MonoBehaviour
{
   [SerializeField] private TypeCollectionType typeCollectionType;

   public TypeCollectionType TypeCollectionType()
   {
      return typeCollectionType;
   }
}
