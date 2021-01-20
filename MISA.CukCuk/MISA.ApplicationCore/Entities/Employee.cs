using System;
using System.Collections.Generic;
using System.Text;
using MISA.ApplicationCore.Entities.BaseEntities;
namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    /// CreatedBy:LTHAI(30/11/2020)
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        [DisplayName("Khóa chính")]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Require]
        [CheckExist]
        [DisplayName("Mã nhân viên")]
        public string EmployeeCode { get; set; }
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
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string GenderName {
            get
            {
                switch (Gender)
                {
                    case 0: 
                        {
                            return "Nữ";
                        }
                    case 1:
                        {
                            return "Nam";
                        }
                    default:
                        {
                            return "Khác";
                        }

                }
            }
         }
        /// <summary>
        /// Số CMTND/Hộ chiếu
        /// </summary>
        [Require]
        [DisplayName("Số CMTND")]
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Hòm thư của khách hàng
        /// </summary>
        /// 
        [Require]
        [DisplayName("Hòm thư")]
        [CustomEmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Require]
        [MaxLength(10)]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Mã vị trí
        /// </summary>
        public Guid? PositionId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// Mã số thuế cá nhân
        /// </summary>
        public string PersonalTaxCode { get; set; }
        /// <summary>
        /// Ngày gia nhập
        /// </summary>
        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// Lương
        /// </summary>
        public double? Salary { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public int? WorkStatus { get; set; }
        /// <summary>
        /// Trạng thái làm việc
        /// </summary>
        public string WorkStatusName {
            get
            {
                switch (WorkStatus)
                {
                    case 0:
                        {
                            return "Đang làm việc";
                        }
                    case 1:
                        {
                            return "Đã nghỉ việc";
                        }
                    default:
                        {
                            return "Khác";
                        }

                }
            }
        }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName { get; set; }
        #endregion

    }
}
