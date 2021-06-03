using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        OnlineShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }      
        public IEnumerable<OrderDetail> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Price.ToString().Contains(searchString) || x.OrderID.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.OrderID).ToPagedList(page, pageSize);
        }
        public bool Update(OrderDetail entity)
        {
            try
            {              
                var orderdetail = db.OrderDetails.SingleOrDefault(x=>x.OrderID == entity.OrderID && x.ProductID == entity.ProductID);
                orderdetail.Quantity = entity.Quantity;
                orderdetail.ProductID = entity.ProductID;
                orderdetail.Price = db.Products.Find(entity.ProductID).Price;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public OrderDetail GetByID(long id, long productId)
        {
            return db.OrderDetails.SingleOrDefault(x=>x.OrderID == id && x.ProductID == productId);
        }
        public bool Delete(long id,long productId)
        {
            try
            {
                db = new OnlineShopDbContext();
                var orderdetail = db.OrderDetails.SingleOrDefault(x=>x.OrderID==id && x.ProductID==productId);
                db.OrderDetails.Remove(orderdetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                string a = e.Message;
                return false;
            }
        }
    }
}
