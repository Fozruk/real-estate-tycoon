using System;
using System.Collections.Generic;
using UnityEngine;


public class NeighborhoodFactory
{
    public NeighborhoodFactory()
    {

    }

    public void Create(List<PropertyMarker> markers, GameObject map)
    {
        // Get a reference to the zone data.
        PropertyZoneData zoneData = markers[0].ZoneData;
        // Get total number of properties.
        int totalProperties = markers.Count;
        // Get total ratio.
        int totalRatio = zoneData.ClassAPropertyRatio
            + zoneData.ClassBPropertyRatio
            + zoneData.ClassCPropertyRatio;
        // Determine how many class A, B, and C properties to create.
        int classAPropertiesRatio = (int)((double)zoneData.ClassAPropertyRatio / totalRatio * totalProperties);
        int classBPropertiesRatio = (int)((double)zoneData.ClassBPropertyRatio / totalRatio * totalProperties);
        int classCPropertiesRatio = (int)((double)zoneData.ClassCPropertyRatio / totalRatio * totalProperties);

        // Get leftovers from rounding errors.
        int leftovers = totalProperties
            - classAPropertiesRatio
            - classBPropertiesRatio
            - classCPropertiesRatio;

        // Add leftovers to Class C properties.
        classCPropertiesRatio += leftovers;

        Debug.Log($"Creating {classAPropertiesRatio} Class A properties");
        Debug.Log($"Creating {classBPropertiesRatio} Class B properties");
        Debug.Log($"Creating {classCPropertiesRatio} Class C properties");

        // Randomize which properties belong to which class.
        List<PropertyClassRating> ratings = new List<PropertyClassRating>();
        for (int i = 0; i < classAPropertiesRatio; i++)
        {
            ratings.Add(PropertyClassRating.A);
        }
        for (int i = 0; i < classBPropertiesRatio; i++)
        {
            ratings.Add(PropertyClassRating.B);
        }
        for (int i = 0; i < classCPropertiesRatio; i++)
        {
            ratings.Add(PropertyClassRating.C);
        }

        ratings.Shuffle();

        InvestmentPropertyFactory factory = new InvestmentPropertyFactory();
        for (int i = 0; i < markers.Count; i++)
        {
            factory.NewProperty(markers[i], map, ratings[i]);
        }
    }
}
