using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UI_SellPropertyDetails : MonoBehaviour
{
    private Button sellPropertyBtn;
    private TMP_Text appraisedValueText;
    private TMP_Text mortgageValueText;
    private TMP_Text profitValueText;

    public Button SellPropertyBtn => sellPropertyBtn;
    public TMP_Text AppraisedValueText => appraisedValueText;
    public TMP_Text MortgageValueText => mortgageValueText;
    public TMP_Text ProfitValueText => profitValueText;

    public delegate void OnSellPropertyDelegate();

    public event OnSellPropertyDelegate onSellProperty;

    private void Awake()
    {
        // Set references.
        sellPropertyBtn = transform
            .Find("SellProperty_btn")
            .GetComponent<Button>();
        appraisedValueText = transform
            .Find("AppraisedValue_text")
            .GetComponent<TMP_Text>();
        mortgageValueText = transform
            .Find("Mortgage_text")
            .GetComponent<TMP_Text>();
        profitValueText = transform
            .Find("Profit_text")
            .GetComponent<TMP_Text>();
    }

    // Use this for initialization
    void Start()
    {
        // Set listeners.
        sellPropertyBtn
            .onClick
            .AddListener(delegate() { OnSellProperty(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Set the value for the appraised value text.
    /// </summary>
    /// <param name="value"></param>
    public void SetAppraisedValueText(double value)
    {
        AppraisedValueText.text = value.ToString();
    }

    /// <summary>
    /// Set the value for the outstanding balance remaining on the mortgage.
    /// </summary>
    /// <param name="value"></param>
    public void SetMortgageText(double value)
    {
        MortgageValueText.text = value.ToString();
    }

    /// <summary>
    /// Set the value for the profit gained from selling this property.
    /// </summary>
    /// <param name="value"></param>
    public void SetProfitText(double value)
    {
        ProfitValueText.text = value.ToString();
    }

    public void OnSellProperty()
    {
        if (onSellProperty != null)
        {
            onSellProperty.Invoke();
        }
    }
}
