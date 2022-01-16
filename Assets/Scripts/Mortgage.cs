using System;
using UnityEngine;

[Serializable]
public class Mortgage
{
    #region Fields
    /// <summary>
    /// The number of months in a year.
    /// </summary>
    private readonly int MONTHS_IN_A_YEAR = 12;

    /// <summary>
    /// The number of weeks in a year.
    /// </summary>
    private readonly int WEEKS_IN_A_YEAR = 52;

    /// <summary>
    /// The asset for which this loan applies.
    /// </summary>
    private InvestmentPropertyData asset;

    /// <summary>
    /// The number of pay periods in a year.
    /// </summary>
    private int annualPayPeriods;

    /// <summary>
    /// The down payment made when the loan is issued.
    /// </summary>
    [SerializeField] private double downPayment;

    /// <summary>
    /// The minimum payment per pay period.
    /// </summary>
    [SerializeField] private double minimumPayment;

    /// <summary>
    /// The outstanding balance on the loan.
    /// </summary>
    [SerializeField] private double outstandingBalance;

    /// <summary>
    /// The number of pay periods required to pay the loan in full.
    /// </summary>
    [SerializeField] private int payPeriods;

    /// <summary>
    /// The initial amount borrowed.
    /// </summary>
    [SerializeField] private int principal;

    /// <summary>
    /// The interest paid when making a period payment.
    /// </summary>
    [SerializeField] private int interestPayment;

    /// <summary>
    /// The number of remaining pay periods.
    /// </summary>
    [SerializeField] private int remainingPayPeriods;

    /// <summary>
    /// The number of years required to pay the loan in full.
    /// </summary>
    [SerializeField] private int term;

    /// <summary>
    /// The annual interest rate.
    /// </summary>
    [SerializeField] private double interestRate;
    
    #endregion

    #region Properties
    /// <summary>
    /// The asset for which this loan applies.
    /// </summary>
    public InvestmentPropertyData Asset => asset;

    /// <summary>
    /// The number of pay periods in a year.
    /// </summary>
    public int AnnualPayPeriods => annualPayPeriods;

    /// <summary>
    /// The down payment made when the loan is issued.
    /// </summary>
    public double DownPayment => downPayment;

    /// <summary>
    /// The minimum payment per pay period.
    /// </summary>
    public double MinimumPayment => minimumPayment;

    /// <summary>
    /// The outstanding balance remaining on the loan.
    /// </summary>
    public double OutstandingBalance => outstandingBalance;

    /// <summary>
    /// The number of payments required to pay the loan in full.
    /// </summary>
    public int PayPeriods => payPeriods;

    /// <summary>
    /// The initial amount borrowed.
    /// </summary>
    public int Principal => principal;

    /// <summary>
    /// The interest paid when making a period payment.
    /// </summary>
    public int InterestPayment => interestPayment;

    /// <summary>
    /// The number of remaining pay periods.
    /// </summary>
    public int RemainingPayPeriods => remainingPayPeriods;

    /// <summary>
    /// The number of years required to pay the loan in full.
    /// </summary>
    public int Term => term;

    /// <summary>
    /// The annual interest rate.
    /// </summary>
    public double InterestRate => interestRate;
    #endregion

    public Mortgage(InvestmentPropertyData asset, int principal, double downPayment, double interestRate, int term)
    {
        annualPayPeriods = MONTHS_IN_A_YEAR;
        this.asset = asset;
        this.downPayment = downPayment;
        this.interestRate = interestRate;
        this.term = term;
        this.principal = principal;
        outstandingBalance = principal - downPayment;
        payPeriods = CalculatePayPeriods(term);
        remainingPayPeriods = payPeriods;
        minimumPayment = CalculateMinimumPayment();
        interestPayment = CalculateInterestPayment();
    }

    /// <summary>
    /// Make the minimum payment for the pay period.
    /// </summary>
    public void MakeMinimumPayment()
    {
        // Subtract the principal payment from the outstanding balance.
        outstandingBalance -= Math.Max(0, MinimumPayment - InterestPayment);

        // Update the interest payment.
        interestPayment = CalculateInterestPayment();

        // Update the number of pay periods.
        remainingPayPeriods--;
    }

    /// <summary>
    /// Make an additional payment towards the principal of the loan.
    /// </summary>
    /// <param name="payment">A payment towards the principal.</param>
    public void MakePrincipalPayment(int payment)
    {
        // Subtract the principal payment from the outstanding balance.
        outstandingBalance -= payment;

        // Update the interest payment.
        interestPayment = CalculateInterestPayment();
    }

    /// <summary>
    /// Calculate the minimum payment for the pay period.
    /// </summary>
    /// <returns>The minimum payment for the pay period.</returns>
    private double CalculateMinimumPayment()
    {
        double P = OutstandingBalance; // Remaining principal.
        double t = Term; // Loan term in years.
        double r = InterestRate; // Annual interest rate.
        double m = MONTHS_IN_A_YEAR; // Pay periods in a year.
        double i = r / m; // Interest paid per period, per year.
        double n = t * m; // The total number of pay periods.

        double result = P * i / (1 - Math.Pow(1 + i, -n)); // Formula.

        return result;
    }

    /// <summary>
    /// Calculate the number of pay periods in the loan term.
    /// </summary>
    /// <param name="years">A number of years.</param>
    /// <returns>The number of pay periods in the term.</returns>
    private int CalculatePayPeriods(int years)
    {
        return years * AnnualPayPeriods;
    }

    /// <summary>
    /// Calculate the interest amount paid when making a period payment.
    /// </summary>
    /// <returns>The principal amount.</returns>
    private int CalculateInterestPayment()
    {
        return (int)(OutstandingBalance * InterestRate / AnnualPayPeriods);
    }

    /// <summary>
    /// Calculate the weekly interest gained on the loan.
    /// </summary>
    /// <returns>The weekly interest rate.</returns>
    private double WeeklyInterestRate()
    {
        return InterestRate / AnnualPayPeriods;
    }
}
