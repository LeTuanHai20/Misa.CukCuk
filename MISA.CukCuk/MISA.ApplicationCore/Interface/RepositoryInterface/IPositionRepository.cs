using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.RepositoryInterface
{
    /// <summary>
    /// Quy định các phương thức thực hiện truy vấn database của chức vụ
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public interface IPositionRepository : IBaseRepository<Position>
    {
    }
}
