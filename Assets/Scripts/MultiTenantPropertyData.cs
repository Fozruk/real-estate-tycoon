using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New SingleFamilyHomeData", menuName = "Single-Family Home Data", order = 57)]
public class SingleFamilyHomeData : ScriptableObject
{
    public string propertyName = "New Property";
    public int minSquareFootage = 0;
    public int maxSquareFootage = 0;
}
