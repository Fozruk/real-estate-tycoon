using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    [SerializeField] private double wallet = 50_000;
    [SerializeField] private double assets = 0;
    [SerializeField] private double liabilities = 0;
    [SerializeField] private double netWorth = 0;
    [SerializeField] private double weeklyExpenses = 0;
    [SerializeField] private double totalExpenses = 0;
    [SerializeField] private double weeklyIncome = 0;
    [SerializeField] private double totalIncome = 0;
    [SerializeField] private List<InvestmentPropertyData> investmentProperties;
    [SerializeField] private List<Mortgage> mortgages;

    public double Wallet => wallet;
    public double Networth => netWorth;
    public double WeeklyExpenses => weeklyExpenses;
    public double TotalExpenses => totalExpenses;
    public double WeeklyIncome => weeklyIncome;
    public double TotalIncome => totalIncome;
    [SerializeField] public List<InvestmentPropertyData> InvestmentProperties => investmentProperties;
    public List<Mortgage> Mortgages => mortgages;

    public Data()
    {
        investmentProperties = new List<InvestmentPropertyData>();
        mortgages = new List<Mortgage>();
    }

    public void RemoveFunds(double value)
    {
        if (wallet < value)
        {
            throw new Exception("Not enough money.");
        }

        wallet -= value;
    }

    public void SetNetWorth(double value)
    {
        netWorth = value;
    }

    public void SetWeeklyIncome(double value)
    {
        weeklyIncome = value;
    }

    public void SetWeeklyExpenses(double value)
    {
        weeklyExpenses = value;
    }

    public void AddFunds(double value)
    {
        wallet += value;
    }

    public void AddTotalIncome(double value)
    {
        totalIncome += value;
    }

    public void AddTotalExpenses(double value)
    {
        totalExpenses += value;
    }

    public void AddProperty(InvestmentPropertyData p)
    {
        investmentProperties.Add(p);
    }

    public void AddMortgage(Mortgage m)
    {
        mortgages.Add(m);
    }
}
