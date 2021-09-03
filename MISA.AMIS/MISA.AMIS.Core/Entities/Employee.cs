using MISA.AMIS.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Employee : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }
        [MISARequired("Mã nhân viên")]
        [MISACheckExist("EntityCode","Mã nhân viên")]
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string EmployeeCode { get; set; }
        [MISARequired("Họ và tên")]
      //  [MISAFormat("Alphabet", "Họ và tên")]
        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính (0 - Nữ, 1 - Nam, 2 - Khác)
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Giới tính 
        /// </summary>
        public string GenderName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        [MISARequired("Số điện thoại di động")]
        [MISAFormat("Number","Số điện thoại di động")]
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string MobilePhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string TelephoneNumber { get; set; }
        [MISAFormat("Email","Email")]
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số Căn cước công dân
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp Căn cước công dân
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp Căn cước công dân
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Khóa ngoại phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string BankBranch { get; set; }      
        #endregion
    }
}
