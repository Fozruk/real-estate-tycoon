using System;
using UnityEngine;

[Serializable]
public class PropertyLoan
{
    [SerializeField] private int termYears = 30;
    [SerializeField] private int termWeeks = 30 * 52;
    [SerializeField] private int termMonths = 30 * 12;
    [SerializeField] private int paymentsRemaining = 30 * 52;
    [SerializeField] private int initialAmount = 0;
    [SerializeField] private int currentAmount = 0;
    [SerializeField] private int weeklyPayment = 0;
    [SerializeField] private int monthlyPayment = 0;
    [SerializeField] private double annualInterestRate = 0;

    public int TermYears => termYears;
    public int TermWeeks => termWeeks;
    public int PaymentsRemaining => paymentsRemaining;
    public int InitialAmount => initialAmount;
    public int CurrentAmount => currentAmount;
    public double AnnualInterestRate => annualInterestRate;
    public int WeeklyPayment => weeklyPayment;
    public int MonthlyPayment => monthlyPayment;

    public PropertyLoan(int amount, double interestRate)
    {
        initialAmount = amount;
        currentAmount = amount;
        annualInterestRate = interestRate;

        if (amount > 0)
        {
            CalculateWeeklyPayments();
        }
    }

    public void CalculateWeeklyPayments()
    {
        int WEEKS_IN_A_YEAR = 52;

        // Weekly interest rate.
        double weeklyInterestRate = annualInterestRate / WEEKS_IN_A_YEAR;

        // Number of weekly payments.
        int numberOfWeeklyPayments = termWeeks;

        // Some weird math.
        double x = 1 - Math.Pow(1 + weeklyInterestRate, -numberOfWeeklyPayments);

        int y = (int)(weeklyInterestRate / x * initialAmount);

        weeklyPayment = y;
    }

    public void CalculateMonthlyPayments()
    {
        int MONTHS_IN_A_YEAR = 12;

        // Weekly interest rate.
        double monthlyInterestRate = annualInterestRate / MONTHS_IN_A_YEAR;

        // Number of weekly payments.
        int numberOfMonthlyPayments = termMonths;

        // Some weird math.
        double x = 1 - Math.Pow(1 + monthlyInterestRate, -numberOfMonthlyPayments);

        int y = (int)(monthlyInterestRate / x * initialAmount);

        monthlyPayment = y;
    }

    public void MakeWeeklyPayment()
    {
        currentAmount -= weeklyPayment;
        paymentsRemaining--;
    }
}
