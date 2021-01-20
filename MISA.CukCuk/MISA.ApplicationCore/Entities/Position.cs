using System;
using System.Collections.Generic;
using System.Text;
using MISA.ApplicationCore.Entities.BaseEntities;
namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Chức vụ
    /// CreatedBy:LTHAI(30/11/2020)
    /// </summary>
    public class Position : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid PositionId { get; set; }
        /// <summary>
        /// Mã chức vụ
        /// </summary>
        public string PositionCode { get; set; }
        /// <summary>
        /// Tên chức vụ
        /// </summary>
        [Require]
        [CheckExist]
        [DisplayName("Tên chức vụ")]
        public string PositionName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
}
