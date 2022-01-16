using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private GameObject buyPropertyDetailsPanel;
    private GameObject sellPropertyDetailsPanel;

    private void Awake()
    {
        buyPropertyDetailsPanel = Resources
            .Load("UI/BuyPropertyDetails_panel") as GameObject;
        sellPropertyDetailsPanel = Resources
            .Load("UI/SellPropertyDetails_panel") as GameObject;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public UI_BuyPropertyDetails ShowBuyPropertyDetailsPanel(InvestmentProperty iProperty)
    {
        Debug.Log("Showing BuyPropertyDetailsPanel.");
        GameObject panel = Instantiate(
            buyPropertyDetailsPanel,
            iProperty.transform.position,
            Quaternion.identity,
            GameObject.Find("Canvas").transform);

        var propertyDetails = panel.GetComponent<UI_BuyPropertyDetails>();

        //propertyDetails.SetPropertyData(iProperty.PropertyData);

        propertyDetails.onPurchaseProperty
            += iProperty.PurchaseProperty;

        return propertyDetails;
    }

    public UI_SellPropertyDetails ShowSellPropertyDetailsPanel(InvestmentProperty iProperty)
    {
        Debug.Log("Showing SellPropertyDetailsPanel.");
        GameObject panel = Instantiate(
            sellPropertyDetailsPanel,
            iProperty.transform.position,
            Quaternion.identity,
            GameObject.Find("Canvas").transform);

        var propertyDetails = panel.GetComponent<UI_SellPropertyDetails>();

        propertyDetails.onSellProperty
            += iProperty.SellProperty;

        return propertyDetails;
    }
}
