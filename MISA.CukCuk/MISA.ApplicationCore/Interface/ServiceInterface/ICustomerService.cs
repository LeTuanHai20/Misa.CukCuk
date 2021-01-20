using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface.BaseInterface;
using System;
using System.Collections.Generic;

namespace MISA.ApplicationCore.Interface.ServiceInterface
{
    /// <summary>
    /// Các phương thức thực hiện nghiệp vụ của khách hàng
    /// CreatedBy: LTHAI(25/11/2020)
    /// </summary>
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Lấy dữ liệu customer cho từng trang
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Danh sách khách hàng</returns>
        public IEnumerable<Customer> Panigation(int limit, int offset);
        /// <summary>
        /// Lấy danh sách khách hàng theo nhóm
        /// </summary>
        /// <param name="id">Khóa chính của nhóm khách hàng</param>
        /// <returns>Danh sách khách hàng</returns>
        public IEnumerable<Customer> GetCustomerByCustomerGroup(Guid id);
    }
}
