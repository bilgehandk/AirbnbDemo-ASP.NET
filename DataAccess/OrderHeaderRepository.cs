using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace DataAccess;

public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository<OrderHeader>
{
    private readonly ApplicationDbContext _db;

    public OrderHeaderRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


    public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
    {
        var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
        if (orderFromDb != null)
        {
            orderFromDb.OrderStatus = orderStatus;
            if (paymentStatus != null)
            {
                orderFromDb.PaymentStatus = paymentStatus;
            }
        }
    }
    
    public void UpdateStripePaymentID(int id, string sessionId, string paymentLinkId)
    {
        var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
        orderFromDb.PaymentDate = DateTime.Now;
        orderFromDb.SessionId = sessionId;
        orderFromDb.PaymentIntentId = paymentLinkId;
    }

}