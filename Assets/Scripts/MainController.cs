using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MainController : MonoBehaviour
{
    [SerializeField] private Data data;

    [SerializeField] private List<GameObject> singleFamilyHomes = new List<GameObject>();
    [SerializeField] private List<GameObject> allProperties = new List<GameObject>();
    [SerializeField] private Bank theBank = new Bank();

    private void Awake()
    {
        data = new Data();
    }

    // Use this for initialization
    void Start()
    {
        // Create new property data.
        InvestmentPropertyData newProperty = new InvestmentPropertyData();

        // Set some attributes.
        newProperty.SetBaseValue(100_000);
        newProperty.SetListPrice(100_000);
        newProperty.SetAppraisedValue(100_000);
        newProperty.UpdateRent();
        newProperty.UpdateOperatingExpensesRate();
        newProperty.UpdateOperatingExpenses();

        // Pass the data to the InvestmentProperty.
        GameObject
            .Find("InvestmentProperty")
            .GetComponent<InvestmentProperty>()
            .SetPropertyData(newProperty);

        // Add the property to the market.
        data.AddProperty(newProperty);

        //GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMap()
    {
        GameObject map = GameObject.Find("Map");

        // Collect all of the property markers.
        GameObject[] markers = CollectPropertyMarkers();

        // Generate neighborhoods.
        NeighborhoodFactory neighborhoodFactory = new NeighborhoodFactory();

        foreach (var neighborhood in CollectNeighborhoodData(markers))
        {
            neighborhoodFactory.Create(neighborhood.Value, map);
        }
    }

    private GameObject[] CollectPropertyMarkers()
    {
        return GameObject.FindGameObjectsWithTag("PropertyMarker");
    }

    private Dictionary<string, List<PropertyMarker>> CollectNeighborhoodData(GameObject[] markers)
    {
        // Iterate over the markers and organize them by zone name.
        Dictionary<string, List<PropertyMarker>> neighborhoods =
            new Dictionary<string, List<PropertyMarker>>();
        foreach (var m in markers)
        {
            PropertyMarker marker = m.GetComponent<PropertyMarker>();
            if (!neighborhoods.ContainsKey(marker.ZoneName))
            {
                neighborhoods[marker.ZoneName] = new List<PropertyMarker>();
            }

            neighborhoods[marker.ZoneName].Add(marker);
        }

        return neighborhoods;
    }

    /// <summary>
    /// Buy an investment property. The down payment will be removed from your
    /// account and a new mortgage issued if the property wasn't purchased
    /// in full.
    /// </summary>
    /// <param name="d">An investment property.</param>
    public void BuyInvestmentProperty(InvestmentPropertyData d, double downpayment)
    {
        // Issue a new mortgage.
        d.Mortgage = theBank.Issue30YearMortgage(
            propertyData: d,
            offerPrice: d.ListPrice,
            downPayment: downpayment);
        // Purchase the property and take it off the market.
        d.Purchase();
        // Pay for the down payment.
        data.RemoveFunds(d.Mortgage.DownPayment);
        //// Add the mortgage to our list of liabilities.
        //data.AddMortgage(mortgage);
        // Update our net worth.
        UpdateNetWorth();
    }

    //public void BuyProperty(RealEstateProperty p, int offerValue)
    //{
    //    double TMP_LOAN_INTEREST_RATE = 0.0275;

    //    try
    //    {
    //        data.RemoveFunds(p.MinimumDownPayment);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError(e);
    //        Debug.Log($"Can't afford to buy property.");
    //        return;
    //    }

    //    PropertyLoan loan = new PropertyLoan(offerValue - p.MinimumDownPayment, TMP_LOAN_INTEREST_RATE);
        
        
    //    p.PurchaseProperty(
    //        new PropertyPurchaseData(
    //            offerValue: offerValue, loan: loan));

        //// Buy all upgrades.
        //foreach (var upgrade in p.Upgrades)
        //{
        //    if (upgrade.State.GetType() == typeof(PlanningState))
        //    {
        //        Debug.Log($"Buying the {upgrade.Name} upgrade.");
        //        data.RemoveFunds(upgrade.Price);
        //        upgrade.StartConstruction();
        //    }
        //    else if (upgrade.State.GetType() == typeof(UnderConstructionState))
        //}

    //    data.AddProperty(p);
    //}

    public void FinishWeek()
    {
        // Get income from tenants and remove expenses from wallet.
        foreach (var p in data.InvestmentProperties)
        {
            p.Appreciate();
            p.UpdateRent();
            p.UpdateOperatingExpenses();

            if (p.IsForSale) { continue; }

            data.AddFunds(p.Rent);
            data.RemoveFunds(p.OperatingExpenses);
            p.Mortgage.MakeMinimumPayment();
            data.RemoveFunds(p.Mortgage.MinimumPayment);
        }

        //foreach (var m in data.Mortgages)
        //{
        //    m.MakeMinimumPayment();
        //    data.RemoveFunds(m.MinimumPayment);
        //}

        UpdateNetWorth();
    }

    private void UpdateNetWorth()
    {
        double cash = data.Wallet;
        double assets = 0;
        double liabilities = 0;
        double income = 0;
        double expenses = 0;

        foreach (var p in data.InvestmentProperties)
        {
            if (p.IsForSale) { continue; }
            assets += p.AppraisedValue;
            income += p.Rent;
            expenses += p.OperatingExpenses + p.Mortgage.MinimumPayment;
            liabilities += p.Mortgage.OutstandingBalance;
        }

        //foreach (var m in data.Mortgages)
        //{
        //    liabilities += m.OutstandingBalance;
        //    expenses += m.MinimumPayment;
        //}

        data.SetWeeklyIncome(income);
        data.SetWeeklyExpenses(expenses);
        data.SetNetWorth(cash + assets - liabilities);
        data.AddTotalIncome(income);
        data.AddTotalExpenses(expenses);
    }

    /// <summary>
    /// Calculate a random rent payment within 10% of a property's projected weekly NOI.
    /// This should be handled differently for residential properties with multiple tenants.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private int CalculateRandomTenantRent(RealEstateProperty p)
    {
        if (p.GetType() == typeof(SingleFamilyHome))
        {
            //// Calculate a random value within +-5% of the estimated weekly income.
            //return UnityEngine.Random.Range((int)(p.EstimatedWeeklyIncome * 0.95), (int)(p.EstimatedWeeklyIncome * 1.05));
            return p.EstimatedMonthlyIncome;
        }

        var m = p as MultiTenantProperty;

        return m.BaseSquareFootage / m.MaxTenancy * m.IncomePerSquareFoot / 52;
    }
}