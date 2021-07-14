using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using foodfun.Models;


[Serializable] //可序列化
public class AdminCart
{

    //建構子
    public AdminCart()
    {
        this.cartItems = new List<Carts>();
    }

    //儲存商品
    public List<Carts> cartItems;

    /// <summary>
    /// 取得購物車內商品的總數量
    /// </summary>
    public int Count
    {
        get
        {
            return this.cartItems.Count;
        }
    }


    //取得商品總價
    public int TotalAmount
    {
        get
        {
            int? totalAmount = 0;

            if (this.cartItems != null)
            {
                totalAmount = this.cartItems.Sum(m => m.each_item_amount);
            }

            return totalAmount.GetValueOrDefault();
        }
    }




    //新增一筆Product，使用ProductId
    public void AddCart(string productNo, int qty, string prop_select)
    {
        var findItem = this.cartItems
                        .Where(m => m.product_no == productNo)
                        .Where(m => m.Property_select == prop_select)
                        .FirstOrDefault();

        //判斷相同Id的CartItem是否已經存在購物車內
        int int_price = Shop.GetProductPrice(productNo);
        if (findItem == null)
        {   //不存在購物車內，則新增一筆

            var cartItem = new Carts()
            {
                product_no = productNo,
                Property_select = prop_select,
                qty = qty,
                each_item_amount = qty* int_price
            };
            this.cartItems.Add(cartItem);
        }
        else
        {
            findItem.qty += qty;
            findItem.each_item_amount = findItem.qty * int_price;
        }



    }








}
