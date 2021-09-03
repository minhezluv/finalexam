using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity>
    {
        #region Methods
        /// <summary>
        /// Lấy danh sách entity
        /// </summary>
        /// <returns>Danh sách entity</returns>
        List<MISAEntity> GetAll();
        /// <summary>
        /// Lấy entity theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>entity theo id</returns>
        MISAEntity GetById(Guid entityId);
        /// <summary>
        /// Thêm entity
        /// </summary>
        /// <param name="entity"></param>
    
        Int32 Add(MISAEntity entity);
        /// <summary>
        /// Update chỉnh sủa enityt
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Int32 Update(Guid id, MISAEntity entity);
        /// <summary>
        /// Xóa entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Int32 Delete(Guid entityId);
        /// <summary>
        /// Kiểm tra mã entity đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>true-chưa tồn tại\false-tồn tại</returns>
        bool CheckCodeExists(string entityCode);
        /// <summary>
        /// kiểm tra email đã tồn tại hay chưa
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true-chưa tồn tại\false-tồn tại</returns>
        bool CheckEmailExists(string email);
        /// <summary>
        /// kiểm tra số điện thoại đã tồn tại hay chưa
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns>true-chưa tồn tại\false-tồn tại</returns>
        bool CheckMobilePhoneNumber(string mobilePhoneNumber);
        #endregion
    }
}
