using MISA.ApplicationCore.Entities;
using System.Collections.Generic;
using MISA.ApplicationCore.Interface;
using System;
using MISA.ApplicationCore.BaseService;
using MISA.ApplicationCore.Entities.BaseEntities;
using MISA.ApplicationCore.Interface.RepositoryInterface;
using MISA.ApplicationCore.Interface.ServiceInterface;

namespace MISA.ApplicationCore.Service
{
    /// <summary>
    /// Thực thi các phương thức của ICustomerServiceRepository
    /// CreatedBy: LTHAI(25/11/2020)
    /// </summary>
    public class CustomerService: BaseService<Customer>, ICustomerService
    {

        #region Attribute
        private readonly ICustomerRepository _customerRepository;

        #endregion
        #region Contructor
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        #endregion
        #region Hàm viết lại cho customer từ base
        public override object Add(Customer customer)
        {
            // Validate dữ liệu 
            var serviceResult = base.Validate(customer);
            if (serviceResult != null)
            {
                return serviceResult;
            }
            // Gọi hàm thêm mới khách hàng 
            var rowEffects = _customerRepository.Add(customer);
            return new ServiceResult() { Data = rowEffects, MisaCode = MISACode.IsValid };
        }

        public override object Delete(string customerId)
        {
            var rowAffects = base.Delete(customerId);
            return new ServiceResult() { Data = rowAffects, MisaCode = MISACode.IsValid };
        }
        public override object Update(string customerId, Customer customer)
        {
            // Validate dữ liệu 
            customer.entityState = EntityState.Update;
            var serviceResult = base.Validate(customer, customerId);
            if (serviceResult != null)
            {
                return serviceResult;
            }
            // Gọi cập nhật khách hàng 
            var rowAffects = _customerRepository.Update(customerId, customer);
            return new ServiceResult() { Data = rowAffects, MisaCode = MISACode.IsValid };
        }
        #endregion
        #region Hàm riêng cho customer
        public IEnumerable<Customer> GetCustomerByCustomerGroup(Guid id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Customer> Panigation(int limit, int offset)
        {
            throw new NotImplementedException();
        }
        #endregion
      

    }
}
