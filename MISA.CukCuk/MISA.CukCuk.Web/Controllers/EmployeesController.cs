using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Entities.BaseEntities;
using MISA.ApplicationCore.Interface.ServiceInterface;

namespace MISA.CukCuk.Web.Controllers
{
    /// <summary>
    /// Api danh mục nhân viên
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        #region Attribute
        private readonly IEmployeeService _employeeService;
        #endregion
        #region Contructor
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            this._employeeService = employeeService;
        }
        #endregion
        /// <summary>
        /// Lấy danh sách trạng thái làm việc
        /// CreatedBy: LTHAI(1/12/2020)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("employeecodemax")]
        public IActionResult GetEmployeeCodeMax()
        {
            var employeeCodeMax = _employeeService.GetEmployeeCodeMax();
            if (employeeCodeMax != null)
            {
                return Ok(employeeCodeMax);
            }
            return BadRequest();
        }
        /// <summary>
        /// Lấy danh sách nhân viên theo giá trị nhập
        /// CreatedBy: LTHAI(2/12/2020)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("filter")]
        public IActionResult Gets([FromQuery] string value, [FromQuery] string positionId, [FromQuery] string departmentId)
        {
            var employees = _employeeService.GetEmployeesByFilters(value, positionId, departmentId);
            if (employees.Count() > 0)
            {
                return Ok(employees);
            }
            return BadRequest();
        }
    }
}
