//using UnityEngine;
//using UnityEngine.TestTools;
//using NUnit.Framework;
//using System.Collections;

//public class TestInvestmentPropertyData
//{
//    InvestmentPropertyData _property;

//    [SetUp]
//    public void Setup()
//    {
//        _property = new InvestmentPropertyData();
//    }

//    [UnityTest]
//    public IEnumerator SetAppraisedValueEqualsTen()
//    {
//        int expected = 10;

//        _property.SetAppraisedValue(expected);

//        yield return null;

//        Assert.AreEqual(_property.AppraisedValue, expected);
//    }

//    [UnityTest]
//    public IEnumerator SetBaseValueEqualsTen()
//    {
//        int expected = 10;

//        _property.SetBaseValue(expected);

//        yield return null;

//        Assert.AreEqual(_property.BaseValue, expected);
//    }

//    [UnityTest]
//    public IEnumerator SetListPriceEqualsTen()
//    {
//        int expected = 10;

//        _property.SetListPrice(expected);

//        yield return null;

//        Assert.AreEqual(_property.ListPrice, expected);
//    }

//    [UnityTest]
//    public IEnumerator SetBaseSquareFootageEqualsTen()
//    {
//        int expected = 10;

//        _property.SetBaseSquareFootage(expected);

//        yield return null;

//        Assert.AreEqual(_property.BaseSquareFootage, expected);
//    }

//    [UnityTest]
//    public IEnumerator SetOperatingExpenseRateEqualsOnePercent()
//    {
//        double expected = 0.01;

//        _property.SetOperatingExpenseRate(expected);

//        yield return null;

//        Assert.AreEqual(_property.OperatingExpensesRate, expected);
//    }

//    [UnityTest]
//    public IEnumerator AddSquareFootageEqualsOneThousand()
//    {
//        int input = 1000;
//        int expected = 1000;

//        _property.AddSquareFootage(input);

//        yield return null;

//        Assert.AreEqual(_property.AdditionalSquareFootage, expected);
//    }

//    [UnityTest]
//    public IEnumerator SetForSaleEqualsTrue()
//    {
//        bool expected = true;

//        _property.SetForSale(expected);

//        yield return null;

//        Assert.AreEqual(_property.IsForSale, expected);
//    }

//}