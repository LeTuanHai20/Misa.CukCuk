using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities.BaseEntities
{
    /// <summary>
    /// Lưu thông tin trả về 
    ///  CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// Thông tin cho dev
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Thông tin cho người dùng
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Mã thông báo nội bộ 
        /// </summary>
        public MISACode MisaCode { get; set; }

    }
}
