using System;
using UnityEngine;
using UnityEngine.UI;

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

interface IValidator<T1,T2,T3,T4,T5>
{
    bool Validate(T1 t1,T2 t2,T3 t3,T4 t4,T5 t5);
}

class ProductValidator : IValidator<InputField, InputField, InputField, InputField, InputField>
{
    public bool Validate(InputField supplyPrice, InputField tyComission, InputField kdvRate, InputField cargoExpance, InputField profitRate)
    {
        if (supplyPrice.text == null || supplyPrice.text == "0" || supplyPrice.text == "")
        {
            supplyPrice.GetComponent<Image>().color = Color.red;
            return false;
        }
        else if (tyComission.text == null || tyComission.text == "")
        {
            tyComission.GetComponent<Image>().color = Color.red;
            return false;
        }
        else if (kdvRate.text == null || kdvRate.text == "")
        {
            kdvRate.GetComponent<Image>().color = Color.red;
            return false;
        }
        else if (cargoExpance.text == null || cargoExpance.text == "")
        {
            cargoExpance.GetComponent<Image>().color = Color.red;
            return false;
        }
        else if (profitRate.text == null || profitRate.text == "")
        {
            profitRate.GetComponent<Image>().color = Color.red;
            return false;
        }
        else
            return true;
    }
}
