using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.BaseInterface
{
    /// <summary>
    /// Quy định các phương thức xử lý nghiệp vụ 
    /// </summary>
    /// <typeparam name="TEntity">Thực thể dạng generic</typeparam>
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ danh sách thực thể
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: LTHAI(23/11/2020)
        public IEnumerable<TEntity> Gets();
        /// <summary>
        ///   Lấy thực thể theo khóa chính 
        /// </summary>
        /// <param name="entityId">khóa chính</param>
        /// <returns>Một thực thể được quy định</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        public TEntity GetById(string entityId);
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin thực thể cần thêm</param>
        /// <returns>Object thông báo</returns>
        /// CreatedBy: LTHAI(23/11/2020)
        object Add(TEntity entity);
        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <param name="entity">Thông tin thực thể cần cập nhật</param>
        /// <returns>Object thông báo</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        object Update(string entityId, TEntity entity);
        /// <summary>
        /// Xóa theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: LTHAI(24/11/2020)
        object Delete(string entityId);
        /// <summary>
        /// Xác thực dữ liệu
        /// </summary>
        /// <param name="entity">thực thể cần xác thực</param>
        /// <param name="id">Khóa chính</param>
        /// <returns>đối tượng ghi lỗi</returns>
        ServiceResult Validate(TEntity entity, string id = null);
    }
}
