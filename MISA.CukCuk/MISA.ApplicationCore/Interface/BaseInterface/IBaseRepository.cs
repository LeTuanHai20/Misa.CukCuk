using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.ApplicationCore.Interface.BaseInterface
{
    /// <summary>
    /// Quy định các phương thức thực hiện truy vấn database chung
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ danh sách thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        public IEnumerable<TEntity> Gets();

        /// <summary>
        ///   Lấy khách hàng theo khóa chính 
        /// </summary>
        /// <param name="entityId">khóa chính</param>
        /// <returns>Một thực thể</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        public TEntity GetById(string entityId);
        /// <summary>
        /// Lấy khách hàng theo mã thực thể 
        /// </summary>
        /// <param name="customerCode">Mã thực thể</param>
        /// <returns>Trả về một thực thể đầu tiên nếu có</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        public TEntity GetByCode(string entityCode);
        /// <summary>
        /// Thêm mới thực thể
        /// </summary>
        /// <param name="customer">Thông tin thực thể mới</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        public int Add(TEntity entity);
        /// <summary>
        /// Sửa thông tin thực thể
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <param name="customer">Thông tin thực thể cần cập nhật</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        public int Update(string entityId, TEntity entity);
        /// <summary>
        ///  Xóa thực thể theo khóa chính 
        /// </summary>
        /// <param name="id">Khóa chính </param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        public int Delete(string entityId);
          /// <summary>
          /// Lấy thông tin của thực thể theo thuộc tính chỉ định
          /// </summary>
          /// <param name="entity">Thực thể</param>
          /// <param name="propertyInfo">Thuộc tính chỉ định</param>
          /// <param name="id">Khóa chính</param>
          /// <returns></returns>
        public TEntity GetEntityByProperty(TEntity entity, PropertyInfo propertyInfo, string id = null);
    }
}
