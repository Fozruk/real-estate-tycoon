using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New MultiTenantPropertyData", menuName = "Multi Tenant Property Data", order = 57)]
public class MultiTenantPropertyData : ScriptableObject
{
    public string propertyName = "New Property";
    public int minSquareFootage = 0;
    public int maxSquareFootage = 0;
}
