using System;
using UnityEngine;

[Serializable]
public class InvestmentPropertyData
{
    #region Fields
    /// <summary>
    /// The square footage added to the property through upgrades and
    /// renovations.
    /// </summary>
    [SerializeField] protected int additionalSquareFootage = 0;
    /// <summary>
    /// The base square footage with no additions or renovations.
    /// </summary>
    [SerializeField] protected int baseSquareFootage = 0;
    /// <summary>
    /// The base valuation of the property.
    /// </summary>
    [SerializeField] protected int baseValue = 0;
    /// <summary>
    /// The list price.
    /// </summary>
    [SerializeField] protected int listPrice = 0;

    [SerializeField] protected Mortgage mortgage;


    /// <summary>
    /// The appraised value.
    /// </summary>
    [SerializeField] protected double appraisedValue = 0;
    [SerializeField] protected double annualAppreciationRate = 0.05;
    /// <summary>
    /// The base annual operating expenses expressed as a percentage of
    /// the property's gross annual income. This number does not include
    /// mortgage payments, capital expenses like upgrades, or major repairs
    /// like a new roof or HVAC.
    /// </summary>
    [SerializeField] protected double baseOperatingExpensesRate = 0.5;
    [SerializeField] protected double baseVacancyRate = 0.08;
    /// <summary>
    /// The operating expenses paid per turn.
    /// </summary>
    [SerializeField] protected double operatingExpenses = 0.0;
    /// <summary>
    /// The annual operating expenses expressed as a percentage of
    /// the property's gross annual income. This number does not include
    /// mortgage payments, capital expenses like upgrades, or major repairs
    /// like a new roof or HVAC.
    /// </summary>
    [SerializeField] protected double operatingExpensesRate = 0.0;
    /// <summary>
    /// The gross income generated per turn.
    /// </summary>
    [SerializeField] protected double rent = 0.0;
    /// <summary>
    /// The appraised value is multiplied by this number to calculate the
    /// average rent to charge per turn.
    /// </summary>
    [SerializeField] protected double baseRentMultiplier = 0.01;


    /// <summary>
    /// Return true if the property is for sale.
    /// </summary>
    [SerializeField] protected bool isForSale = true;

    #endregion

    #region Properties
    /// <summary>
    /// The square footage added to the property through upgrades and
    /// renovations.
    /// </summary>
    public int AdditionalSquareFootage => additionalSquareFootage;
    /// <summary>
    /// The appraised value.
    /// </summary>
    public double AppraisedValue => appraisedValue;
    /// <summary>
    /// The base square footage with no additions or renovations.
    /// </summary>
    public int BaseSquareFootage => baseSquareFootage;
    /// <summary>
    /// The base value of the property. This value acts as a starting point for
    /// many of the other valuation calculations.
    /// </summary>
    public int BaseValue => baseValue;
    /// <summary>
    /// The list price.
    /// </summary>
    public int ListPrice => listPrice;

    /// <summary>
    /// The annual operating expenses expressed as a percentage of
    /// the property's gross annual income. This number does not include
    /// mortgage payments, capital expenses like upgrades, or major repairs
    /// like a new roof or HVAC.
    /// </summary>
    public double OperatingExpensesRate => operatingExpensesRate;
    /// <summary>
    /// The operating expenses paid per turn.
    /// </summary>
    public double OperatingExpenses => operatingExpenses;
    /// <summary>
    /// The gross income generated per turn.
    /// </summary>
    public double Rent => rent;


    /// <summary>
    /// Return true if the property is for sale.
    /// </summary>
    public bool IsForSale => isForSale;


    public Mortgage Mortgage { get => mortgage; set => mortgage = value; }

    #endregion

    public InvestmentPropertyData()
    {

    }

    #region Methods
    /// <summary>
    /// Add more square footage to the property, usually as a result of an
    /// upgrade or renovation.
    /// </summary>
    /// <param name="value"></param>
    public void AddSquareFootage(int value)
    {
        additionalSquareFootage += value;
    }

    /// <summary>
    /// Set the appraised value.
    /// </summary>
    /// <param name="value">An integer value.</param>
    public void SetAppraisedValue(int value)
    {
        appraisedValue = value;
    }

    /// <summary>
    /// Set the base square footage. This number does not include additional
    /// renovations.
    /// </summary>
    /// <param name="value">An integer value.</param>
    public void SetBaseSquareFootage(int value)
    {
        baseSquareFootage = value;
    }

    /// <summary>
    /// Set the base value.
    /// </summary>
    /// <param name="value">An integer value.</param>
    public void SetBaseValue(int value)
    {
        baseValue = value;
    }

    /// <summary>
    /// Set to true to put the property up for sale.
    /// </summary>
    /// <param name="value"></param>
    public void SetForSale(bool value)
    {
        isForSale = value;
    }

    /// <summary>
    /// Set the list price.
    /// </summary>
    /// <param name="value">An integer value.</param>
    public void SetListPrice(int value)
    {
        listPrice = value;
    }

    /// <summary>
    /// Update the operating expenses rate, expressed as a percentage of the
    /// rental income.
    /// </summary>
    public void UpdateOperatingExpensesRate()
    {
        operatingExpensesRate = baseOperatingExpensesRate;
    }

    /// <summary>
    /// Update the operating expenses incurred while owning the property. This
    /// number is determined by multiplying the rent made during an income
    /// period by the property's operating expenses rate.
    /// </summary>
    public void UpdateOperatingExpenses()
    {
        operatingExpenses = Rent * operatingExpensesRate;
    }

    /// <summary>
    /// Calculate the rent to charge per turn. Rent is calculated as a percentage
    /// of the appraised value of the property, minus the vacancy rate.
    /// </summary>
    public void UpdateRent()
    {
        rent = AppraisedValue * baseRentMultiplier * (1 - baseVacancyRate);
    }

    /// <summary>
    /// Increase or decrease the appraised value of the property by the current
    /// annual appreciation rate, divided by the number of income periods in a
    /// year.
    /// </summary>
    public void Appreciate()
    {
        appraisedValue *= 1 + (annualAppreciationRate / 12);
    }

    /// <summary>
    /// Purchase the property and take it off the market.
    /// </summary>
    public void Purchase()
    {
        isForSale = false;
    }

    /// <summary>
    /// Sell the property and put it back on the market.
    /// </summary>
    public void Sell()
    {
        isForSale = true;
    }

    #endregion
}
