using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities.BaseEntities;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Khách hàng
    /// CreatedBy: LTHAI(23/11/2020)
    /// </summary>
    public class Customer: BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [Require]
        [CheckExist]
        [@DisplayName("Mã khách hàng")]
        [@MaxLength(20, "Mã khách hàng không được vượt quá 20 kí tự")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        [Require]
        [@DisplayName("Hộ tên khách hàng")]
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Hòm thư của khách hàng
        /// </summary>
        [Require]
        public string Email { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Mã nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }
        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }
        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Mã số thuế của công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }
        /// <summary>
        /// Số tiền nợ
        /// </summary>
        public double? DebitAmount { get; set; }
        /// <summary>
        /// Dừng theo dõi
        /// </summary>
        public int? IsStopFollow { get; set; }
        
        #endregion
    }
}
