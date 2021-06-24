namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Mã khách hàng")]
        public long? CustomerID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên khách hàng")]
        public string ShipName { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string ShipMobile { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string ShipAddress { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string ShipEmail { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
