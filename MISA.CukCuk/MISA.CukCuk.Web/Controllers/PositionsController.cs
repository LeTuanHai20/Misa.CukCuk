using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface.ServiceInterface;

namespace MISA.CukCuk.Web.Controllers
{
    /// <summary>
    /// Api danh mục chức vụ
    /// CreatedBy: LTHAI(23/11/2020)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : BaseController<Position>
    {
        #region Attribute
        private readonly IPositionService _positionService;
        #endregion

        #region Contructor
        public PositionsController(IPositionService positionService):base(positionService)
        {
            this._positionService = positionService;
        }
        #endregion

    }
}
