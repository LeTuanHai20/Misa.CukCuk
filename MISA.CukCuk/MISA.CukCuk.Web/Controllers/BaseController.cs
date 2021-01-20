using System;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface.ServiceInterface;
using MISA.ApplicationCore.Entities.BaseEntities;
using MISA.ApplicationCore.Interface.BaseInterface;

namespace MISA.CukCuk.Web.Controllers
{
    /// <summary>
    /// Api chung cho các danh mục
    /// CreatedBy: LTHAI(3/12/2020)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase
    {
        #region Attribute
        private readonly IBaseService<TEntity> _baseService;
        #endregion

        #region Contructor
        public BaseController(IBaseService<TEntity> baseService)
        {
            this._baseService = baseService;
        }
        #endregion
        #region Controller
        /// <summary>
        /// Lấy toàn bộ danh sách thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.Gets();
            return Ok(entities);
        }

        /// <summary>
        /// Lấy khách hàng theo mã thực thể
        /// </summary>
        /// <param name="id"> Mã thực thể</param>
        /// <returns> Thực thể tương ứng</returns>
        /// CreatedBy: LTHAI(23/11/2020)
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {

            var entity = _baseService.GetById(id);
            if (entity != null)
            {
                return Ok(entity);
            }
            return BadRequest();
        }

        /// <summary>
        /// Thêm mới một thực thể
        /// </summary>
        /// <param name="customer">thực thể mới</param>
        /// <returns>
        /// + Thành công : Trả về thực thể mới
        /// + Không thành công: BadRequest 
        /// </returns>
        /// CreatedBy: LTHAI(3/12/2020)
        [HttpPost]
        public IActionResult Post([FromBody] TEntity entity)
        {
            var objResult = _baseService.Add(entity);
            if (((ServiceResult)objResult).MisaCode == MISACode.NotValid)
            {
                return BadRequest(objResult);
            }
            else if (((ServiceResult)objResult).MisaCode == MISACode.IsValid && Convert.ToInt32(((ServiceResult)objResult).MisaCode) > 0)
            {
                return Ok(objResult);
            }
            return NoContent();
        }

        /// <summary>
        /// Cập nhật một thực thể
        /// </summary>
        /// <param name="id">Mã đthực thể</param>
        /// <param name="value">Thông tin khách hàng được cập nhật</param>
        /// <returns>
        /// + Thành công : Trả về Ok
        /// + Không thành công: NoContent
        /// </returns>
        /// CreatedBy: LTHAI(3/12/2020)
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] TEntity entity)
        {

            var objResult = _baseService.Update(id, entity);
            if (((ServiceResult)objResult).MisaCode == MISACode.NotValid)
            {
                return BadRequest(objResult);
            }
            else if (((ServiceResult)objResult).MisaCode == MISACode.IsValid && Convert.ToInt32(((ServiceResult)objResult).MisaCode) > 0)
            {
                return Ok(objResult);
            }
            return NoContent();
        }

        /// <summary>
        /// Xóa một thực thể
        /// </summary>
        /// <param name="id">Mã thực thể</param>
        /// <returns>
        /// + Thành công : Trả về Ok
        /// + Không thành công: NoContent
        /// </returns>
        /// CreatedBy: LTHAI(3/12/2020)
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            var objResult = _baseService.Delete(id);
            if (((ServiceResult)objResult).MisaCode == MISACode.IsValid && Convert.ToInt32(((ServiceResult)objResult).MisaCode) > 0)
            {
                return Ok(objResult);
            }
            return NoContent();
        }
        #endregion
    }
}
