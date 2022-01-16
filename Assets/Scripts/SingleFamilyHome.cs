using System;
using UnityEngine;

public class SingleFamilyHome : RealEstateProperty
{
    [SerializeField] protected SingleFamilyHomeData _base;
    
    public SingleFamilyHomeData BaseData => _base;

    private new void Start()
    {
        base.Start();
        maxTenancy = 1;
    }

    public override void Generate()
    {
        gameObject.name = _base.propertyName;
    }
}
