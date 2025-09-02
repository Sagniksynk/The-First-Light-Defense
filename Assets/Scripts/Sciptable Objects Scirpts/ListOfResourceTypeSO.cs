using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ListOfResourceTypeSO")]
public class ListOfResourceTypeSO : ScriptableObject
{
    public List<ResourceTypeSO> resourceTypes;
}
