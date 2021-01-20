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
    /// Thực thi các phương thức của IPositionServiceRepository
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public class PositionService : BaseService<Position>, IPositionService
    {
        #region Attribute
        private readonly IPositionRepository _positionRepository;

        #endregion
        #region Contructor
        public PositionService(IPositionRepository positionRepository) : base(positionRepository)
        {
            this._positionRepository = positionRepository;
        }
        #endregion
        #region Hàm viết lại cho department từ base
        public override object Add(Position position)
        {
            // Validate dữ liệu 
            var serviceResult = base.Validate(position);
            if (serviceResult != null)
            {
                return serviceResult;
            }
            // Gọi hàm thêm mới khách hàng 
            var rowEffects = _positionRepository.Add(position);
            return new ServiceResult() { Data = rowEffects, MisaCode = MISACode.IsValid };
        }

        public override object Delete(string positionId)
        {
            var rowAffects = base.Delete(positionId);
            return new ServiceResult() { Data = rowAffects, MisaCode = MISACode.IsValid };
        }
        public override object Update(string positionId, Position position)
        {
            // Validate dữ liệu 
            position.entityState = EntityState.Update;
            var serviceResult = base.Validate(position, positionId);
            if (serviceResult != null)
            {
                return serviceResult;
            }
            // Gọi cập nhật khách hàng 
            var rowAffects = _positionRepository.Update(positionId, position);
            return new ServiceResult() { Data = rowAffects, MisaCode = MISACode.IsValid };
        }
        #endregion
    }
}
