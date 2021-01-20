using MISA.ApplicationCore.BaseService;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Entities.BaseEntities;
using MISA.ApplicationCore.Interface.RepositoryInterface;
using MISA.ApplicationCore.Interface.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    /// <summary>
    /// Thực thi các phương thức của IDepartmentServiceRepository
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Attribute
        private readonly IDepartmentRepository _departmentRepository;

        #endregion
        #region Contructor
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }
        #endregion
        #region Hàm viết lại cho department từ base
        public override object Add(Department department)
        {
            // Validate dữ liệu 
            var serviceResult = base.Validate(department);
            if (serviceResult != null)
            {
                return serviceResult;
            }
            // Gọi hàm thêm mới khách hàng 
            var rowEffects = _departmentRepository.Add(department);
            return new ServiceResult() { Data = rowEffects, MisaCode = MISACode.IsValid };
        }

        public override object Delete(string departmentId)
        {
            var rowAffects = base.Delete(departmentId);
            return new ServiceResult() { Data = rowAffects, MisaCode = MISACode.IsValid };
        }
        public override object Update(string departmentId, Department department)
        {
            // Validate dữ liệu 
            department.entityState = EntityState.Update;
            var serviceResult = base.Validate(department, departmentId);
            if (serviceResult != null)
            {
                return serviceResult;
            }
            // Gọi cập nhật khách hàng 
            var rowAffects = _departmentRepository.Update(departmentId, department);
            return new ServiceResult() { Data = rowAffects, MisaCode = MISACode.IsValid };
        }
        #endregion
    }
}
