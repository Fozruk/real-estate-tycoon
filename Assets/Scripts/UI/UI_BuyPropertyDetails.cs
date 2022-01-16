using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UI_BuyPropertyDetails : MonoBehaviour
{
    /// <summary>
    /// Button to purchase the property.
    /// </summary>
    private Button purchasePropertyBtn;
    /// <summary>
    /// Displays the property's list price.
    /// </summary>
    private TMP_Text listPriceText;
    /// <summary>
    /// Displays the property's appraised value.
    /// </summary>
    private TMP_Text appraisedValueText;
    /// <summary>
    /// Displays the current downpayment.
    /// </summary>
    private TMP_Text downpaymentValueText;
    /// <summary>
    /// Input describing the % downpayment you're making.
    /// </summary>
    private TMP_InputField downpaymentInputField;
    /// <summary>
    /// A reference to the CloseWindow script.
    /// </summary>
    private UI_CloseWindow closeWindow;

    public Button PurchasePropertyBtn => purchasePropertyBtn;
    public TMP_Text ListPriceText => listPriceText;
    public TMP_Text AppraisedValueText => appraisedValueText;
    public TMP_Text DownpaymentValueText => downpaymentValueText;
    public TMP_InputField DownpaymentInputField => downpaymentInputField;

    public delegate void OnPurchasePropertyDelegate(double downpayment);

    public event OnPurchasePropertyDelegate onPurchaseProperty;

    private void Awake()
    {
        // Set references.
        purchasePropertyBtn = transform
            .Find("PurchaseProperty_btn")
            .GetComponent<Button>();
        listPriceText = transform
            .Find("ListPrice_label")
            .GetComponent<TMP_Text>();
        appraisedValueText = transform
            .Find("AppraisedValue_label")
            .GetComponent<TMP_Text>();
        downpaymentValueText = transform
            .Find("DownpaymentValue_label")
            .GetComponent<TMP_Text>();
        downpaymentInputField = transform
            .Find("Downpayment_inputField")
            .GetComponent<TMP_InputField>();
        closeWindow = transform.GetComponent<UI_CloseWindow>();
    }

    // Use this for initialization
    void Start()
    {
        // Set listeners.
        purchasePropertyBtn
            .onClick
            .AddListener(delegate() { OnPurchaseProperty(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Called when the user purchases the property.
    /// </summary>
    public void OnPurchaseProperty()
    {
        if (onPurchaseProperty != null)
        {
            onPurchaseProperty.Invoke(double.Parse(
                downpaymentValueText.text));
        }

        closeWindow.CloseWindow();
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
    /// Set the value for the downpayment text.
    /// </summary>
    /// <param name="value"></param>
    public void SetDownpaymentValueText(double value)
    {
        DownpaymentValueText.text = value.ToString();
    }

    /// <summary>
    /// Set the value for the list price text.
    /// </summary>
    /// <param name="value"></param>
    public void SetListPrice(double value)
    {
        ListPriceText.text = value.ToString();
    }
}
