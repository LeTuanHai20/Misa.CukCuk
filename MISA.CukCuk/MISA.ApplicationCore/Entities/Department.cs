using System;
using System.Collections.Generic;
using System.Text;
using MISA.ApplicationCore.Entities.BaseEntities;
namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Phòng ban 
    /// CreatedBy:LTHAI(30/11/2020)
    /// </summary>
    public class Department: BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Require]
        [CheckExist]
        [DisplayName("Tên phòng ban")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
}
