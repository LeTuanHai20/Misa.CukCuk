using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.RepositoryInterface
{
    /// <summary>
    /// Quy định các phương thức thực hiện truy vấn database của nhân viên
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Nhân viên</returns>
        /// CreatedBy: LTHAI(2/12/2020)
        public Employee GetEmployeeCodeMax();
        /// <summary>
        ///  Lấy danh sách nhân viên dựa theo bộ lọc
        /// </summary>
        /// <param name="value">Giá trị tìm kiếm</param>
        /// <param name="positionId">Mã chức vụ</param>
        /// <param name="departmentId">Mã phòng ban</param>
        /// <returns>Danh sách nhân viên</returns>
        public IEnumerable<Employee> GetEmployeesByFilters(string value, string positionId, string departmentId);

       
    }
}
