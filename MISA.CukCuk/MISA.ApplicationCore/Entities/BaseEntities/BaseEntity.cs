using MISA.ApplicationCore.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace MISA.ApplicationCore.Entities.BaseEntities
{
    /// <summary>
    /// Attribute dùng để xác thực các thuộc tính bắt buộc
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Require: Attribute
    {

    }
    /// <summary>
    /// Attribute dùng để xác thực các thuộc tính đã tồn tại
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckExist : Attribute
    {

    }
    /// <summary>
    /// Attribute dùng để chỉ rõ khóa chính của bảng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }
    /// <summary>
    /// Attribute dùng để chỉ rõ độ dài cho phép cho các thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        public int Length { get; set; }
        public string ErrorMessage { get; set; }
        public MaxLength(int length, string errorMessage = "")
        {
            Length = length;
            ErrorMessage = errorMessage;
        }
    }
    /// <summary>
    /// Attribute dùng để hiển thị tên tiếng việt cho các thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name)
        {
            Name = name;
        }
    }
    /// <summary>
    /// Attribute dùng để định dạng email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomEmailAddress : Attribute
    {
        /// <summary>
        ///  Hàm kiểm tra định dạng email
        /// </summary>
        /// <param name="email">Gía trị email</param>
        /// <returns>Đúng : true, Sai : false</returns>
        public bool IsValid(string email)
        {
            Regex rgx = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!rgx.IsMatch(email))
            {
                return false;
            }

            return true;
        }

        public string GetErrorMessage()
        {
            return $"Please enter a valid email which ends with @mit.edu";
        }
    }
    /// <summary>
    /// Lưu các thuộc tính chung
    /// CreatedBy: LTHAI(30/11/2020)
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Trạng thái của phương thức
        /// </summary>
        public EntityState entityState { get; set; } = EntityState.Add;
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
