using MISA.ApplicationCore.BaseService;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Entities.BaseEntities;
using MISA.ApplicationCore.Interface.BaseInterface;
using MISA.ApplicationCore.Interface.RepositoryInterface;
using MISA.ApplicationCore.Interface.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    /// <summary>
    /// Thực thi các phương thức của IEmployeeServiceRepository
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Attribute
        private readonly IEmployeeRepository _employeeRepository;

        #endregion
        #region Contructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }


        #endregion
        #region Hàm viết lại cho employee từ base
        public override object Add(Employee employee)
        {
            // Validate dữ liệu 
            var serviceResult = base.Validate(employee);
            if (serviceResult != null)
            {
                return serviceResult;
            }
            // Gọi hàm thêm mới khách hàng 
            var rowEffects = _employeeRepository.Add(employee);
            return new ServiceResult() { Data = rowEffects, MisaCode = MISACode.IsValid };
        }
        public override object Update(string employeeId, Employee employee)
        {
            // Validate dữ liệu 
            employee.entityState = EntityState.Update;
            var serviceResult = base.Validate(employee, employeeId);
            if (serviceResult != null)
            {
                return serviceResult;
            }
            // Gọi cập nhật khách hàng 
            var rowAffects = _employeeRepository.Update(employeeId, employee);
            return new ServiceResult() { Data = rowAffects, MisaCode = MISACode.IsValid };
        }
        public override object Delete(string employeeId)
        {
            var rowAffects = base.Delete(employeeId);
            return new ServiceResult() { Data = rowAffects, MisaCode = MISACode.IsValid };
        }

        public Employee GetEmployeeCodeMax()
        {
            return _employeeRepository.GetEmployeeCodeMax();
        }
        public IEnumerable<Employee> GetEmployeesByFilters(string value, string positionId, string departmentId)
        {
            return _employeeRepository.GetEmployeesByFilters(value, positionId, departmentId);
        }
        #endregion
        #region Hàm riêng cho employee

        #endregion

    }
}
