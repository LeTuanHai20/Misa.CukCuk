using MISA.ApplicationCore.Entities.BaseEntities;
using MISA.ApplicationCore.Interface.BaseInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MISA.ApplicationCore.BaseService
{
    /// <summary>
    /// Nơi viết các nghiệp vụ chung 
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity: BaseEntity
    {
        #region Attribute
        private readonly IBaseRepository<TEntity> _baseRepository;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            this._baseRepository = baseRepository;
        }
        #endregion

        #region Method
        public virtual object Add(TEntity entity)
        {
            return _baseRepository.Add(entity);
        }

        public virtual object Delete(string entityId)
        {
            return _baseRepository.Delete(entityId);
        }

        public TEntity GetById(string entityId)
        {
            return _baseRepository.GetById(entityId);
        }

        public IEnumerable<TEntity> Gets()
        {
            return _baseRepository.Gets();
        }
        public virtual object Update(string entityId, TEntity entity)
        {
            return _baseRepository.Update(entityId, entity);
        }
        public ServiceResult Validate(TEntity entity, string id = null)
        {
            var serviceResult = new ServiceResult();
            // Đọc các property 
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                // giá trị của thuộc tính
                var propertyValue = property.GetValue(entity);
                //Check attribute bắt buộc nhập
                if (property.IsDefined(typeof(Require), false))
                {
                    if (propertyValue == null)
                    {
                        var displayName = property.GetCustomAttributes(typeof(DisplayName), true)[0];
                        var propertyName = (displayName as DisplayName).Name;
                        serviceResult.Data = new { fieldName = property.Name, Msg = $"Trường {propertyName} không được để trống" };
                        serviceResult.Message = $"Trường {propertyName} không được để trống";
                        serviceResult.MisaCode = MISACode.NotValid;
                        return serviceResult;
                    }
                }
                // Check trùng lặp dữ liệu
                if (property.IsDefined(typeof(CheckExist), false))
                {
                    var entityExist = _baseRepository.GetEntityByProperty(entity, property, id);
                    if (entityExist != null)
                    {
                        var displayName = property.GetCustomAttributes(typeof(DisplayName), true)[0];
                        var propertyName = (displayName as DisplayName).Name;
                        serviceResult.Data = new { fieldName = property.Name, Msg = $"Trường {propertyName} đã tồn tại" };
                        serviceResult.Message = $"Trường {propertyName} đã tồn tại";
                        serviceResult.MisaCode = MISACode.NotValid;
                        return serviceResult;
                    }
                }
                // Check độ dài dữ liệu
                if (property.IsDefined(typeof(MaxLength), false))
                {
                    var AttributeMaxLength = property.GetCustomAttributes(typeof(MaxLength), true)[0];
                    int length = (AttributeMaxLength as MaxLength).Length;
                    string msg = (AttributeMaxLength as MaxLength).ErrorMessage;
                    if (propertyValue.ToString().Trim().Length > length)
                    {
                        var displayName = property.GetCustomAttributes(typeof(DisplayName), true)[0];
                        var propertyName = (displayName as DisplayName).Name;
                        serviceResult.Data = new { fieldName = property.Name, msg };
                        serviceResult.Message = msg;
                        serviceResult.MisaCode = MISACode.NotValid;
                        return serviceResult;
                    }
                }
                // Kiểm tra định dạng email
                if(property.IsDefined(typeof(CustomEmailAddress), false))
                {
                    var AttributeEmailAddress = property.GetCustomAttributes(typeof(CustomEmailAddress), true)[0];
                    bool isEmail = (AttributeEmailAddress as CustomEmailAddress).IsValid(propertyValue.ToString());
                    if (!isEmail)
                    {
                        var displayName = property.GetCustomAttributes(typeof(DisplayName), true)[0];
                        var propertyName = (displayName as DisplayName).Name;
                        serviceResult.Data = new { fieldName = property.Name, Msg = $"Trường {propertyName} đã tồn tại" };
                        serviceResult.Message = $"Trường {propertyName} đã  không đúng định dạng của Email";
                        serviceResult.MisaCode = MISACode.NotValid;
                        return serviceResult;
                    }
                }
            }
            return null;
        }
        #endregion

    }
}
