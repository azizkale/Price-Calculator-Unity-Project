using System;
using UnityEngine;

[Serializable]
public class Product : MonoBehaviour
{
    public string ID;
    public string pName;
    public float supplyingPrice;
    public float trendyolComissionRate;
    public float KDV;
    public float cargoExpense;
    public float profitRate;
    public float sellingingPrice;

    //request info
    public string requestInfo;

    //Expense Amounts
    public float totalExpenseAmount;
    public float trendyolComissionExpenseAmount;
    public float kdvExpenseAmount;

    //Profit Amount
    public float profitAmount;

    //Invoice Amount
    public float invoiceAmount;

    //Methods
    public float calculateSellingPrice()
    {
        this.totalExpenseAmount =
            this.supplyingPrice +
              (this.supplyingPrice * this.trendyolComissionRate / 100) + 
            (this.supplyingPrice * (this.KDV / 100)) +
            this.cargoExpense;

        this.profitAmount = this.totalExpenseAmount * this.profitRate / 100;

        this.sellingingPrice = this.profitAmount + this.totalExpenseAmount;

        //makes it with 2 digit after komma
        return this.sellingingPrice;
    }

    public decimal calculateTrendyolComisssionExpenseAmount()
    {
        this.trendyolComissionExpenseAmount = this.supplyingPrice * this.trendyolComissionRate / 100;
        return Decimal.Parse(String.Format("{0:0.##}", this.trendyolComissionExpenseAmount));

    }

    public decimal calculateKDVExpenseAmount()
    {
        this.kdvExpenseAmount = this.supplyingPrice * this.KDV / 100;
        return Decimal.Parse(String.Format("{0:0.##}", this.kdvExpenseAmount));
    }

    public decimal calculateprofitAmount()
    {
        this.profitAmount = this.totalExpenseAmount * this.profitRate / 100;
        return Decimal.Parse(String.Format("{0:0.##}", this.profitAmount));
    }

    public float calculateInvoice()
    {
        this.invoiceAmount = (this.calculateSellingPrice() * 100) / (100f + this.KDV);
        return  this.invoiceAmount;
    }
}

interface IValidator<T>
{
    bool Validate(T t);
}

class ProductValidator : IValidator<Product>
{
    public bool Validate(Product p)
    {
        if (p.pName == null || p.pName == "")
        {
            Debug.Log("Lütfen ürün ismi giriniz.");            
            return false;
        }
        if (p.supplyingPrice == 0)
        {
            Debug.Log("Lütfen ürün alış fiyatı giriniz.");
            return false;
        }
        else
            return true;
    }
}
