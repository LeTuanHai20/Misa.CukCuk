using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities.BaseEntities
{
    /// <summary>
    /// MISACode để xác định trạng thái của việc validate
    /// </summary>
    public enum MISACode
    {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        IsValid = 100,

        /// <summary>
        /// Dữ liệu chưa hợp lệ
        /// </summary>
        NotValid = 900,

        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200
    }
    /// <summary>
    /// Các trạng thái khi giao tiếp với cơ sở dữ liệu
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public enum EntityState
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        Add = 1,
        /// <summary>
        /// Cập nhật
        /// </summary>
        Update = 2,
        /// <summary>
        /// Xóa
        /// </summary>
        Delete = 3
    }
}
