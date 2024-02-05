using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models;

public class Basket
{
    public string UserId { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public decimal TotalPrice
    {
        get => Items.Sum(item => item.ItemPrice);
    }
    public int TotalCount
    {
        get => Items.Sum(item => item.Quantity);
    }
}
