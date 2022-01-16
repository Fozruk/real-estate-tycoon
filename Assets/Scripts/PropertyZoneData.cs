using System;
using System.Collections.Generic;
using UnityEngine;

public class PropertyZoneData : ScriptableObject
{
    [SerializeField] protected string zoneName = "New Zone";
    [SerializeField] protected int maxSquareFootage = 0;
    [SerializeField] private List<GameObject> allowedPropertyTypes;
    [SerializeField] private int minPropertyValue = 0;
    [SerializeField] private int maxPropertyValue = 0;
    [SerializeField] private int classAPropertyRatio = 0;
    [SerializeField] private int classBPropertyRatio = 0;
    [SerializeField] private int classCPropertyRatio = 0;

    public string ZoneName => zoneName;
    public int MaxSquareFootage => maxSquareFootage;
    public int MinPropertyValue => minPropertyValue;
    public int MaxPropertyValue => maxPropertyValue;
    public int ClassAPropertyRatio => classAPropertyRatio;
    public int ClassBPropertyRatio => classBPropertyRatio;
    public int ClassCPropertyRatio => classCPropertyRatio;
    public List<GameObject> AllowedPropertyTypes => allowedPropertyTypes;
}
