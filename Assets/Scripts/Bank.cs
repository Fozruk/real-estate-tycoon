using System;

public class Bank
{
    #region Fields
    /// <summary>
    /// The base interest rate for loans issued by the bank.
    /// </summary>
    private double baseLoanInterestRate = 0.05;
    private readonly int THIRTY_YEAR_MORTGAGE_TERM = 30;

    #endregion

    #region Properties
    /// <summary>
    /// The base interest rate for loans issued by the bank.
    /// </summary>
    public double BaseLoanInterestRate => baseLoanInterestRate;

    #endregion

    public Bank()
    {

    }

    /// <summary>
    /// Return a new 30-year Mortgage loan for a property.
    /// </summary>
    /// <param name="data"></param>
    /// <returns>A new mortgage loan.</returns>
    public Mortgage Issue30YearMortgage(InvestmentPropertyData propertyData, int offerPrice, double downPayment)
    {
        return new Mortgage(
            propertyData,
            offerPrice,
            downPayment,
            baseLoanInterestRate,
            THIRTY_YEAR_MORTGAGE_TERM);
    }
}