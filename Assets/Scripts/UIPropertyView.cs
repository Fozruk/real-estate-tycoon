using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPropertyView : MonoBehaviour
{
    private MainController _controller;

    [SerializeField] private RealEstateProperty realEstateProperty;

    public delegate void BuyPropertyDelegate(RealEstateProperty propertyValue, int offerValue);

    public event BuyPropertyDelegate OnBuyPropertyDelegate;

    private void Awake()
    {
        _controller = GameObject.Find("MainController").GetComponent<MainController>();

        realEstateProperty = GetComponent<RealEstateProperty>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"The value of my property is {realEstateProperty.BaseValue}.");

        //OnBuyPropertyDelegate += _controller.BuyProperty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyProperty()
    {
        if (OnBuyPropertyDelegate != null)
        {
            OnBuyPropertyDelegate.Invoke(realEstateProperty, realEstateProperty.ListPrice);
        }
    }
}
