using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;

public interface IOrdersRepo: IGenericRepo<Order>
{
    Order? GetByIdWithProducts(int id);
    List<Order>? GetByUserWithProducts(string userId);
    int CountAll();
    int CountAccepted();
    int CountRejected();
    int CountPending();
}
