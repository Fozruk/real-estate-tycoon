using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropertyMarker : MonoBehaviour
{
    /// <summary>
    /// Provides information about the zone.
    /// </summary>
    [SerializeField] private PropertyZoneData zoneData;
    /// <summary>
    /// The type of investment properties allowed at this marker.
    /// </summary>
    [SerializeField] private PropertyTypesID propertyTypesID = PropertyTypesID.None;

    /// <summary>
    /// The max square footage allowed for the property created at this marker.
    /// </summary>
    public int MaxSquareFootage => zoneData.MaxSquareFootage;
    public int MinPropertyValue => zoneData.MinPropertyValue;
    public int MaxPropertyValue => zoneData.MaxPropertyValue;
    public string ZoneName => zoneData.ZoneName;
    public PropertyZoneData ZoneData => zoneData;
    /// <summary>
    /// The category of investment properties allowed at this marker.
    /// </summary>
    public PropertyTypesID PropertyTypes => propertyTypesID;
    /// <summary>
    /// A list of property types allowed at this marker.
    /// </summary>
    public List<GameObject> AllowedPropertyTypes => zoneData.AllowedPropertyTypes;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
