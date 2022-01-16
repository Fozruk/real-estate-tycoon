using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestMortgage
{
    Mortgage _mortgage;
    InvestmentPropertyData _data;
    int _term;
    int _downPayment;
    int _amount;
    double _interestRate;


    [SetUp]
    public void Setup()
    {
        _data = new InvestmentPropertyData();
        _term = 30;
        _downPayment = 2000;
        _amount = 100_000;
        _interestRate = 0.05;
    }

    [UnityTest]
    public IEnumerator TermEqualsThirty()
    {
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.Term, _term);
    }

    [UnityTest]
    public IEnumerator DownPaymentEqualsTwoThousand()
    {
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.DownPayment, _downPayment);
    }

    [UnityTest]
    public IEnumerator InitialAmountEqualsOneHundredThousand()
    {
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.Principal, _amount);
    }

    [UnityTest]
    public IEnumerator InterestRateEqualsFivePercent()
    {
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.InterestRate, _interestRate);
    }

    [UnityTest]
    public IEnumerator PayPeriodsEqualsFiftyTwo()
    {
        _term = 1;
        int expected = 52;

        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.PayPeriods, expected);
    }

    [UnityTest]
    public IEnumerator DownPaymentEqualsTwentyThousand()
    {
        int expected = 20_000;
        _downPayment = 20_000;
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.DownPayment, expected);
    }

    [UnityTest]
    public IEnumerator MinimumPaymentEqualsOneHundredFiftyThree()
    {
        int expected = 153;

        _amount = 30_000;
        _interestRate = 0.03;
        _term = 4;
        _downPayment = 0;
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.MinimumPayment, expected);
    }

    [UnityTest]
    public IEnumerator OutstandingBalanceEqualsAmountMinusDownPayment()
    {
        int expected = _amount - _downPayment;

        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.OutstandingBalance, expected);
    }

    [UnityTest]
    public IEnumerator PrincipalPaymentEqualsOneHundredThirtyFive()
    {
        int expected = 135;

        _amount = 30_000;
        _interestRate = 0.03;
        _term = 4;
        _downPayment = 0;
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.InterestPayment, expected);
    }

    [UnityTest]
    public IEnumerator MakePaymentSubtractsPrincipalPaymentFromOutstandingBalance()
    {
        _amount = 30_000;
        _interestRate = 0.03;
        _term = 4;
        _downPayment = 0;
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        double expected = _amount - (_mortgage.MinimumPayment - _mortgage.InterestPayment);
        _mortgage.MakeMinimumPayment();

        yield return null;

        Assert.AreEqual(_mortgage.OutstandingBalance, expected);
    }

    [UnityTest]
    public IEnumerator MakePaymentSubtractsOneFromRemainingPayPeriods()
    {
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        int expected = _mortgage.RemainingPayPeriods - 1;
        _mortgage.MakeMinimumPayment();

        yield return null;

        Assert.AreEqual(_mortgage.RemainingPayPeriods, expected);
    }

    [UnityTest]
    public IEnumerator AssetReturnsAsset()
    {
        _mortgage = new Mortgage(
            _data,
            _amount,
            _downPayment,
            _interestRate,
            _term);

        yield return null;

        Assert.AreEqual(_mortgage.Asset, _data);
    }
}