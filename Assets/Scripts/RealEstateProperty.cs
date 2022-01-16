using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Commercial Property class determines a few things.
///
/// Appreciation potential.
///     Class A properties are higher-end and require fewer updates.
///     May be affected by economic recession when high-income
///     earners lose their jobs? Interesting...
///
///     Class B have lower income from tenants than Class A.
///         - Class can be upgraded by improvements: remodels,
///           renovations, etc.
///     Class C
///         - Generally require a number of renovations and
///           infrastructure improvements.
///         - Lowest rental rates
///         - Have a higher potential for appreciation
///         - Require more upfront capital to bring cashflow
/// </summary>
public enum CommercialPropertyClassRating
{
    None, A, B, C
}

/// <summary>
/// Same deal as above. Should help in generating properties.
/// </summary>
public enum PropertyClassRating
{
    None, A, B, C
}

/* Apartment buildings may have room for additonal upgrades:
 *  - Gym
 *  - Pool
 *  - Barbecue Areas
 *  - Playground
 *  - Clubhouse
 *  - Dog Park
 *  - 
 * 
 * 
 * 
 * 
 */

/* Properties are listed for a price.
 *  - Some properties are listed for below/above their appraisal value.
 *  
 * You put an offer on a property. The number of existing offers indicates
 *  how aggressive you need to be in your offer.
 *  - 0-2 offers means you can offer within +0-5% of the list price
 *  - 3-5 offers means you can offer +5-10% of the list price
 *  - 6+  offers means you need to offer 20%+
 */

/* The longer a property sit on the market, the lower its list price becomes.
 * - Every week a property remains on the market, there is a small chance of a
 *   price drop. The player doesn't know how long the property will stay on the
 *   market so they need to play their cards right.
 * - A property can stay on the market anywhere from 4 weeks to 26 weeks (6 months)
 * - A property also has a chance to get additional offers over time. The higher
 *   the number of offers, the greater the chance it's taken off the market. If
 *   you see more than 2 offers on a property, you better make your move!
 */

/* Should there be a leveling system? You gain experience from any deal, good
 * or bad. Profitable deals gain more exp than losses. Experience over time
 * unlocks...what?
 *  - Other property types? Commercial, multi-tenant, hotels, etc.?
 * 
 */

/* The real estate market generates all of the available properties at once.
 * - Then, some subset of the properties are marked for sale.
 * - Select a property and buy it.
 * - Step one:
 *  - Buy it with a 20% down payment.
 * 
 */

public class RealEstateProperty : MonoBehaviour
{
    [SerializeField] protected PropertyMarker propertyMarker;
    [SerializeField] protected InvestmentPropertyData propertyData;
    [SerializeField] protected PropertyClassRating classRating = PropertyClassRating.C;
    [SerializeField] protected double classRatingIncomeMultiplier = 0;
    /// <summary>
    /// The number of weeks in the calendar year.
    /// </summary>
    protected readonly int WEEKS_IN_A_YEAR = 52;
    /// <summary>
    /// The number of months in the calendar year.
    /// </summary>
    protected readonly int MONTHS_IN_A_YEAR = 12;

    [Header("Property Valuation")]
    /// <summary>
    /// The current appraised value.
    /// </summary>
    [SerializeField] protected int appraisedValue = 0;
    /// <summary>
    /// The list price.
    /// </summary>
    [SerializeField] protected int listPrice = 0;
    /// <summary>
    /// The base value.
    /// </summary>
    [SerializeField] protected int baseValue = 0;
    /// <summary>
    /// Closing costs required when buying the property.
    /// </summary>
    [SerializeField] protected int closingCosts;
    /// <summary>
    /// The total equity.
    /// </summary>
    [SerializeField] protected float equity = 0;
    /// <summary>
    /// The minimum down payment required to purchase the property.
    /// </summary>
    [SerializeField] protected int minimumDownPayment = 0;
    /// <summary>
    /// The price per square foot.
    /// </summary>
    [SerializeField] protected int pricePerSquareFoot = 0;

    [Header("Property Attributes")]
    /// <summary>
    /// The square footage of the property before additions and renovations.
    /// </summary>
    [SerializeField] protected int baseSquareFootage = 0;
    /// <summary>
    /// The maximum number of tenants.
    /// </summary>
    [SerializeField] protected int maxTenancy = 0;
    /// <summary>
    /// The total operating expenses required expressed as a percentage of the
    /// purchase price of the property.
    /// </summary>
    [SerializeField] protected double operatingExpenses = 0.01;
    /// <summary>
    /// The property tax rate expressed a percentage of the purchase price of
    /// the property.
    /// </summary>
    [SerializeField] protected double propertyTaxRate = 0.01;
    /// <summary>
    /// A list of upgrades available to the property.
    /// </summary>
    [SerializeField] protected List<PropertyUpgrade> upgrades = new List<PropertyUpgrade>();

    [Header("Purchase Data")]
    /// <summary>
    /// The down payment made when the property was purchased.
    /// </summary>
    [SerializeField] protected int downPayment = 0;
    /// <summary>
    /// The price paid for the property.
    /// </summary>
    [SerializeField] protected int purchasePrice = 0;
    /// <summary>
    /// The base appreciation rate of the property's value over one year.
    /// </summary>
    [SerializeField] protected double baseAppreciationRate = 0.035;
    /// <summary>
    /// The loan associated with the property.
    /// </summary>
    [SerializeField] protected PropertyLoan loan;
    /// <summary>
    /// The total income generated.
    /// </summary>
    [SerializeField] protected int totalIncome = 0;
    /// <summary>
    /// The net income generated after expenses.
    /// </summary>
    [SerializeField] protected int netIncome = 0;
    /// <summary>
    /// The total expenses paid maintaining the property.
    /// </summary>
    [SerializeField] protected int totalExpensesPaid = 0;
    [SerializeField] protected double estimatedVacancyRate = 0;
    [SerializeField] protected int estimatedMonthlyIncome = 0;
    [SerializeField] protected int estimatedMonthlyOperatingCost = 0;
    [SerializeField] protected int estimatedMonthlyLoanPayment = 0;
    [SerializeField] protected int estimatedMonthlyNOI = 0;
    [SerializeField] protected double estimatedCashROI = 0.0f;
    [SerializeField] protected double estimatedCapitalizationRate = 0;

    public int EstimatedMonthlyIncome => estimatedMonthlyIncome;
    public int EstimatedMonthlyOperatingCost => estimatedMonthlyOperatingCost;
    public PropertyMarker PropertyMarker { get => propertyMarker; set => propertyMarker = value; }
    public PropertyClassRating ClassRating => classRating;
    public double ClassRatingIncomeMultiplier { get => classRatingIncomeMultiplier; set => classRatingIncomeMultiplier = value; }
    public int BaseValue { get => baseValue; set => baseValue = value; }
    public int AppraisedValue { get => appraisedValue; set => appraisedValue = value; }
    public int ListPrice { get => listPrice; set => listPrice = value; }
    public int BaseSquareFootage { get => baseSquareFootage; set => baseSquareFootage = value; }
    public int PricePerSquareFoot { get => pricePerSquareFoot; set => pricePerSquareFoot = value; }
    public int DownPayment => downPayment;
    public int MinimumDownPayment { get => minimumDownPayment; set => minimumDownPayment = value; }
    public int ClosingCosts { get => closingCosts; set => closingCosts = value; }
    public int PurchasePrice => purchasePrice;
    public double OperatingExpenses => operatingExpenses;
    public double PropertyTaxRate => propertyTaxRate;
    public double BaseAppreciationRate => baseAppreciationRate;
    public int TotalExpensesPaid => totalExpensesPaid;
    public int TotalIncome => totalIncome;
    public int NetIncome => netIncome;
    public PropertyLoan Loan => loan;
    public List<PropertyUpgrade> Upgrades { get => upgrades; set => upgrades = value; }
    public int MaxTenancy => maxTenancy;

    public void Start()
    {
        // Set the estimated vacancy rate. This number affects all of the other
        // estimates.
        SetEstimatedVacancyRate();
        // Set the estimated monthly income.
        SetEstimatedMonthlyIncome();
        // Set the estimated monthly operating cost.
        SetEstimatedMonthlyOperatingCost();
        // Set estimated monthly loan payments.
        SetEstimatedMonthlyLoanPayment();
        // Set estimated monthly NOI.
        SetEstimatedMonthlyNOI();
        // Set estimated cash ROI.
        SetEstimatedCashROI();
        // Set estimated capitalization rate.
        SetEstimatedCapitalizationRate();
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// The estimated vacancy rate of a rental. The national average is 7%.
    /// A 'good' vacancy rate is 4% or less. Bad vacancy rates can be anywhere
    /// from 8-15%? Even higher? Adding upgrades and renovations should either
    /// increase the value of the property or improve vacancy rates.
    /// For simplicity, the property is considered "always rented out", but
    /// your income can be improved by decreasing the vacancy rate.
    /// </summary>
    private void SetEstimatedVacancyRate()
    {
        estimatedVacancyRate = 0.07;
    }

    /// <summary>
    /// Set the estimated monthly rent you can charge a tenant. Should be
    /// somewhere between 0.8-1.1% of the property's value per month.
    /// </summary>
    private void SetEstimatedMonthlyIncome()
    {
        double appraisedValueMultiplier = 0.01;
        estimatedMonthlyIncome = (int)(appraisedValue * appraisedValueMultiplier * (1 - estimatedVacancyRate));
    }

    /// <summary>
    /// Set the estimated annual operating expenses for a property. Expressed
    /// as a percentage of the property's value. In this case, let's use the
    /// asking value of the home to estimate total expenses, and then divide
    /// by the number of months in the year.
    /// </summary>
    private void SetEstimatedMonthlyOperatingCost()
    {
        double multiplier = 0.01;
        double annualOperatingExpenses = listPrice * multiplier;
        estimatedMonthlyOperatingCost = (int)(annualOperatingExpenses / 12);
    }

    /// <summary>
    /// Set the estimated monthly loan payment, assuming a minimum down payment
    /// of 20%.
    /// </summary>
    private void SetEstimatedMonthlyLoanPayment()
    {
        int MONTHS_IN_A_YEAR = 12;
        int assumedLoanTermYears = 30;
        int termMonths = assumedLoanTermYears * 12;
        double assumeAnnualInterestRate = 0.0275;

        // Weekly interest rate.
        double monthlyInterestRate = assumeAnnualInterestRate / MONTHS_IN_A_YEAR;

        // Number of weekly payments.
        int numberOfMonthlyPayments = termMonths;

        // Some weird math.
        double x = 1 - Math.Pow(1 + monthlyInterestRate, -numberOfMonthlyPayments);

        int y = (int)(monthlyInterestRate / x * listPrice);

        estimatedMonthlyLoanPayment = y;
    }

    /// <summary>
    /// Net Operating Income (NOI) is calculated by subtracting operating
    /// expenses from revenue. Expenses should include normal operating
    /// expenses plus any loan payments.
    /// </summary>
    private void SetEstimatedMonthlyNOI()
    {
        estimatedMonthlyNOI = estimatedMonthlyIncome
            - estimatedMonthlyOperatingCost
            - estimatedMonthlyLoanPayment;
    }

    /// <summary>
    /// Update the cash-on-cash return on investment.
    /// </summary>
    private void SetEstimatedCashROI()
    {
        double x = estimatedMonthlyNOI * 12;
        double y = x / minimumDownPayment;
        double z = y * 100;
        estimatedCashROI = z;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetEstimatedCapitalizationRate()
    {
        double x = estimatedMonthlyNOI * 12;
        double y = x / listPrice;
        double z = y * 100;
        estimatedCapitalizationRate = z;
    }

    public virtual void Generate()
    {

    }

    public void SetPropertyMarker(PropertyMarker marker)
    {
        propertyMarker = marker;
    }

    public void SetBaseSquareFootage(int value)
    {
        baseSquareFootage = value;
    }

    public void SetBaseValue(int value)
    {
        baseValue = value;
    }

    public void SetPricePerSquareFoot(int value)
    {
        pricePerSquareFoot = value;
    }

    public void SetAppraisedValue(int value)
    {
        appraisedValue = value;
    }

    public void SetListPrice(int value)
    {
        listPrice = value;
    }

    public void SetMinimumDownPayment(int value)
    {
        minimumDownPayment = value;
    }

    public void SetClosingCosts(int value)
    {
        closingCosts = value;
    }

    public void SetClassRatingIncomeMultiplier(double value)
    {
        classRatingIncomeMultiplier = value;
    }

    /// <summary>
    /// Set the upgrades available for this property.
    /// </summary>
    /// <param name="upgrades">A list of upgrades.</param>
    public void SetAvailableUpgrades(List<PropertyUpgrade> upgrades)
    {
        this.upgrades = upgrades;
    }

    public void PurchaseProperty(PropertyPurchaseData d)
    {
        loan = d.Loan;
        SetPurchaseValue(d.OfferValue);
        SetDownPaymentValue(d.OfferValue - d.Loan.InitialAmount);
        UpdateEquity();
    }

    /// <summary>
    /// Set the property's class rating.
    /// </summary>
    /// <param name="rating"></param>
    public void SetClassRating(PropertyClassRating rating)
    {
        classRating = rating;
    }

    /// <summary>
    /// Set the purchase value.
    /// </summary>
    /// <param name="value">The price paid for the property.</param>
    private void SetPurchaseValue(int value)
    {
        purchasePrice = value;
    }

    /// <summary>
    /// Set the down payment.
    /// </summary>
    /// <param name="value">The value of the down payment made when the
    /// property was purchased.</param>
    private void SetDownPaymentValue(int value)
    {
        downPayment = value;
    }

    /// <summary>
    /// Update the equity in the property.
    /// </summary>
    private void UpdateEquity()
    {
        equity = appraisedValue - loan.CurrentAmount;
    }

    /// <summary>
    /// Add income from rent to total income.
    /// </summary>
    /// <param name="value">An income value.</param>
    public void AddIncome(int value)
    {
        totalIncome += value;
    }

    /// <summary>
    /// Add an expense to total expenses paid. This could be construction and
    /// renovation costs or weekly maintenance and operating costs.
    /// </summary>
    /// <param name="value">The value of an expense.</param>
    public void AddExpense(int value)
    {
        totalExpensesPaid += value;
    }

    /// <summary>
    /// Update the property's appraised value.
    /// </summary>
    public void UpdateAppraisedValue()
    {
        //// Weekly interest rate.
        //double x = baseAppreciationRate / 100;
        //Debug.Log($"Weekly interest")
        //double weeklyGrowthRate = x / WEEKS_IN_A_YEAR;
        double weeklyGrowthRate = baseAppreciationRate / WEEKS_IN_A_YEAR;

        appraisedValue += (int)(appraisedValue * weeklyGrowthRate);
    }

    /// <summary>
    /// Return monthly income divided by 4. If the property is being renovated,
    /// return 0.
    /// </summary>
    /// <returns></returns>
    public int WeeklyIncome()
    {
        return EstimatedMonthlyIncome / 4;
    }

    public int WeeklyExpenses()
    {
        return estimatedMonthlyOperatingCost + loan.MonthlyPayment / 4;
    }

    public int CollectRent()
    {
        int income = WeeklyIncome() - WeeklyExpenses();
        loan.MakeWeeklyPayment();
        AddIncome(income);
        UpdateAppraisedValue();
        UpdateEquity();

        return income;
    }

    public void FinishUpgrade(PropertyUpgrade upgrade)
    {
        upgrade.Complete();
        UpdateEquity();
    }

    public void StartUpgrade(PropertyUpgrade upgrade)
    {
        AddExpense(upgrade.Price);
    }
}


[Serializable]
public class PropertyUpgradeState
{
    protected PropertyUpgrade upgrade;
    protected bool isComplete = false;

    public bool IsComplete => isComplete;

    public PropertyUpgradeState(PropertyUpgrade upgrade)
    {
        this.upgrade = upgrade;
    }

    public virtual void Next() { }
}


[Serializable]
public class PlanningState : PropertyUpgradeState
{
    public PlanningState(PropertyUpgrade upgrade) : base(upgrade) { }
}


[Serializable]
public class UnderConstructionState : PropertyUpgradeState
{
    public UnderConstructionState(PropertyUpgrade upgrade) : base(upgrade) { }

    public override void Next()
    {
        if (upgrade.RemainingWeeksToComplete == 0)
        {
            upgrade.ChangeState(new UpgradeComplete(upgrade));
            return;
        }

        upgrade.RemainingWeeksToComplete--;
    }
}


[Serializable]
public class UpgradeComplete : PropertyUpgradeState
{
    public UpgradeComplete(PropertyUpgrade upgrade) : base(upgrade)
    {
        isComplete = true;
    }
}


[Serializable]
public class PropertyUpgrade
{
    [SerializeField] protected string upgradeName = "New Upgrade";
    [SerializeField] protected int price = 0;
    [SerializeField] protected int weeksToComplete = 0;
    [SerializeField] protected int remainingWeeksToComplete = 0;

    protected PropertyUpgradeState state;
    protected RealEstateProperty p;

    public string Name => upgradeName;
    public int Price => price;
    public int WeeksToComplete => weeksToComplete;
    public int RemainingWeeksToComplete { get => remainingWeeksToComplete; set => remainingWeeksToComplete = value; }
    public bool IsComplete => state.IsComplete;
    public PropertyUpgradeState State => state;

    public PropertyUpgrade(RealEstateProperty p)
    {
        this.p = p;
        state = new PlanningState(this);
    }

    public void ChangeState(PropertyUpgradeState state)
    {
        this.state = state;
    }

    public void StartConstruction()
    {
        ChangeState(new UnderConstructionState(this));
    }

    public virtual void Complete()
    {
        ChangeState(new UpgradeComplete(this));
    }
}


[Serializable]
public class KitchenRemodel : PropertyUpgrade
{
    public KitchenRemodel(RealEstateProperty p) : base(p)
    {
        upgradeName = "Kitchen Remodel";
        price = (int)(p.BaseSquareFootage * 0.1 * p.PricePerSquareFoot);
        weeksToComplete = 12;
        remainingWeeksToComplete = weeksToComplete;
    }

    public override void Complete()
    {
        base.Complete();

        p.AppraisedValue += (int)(p.AppraisedValue * 0.2);
    }
}


[Serializable]
public class BathroomRemodel : PropertyUpgrade
{
    public BathroomRemodel(RealEstateProperty p) : base(p)
    {
        upgradeName = "Bathroom Remodel";
        price = (int)(p.BaseSquareFootage * 0.05 * p.PricePerSquareFoot);
        weeksToComplete = 5;
        remainingWeeksToComplete = weeksToComplete;
    }

    public override void Complete()
    {
        base.Complete();

        p.AppraisedValue += (int)(p.AppraisedValue * 0.08);
    }
}


[Serializable]
public class Landscaping : PropertyUpgrade
{
    public Landscaping(RealEstateProperty p) : base(p)
    {
        upgradeName = "Add Landscaping";
        price = (int)(p.BaseSquareFootage * 0.03 * p.PricePerSquareFoot);
        weeksToComplete = 1;
        remainingWeeksToComplete = weeksToComplete;
    }

    public override void Complete()
    {
        base.Complete();

        p.AppraisedValue += (int)(p.AppraisedValue * 0.05);
    }
}


[Serializable]
public class PorchAddition : PropertyUpgrade
{
    private int basePrice = 10_000;
    private int valueIncrease = 2_000;

    public PorchAddition(RealEstateProperty p) : base(p)
    {
        upgradeName = "Add Outside Deck";
        price = UnityEngine.Random.Range((int)(basePrice * 0.8), (int)(basePrice * 1.2));
        weeksToComplete = 2;
        remainingWeeksToComplete = weeksToComplete;
    }

    public override void Complete()
    {
        base.Complete();

        p.AppraisedValue += valueIncrease;
    }
}

/* Classify homes?
 *  Single-family:
 *      - Bungalow
 *          - 800-5000
 *      - Ranch
 *          - 1200-6000
 *      - Cottage
 *          - 1200-4000
 *      - Cabin
 *          - 1000-5000
 *      - Mansion
 *          - 4000-8000
 *      - Chateau
 *          - 4000-10_000
 *      - Villa
 *          - 2000-10_000
 *      - Manor
 *          - 4000-10_000
 * 
 * 
 * 
 * 
 */