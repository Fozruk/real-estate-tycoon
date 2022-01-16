using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class InvestmentProperty : MonoBehaviour
{
    #region Fields
    private MainController mainController;
    private UIManager uiManager;
    /// <summary>
    /// A reference to investment property data.
    /// </summary>
    [SerializeField] private InvestmentPropertyData propertyData;
    /// <summary>
    /// Button to show details about an investment property.
    /// </summary>
    private Button showPropertyDetailsBtn;

    #endregion

    public InvestmentPropertyData PropertyData => propertyData;

    #region Properties

    #endregion

    private void Awake()
    {
        // Set references.
        mainController = GameObject
            .Find("MainController")
            .GetComponent<MainController>();
        uiManager = GameObject
            .Find("MainController")
            .GetComponent<UIManager>();
        showPropertyDetailsBtn = transform
            .Find("ShowPropertyDetails_btn")
            .GetComponent<Button>();
    }

    public void Start()
    {
        // Set listeners.
        showPropertyDetailsBtn
            .onClick
            .AddListener(delegate () { ShowPropertyDetails(); });
    }

    public void Update()
    {
        
    }

    /// <summary>
    /// Provide property data so that UI elements can be populated.
    /// </summary>
    /// <param name="data"></param>
    public void SetPropertyData(InvestmentPropertyData data)
    {
        propertyData = data;
    }

    /// <summary>
    /// Show the property details panel.
    /// </summary>
    private void ShowPropertyDetails()
    {
        if (propertyData.IsForSale)
        {
            Debug.Log("Showing property details to buy property.");
            var panel = uiManager.ShowBuyPropertyDetailsPanel(this);

            panel.SetListPrice(propertyData.ListPrice);
            panel.SetAppraisedValueText(propertyData.AppraisedValue);
            panel.DownpaymentInputField.onValueChanged.AddListener(
                delegate { UpdateDownpaymentValue(panel); });

        } else
        {
            Debug.Log("Showing property details to sell property.");
            var panel = uiManager.ShowSellPropertyDetailsPanel(this);

            panel.SetAppraisedValueText(propertyData.AppraisedValue);
            panel.SetMortgageText(propertyData.Mortgage.OutstandingBalance);
            panel.SetProfitText(CalculateSaleProfit());
        }
        
    }

    /// <summary>
    /// Update the value displayed for the downpayment.
    /// </summary>
    public void UpdateDownpaymentValue(UI_BuyPropertyDetails panel)
    {
        double downpaymentPercent = double.Parse(
            panel.DownpaymentInputField.text) / 100;
        panel.SetDownpaymentValueText(CalculateDownpayment(downpaymentPercent));
    }

    /// <summary>
    /// Calculate a new downpayment, determined by the downpayment % value.
    /// </summary>
    /// <param name="downpaymentPercent"></param>
    /// <returns></returns>
    public double CalculateDownpayment(double downpaymentPercent)
    {
        return propertyData.ListPrice * downpaymentPercent;
    }

    public double CalculateSaleProfit()
    {
        return propertyData.AppraisedValue - propertyData.Mortgage.OutstandingBalance;
    }

    public void PurchaseProperty(double downpayment)
    {
        Debug.Log($"I'm purchasing this property for {downpayment}, bitch!");
        mainController.BuyInvestmentProperty(propertyData, downpayment);
    }

    public void SellProperty()
    {
        Debug.Log("I'm selling this property, bitch!");
        //mainController.BuyInvestmentProperty(propertyData, downpayment);
    }
}
