using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models;

[NotMapped]
public class ShoppingCartVM
{
    
    public Reservation? Reservation { get; set; }
    [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
    public int Count { get; set; }
    public IEnumerable<ShoppingCart>? cartItems { get; set; }
    
    public double CartTotal {get; set;}
    
    public OrderHeader? OrderHeader { get; set; }
    
    public static double GetPriceBasedOnQuantity(double quantity, double unitPrice, double priceHalfDozen, double priceDozen)
    {
        if (quantity <= 5)
        {
            return unitPrice;
        }
        else
        {
            if (quantity <= 11)
            {
                return priceHalfDozen;
            }
            return priceDozen;
        }
    }

}