using System;

public class PropertyPurchaseData
{
    private int offerValue;
    private PropertyLoan loan;

    public int OfferValue => offerValue;
    public PropertyLoan Loan => loan;

    public PropertyPurchaseData(int offerValue, PropertyLoan loan)
    {
        this.offerValue = offerValue;
        this.loan = loan;
    }
}
