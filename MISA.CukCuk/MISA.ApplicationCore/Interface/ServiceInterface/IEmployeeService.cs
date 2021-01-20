using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.ServiceInterface
{
    /// <summary>
    /// Nơi viết nghiệp vụ cho khách hàng
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Nhân viên</returns>
        public Employee GetEmployeeCodeMax();

        /// <summary>
        ///  Nghiệp vụ lấy danh sách nhân viên dựa theo bộ lọc
        /// </summary>
        /// <param name="value">Giá trị tìm kiếm</param>
        /// <param name="positionId">Mã chức vụ</param>
        /// <param name="departmentId">Mã phòng ban</param>
        /// <returns>Danh sách nhân viên</returns>
        public IEnumerable<Employee> GetEmployeesByFilters(string value, string positionId, string departmentId);
    }
}
