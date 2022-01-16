using System;
using UnityEngine;


public class MultiTenantProperty : RealEstateProperty
{
    private readonly int MIN_TENANTS = 6;
    private readonly int MAX_TENANTS = 450;
    private readonly int MIN_UNIT_SIZE = 500;
    private readonly int MAX_UNIT_SIZE = 1200;
    private readonly int MIN_PSF = 95;
    private readonly int MAX_PSF = 500;
    private readonly int MIN_ISF = 10;
    private readonly int MAX_ISF = 37;
    private readonly double MIN_DOWNPAYMENT = 0.25;

    [SerializeField] int incomePerSquareFoot = 0;

    public int IncomePerSquareFoot => incomePerSquareFoot;

    private new void Start()
    {
        base.Start();
    }

    public override void Generate()
    {
        maxTenancy = CalculateMaxNumberOfTenants();
        baseSquareFootage = CalculateBaseSquareFootage();
        pricePerSquareFoot = CalculatePricePerSquareFoot();
        baseValue = CalculateBaseValue();
        appraisedValue = CalculateAppraisedValue();
        listPrice = CalculateAskingPrice();
        minimumDownPayment = CalculateMinimumDownPayment();
        incomePerSquareFoot = CalculateIncomePerSquareFoot();
    }

    /// <summary>
    /// Small apartment is 6 units, large can be up to 450
    /// </summary>
    /// <returns></returns>
    private int CalculateMaxNumberOfTenants()
    {
        return UnityEngine.Random.Range(MIN_TENANTS, MAX_TENANTS);
    }

    /// <summary>
    /// Small units are 400, larger units are 2000.
    /// Unit size is usually inversely proportional to the number of units.
    /// </summary>
    /// <returns></returns>
    private int CalculateBaseSquareFootage()
    {
        return UnityEngine.Random.Range(
            maxTenancy * MIN_UNIT_SIZE, maxTenancy * MAX_UNIT_SIZE);
    }

    private int CalculatePricePerSquareFoot()
    {
        return UnityEngine.Random.Range(MIN_PSF, MAX_PSF);
    }

    private int CalculateBaseValue()
    {
        return baseSquareFootage * pricePerSquareFoot;
    }

    private int CalculateAppraisedValue()
    {
        return UnityEngine.Random.Range(
            (int)(baseValue * 0.8), (int)(baseValue * 1.25));
    }

    private int CalculateAskingPrice()
    {
        return UnityEngine.Random.Range(
            (int)(appraisedValue * 0.9), (int)(appraisedValue * 1.1));
    }

    private int CalculateMinimumDownPayment()
    {
        return (int)(appraisedValue * MIN_DOWNPAYMENT);
    }

    /// <summary>
    /// Min 10, Max 37
    /// </summary>
    /// <returns></returns>
    private int CalculateIncomePerSquareFoot()
    {
        return UnityEngine.Random.Range(MIN_ISF, MAX_ISF);
    }
}
