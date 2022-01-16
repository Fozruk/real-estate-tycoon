using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum PropertyTypesID
{
    None, SingleFamilyHomes, MultiTenant
}


public class CreateInvestmentPropertyStrategy : MonoBehaviour
{
    public CreateInvestmentPropertyStrategy() { }

    public virtual RealEstateProperty Create(PropertyMarker marker, GameObject map, PropertyClassRating rating)
    {
        return null;
    }
}


public class CreateSingleFamilyHomeStrategy : CreateInvestmentPropertyStrategy
{
    private readonly Type[] availableUpgrades = new Type[]
        {
            typeof(KitchenRemodel),
            typeof(BathroomRemodel),
            typeof(Landscaping),
            typeof(PorchAddition)
        };

    public CreateSingleFamilyHomeStrategy() : base() { }

    public override RealEstateProperty Create(PropertyMarker marker, GameObject map, PropertyClassRating rating)
    {
        GameObject newProperty = Instantiate(
            GetRandomBaseProperty(marker.AllowedPropertyTypes),
            marker.transform.position,
            Quaternion.identity,
            map.transform);

        SingleFamilyHome p = newProperty.GetComponent<SingleFamilyHome>();

        p.SetPropertyMarker(marker);
        p.SetClassRating(rating);
        p.SetBaseSquareFootage(CalculateBaseSquareFootage(p));
        p.SetBaseValue(CalculateBaseValue(p));
        p.SetPricePerSquareFoot(CalculatePricePerSquareFoot(p));
        p.SetAppraisedValue(CalculateAppraisedValue(p));
        p.SetListPrice(CalculateAskingPrice(p));
        p.SetMinimumDownPayment(CalculateMinimumDownPayment(p));
        p.SetClosingCosts(CalculateClosingCosts(p));
        p.SetClassRatingIncomeMultiplier(IncomeMultiplier(p));
        p.SetAvailableUpgrades(CreateAvailableUpgradesList(p));

        return p;
    }

    private GameObject GetRandomBaseProperty(List<GameObject> propertyList)
    {
        WeightedRandomBag bag = new WeightedRandomBag();

        foreach (var p in propertyList)
        {
            bag.addEntry(p, 1);
        }

        return (GameObject)bag.getRandom();
    }

    private List<PropertyUpgrade> CreateAvailableUpgradesList(RealEstateProperty p)
    {
        System.Random rand = new System.Random();
        List<int> selectedUpgrades = new List<int>();
        switch (p.ClassRating)
        {
            case PropertyClassRating.C:
                Debug.Log("Generating Class C upgrades");
                selectedUpgrades.AddRange(Enumerable.Range(0, availableUpgrades.Length)
                    .OrderBy(i => rand.Next())
                    .Take(4));
                break;
            default:
                break;
        }

        List<PropertyUpgrade> result = new List<PropertyUpgrade>();
        foreach (int upgrade in selectedUpgrades)
        {
            switch (upgrade)
            {
                case 0:
                    result.Add(new KitchenRemodel(p));
                    break;
                case 1:
                    result.Add(new BathroomRemodel(p));
                    break;
                case 2:
                    result.Add(new Landscaping(p));
                    break;
                case 3:
                    result.Add(new PorchAddition(p));
                    break;
                default:
                    break;
            }
        }

        return result;
    }

    /// <summary>
    /// Set the base square footage of the property. This number is randomly
    /// generated based on the min/max square footage allowed by the property
    /// type and by the Property Marker.
    /// </summary>
    /// <param name="p">A Single Family Home.</param>
    /// <returns>The base square footage of a property.</returns>
    private int CalculateBaseSquareFootage(SingleFamilyHome p)
    {
        return UnityEngine.Random.Range(
            p.BaseData.minSquareFootage,
            Math.Min(p.BaseData.maxSquareFootage,
                p.PropertyMarker.MaxSquareFootage));
    }

    /// <summary>
    /// Set the price per square foot. This number is randomly generated.
    /// </summary>
    /// <param name="p">A Single Family Home.</param>
    /// <returns>A property's price per square foot.</returns>
    private int CalculatePricePerSquareFoot(SingleFamilyHome p)
    {
        return p.BaseValue / p.BaseSquareFootage;
    }

    /// <summary>
    /// Set the base value of the property. This is calculated based on the base
    /// square footage of the property and its price per square foot. This
    /// number is then used to calculate the base appraised value.
    /// </summary>
    /// <param name="p">A Single Family Home.</param>
    /// <returns>The base value of a property.</returns>
    private int CalculateBaseValue(SingleFamilyHome p)
    {
        return UnityEngine.Random.Range(
            p.PropertyMarker.MinPropertyValue,
            p.PropertyMarker.MaxPropertyValue);
    }

    /// <summary>
    /// Set the base appraised value of the property. This number is randomly
    /// generated based on the property's class rating.
    /// </summary>
    /// <param name="p">A Single Family Home.</param>
    /// <returns>The appraised value of a property.</returns>
    private int CalculateAppraisedValue(SingleFamilyHome p)
    {
        double minValueMultiplier = 0;
        double maxValueMultiplier = 0;

        switch (p.ClassRating)
        {
            case PropertyClassRating.A:
                minValueMultiplier = 1.0;
                maxValueMultiplier = 1.25;
                break;
            case PropertyClassRating.B:
                minValueMultiplier = 0.9;
                maxValueMultiplier = 1.1;
                break;
            case PropertyClassRating.C:
                minValueMultiplier = 0.8;
                maxValueMultiplier = 1.05;
                break;
            default:
                break;
        }

        return UnityEngine.Random.Range(
            (int)(p.BaseValue * minValueMultiplier),
            (int)(p.BaseValue * maxValueMultiplier));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p">A Single Family Home.</param>
    /// <returns>A property's asking price.</returns>
    private int CalculateAskingPrice(SingleFamilyHome p)
    {
        return UnityEngine.Random.Range((int)(p.AppraisedValue * 0.9), (int)(p.AppraisedValue * 1.1));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p">A Single Family Home.</param>
    /// <returns>A property's minimum downpayment if it is not bought at
    /// full asking price.</returns>
    private int CalculateMinimumDownPayment(SingleFamilyHome p)
    {
        return (int)(p.AppraisedValue * 0.2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p">A Single Family Home.</param>
    /// <returns>The closing costs associated with purchasing a property.</returns>
    private int CalculateClosingCosts(SingleFamilyHome p)
    {
        // Escrow and pre-paid taxes. Between 0.8-0.9% of appraised value.
        int escrow = UnityEngine.Random.Range((int)(p.AppraisedValue * 0.008), (int)(p.AppraisedValue * 0.009));
        int oneTimeClosingCosts = (int)(.003 * p.AppraisedValue);
        return escrow + oneTimeClosingCosts;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private double IncomeMultiplier(SingleFamilyHome p)
    {
        switch (p.ClassRating)
        {
            case PropertyClassRating.A:
                return 0.003;
            case PropertyClassRating.B:
                return 0.0023;
            case PropertyClassRating.C:
                return 0.0015;
            default:
                break;
        }

        return 0;
    }
}


public class InvestmentPropertyFactory
{
    [SerializeField] private List<GameObject> singleFamilyHomes = new List<GameObject>();
    [SerializeField] private CreateInvestmentPropertyStrategy propertyCreationStrategy;

    public InvestmentPropertyFactory() { }

    public RealEstateProperty NewProperty(PropertyMarker marker, GameObject map, PropertyClassRating rating)
    {
        switch (marker.PropertyTypes)
        {
            case PropertyTypesID.SingleFamilyHomes:
                propertyCreationStrategy = new CreateSingleFamilyHomeStrategy();
                break;
            default:
                break;
        }

        return propertyCreationStrategy.Create(marker, map, rating);
    }
}
