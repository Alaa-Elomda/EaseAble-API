
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.BL;

public interface ICartsManager
{
    void AddToCart(CartDto cartdto);
    void RemoveFromCart(removeCartDto removedCart);
    void Edit(editCartDto editCartdto); List<CartWithProductDto> GetCartByUserId(string userId);

    void EmptyUserCart(string userId);
    int? GetUserCartCount(string userId);
}
