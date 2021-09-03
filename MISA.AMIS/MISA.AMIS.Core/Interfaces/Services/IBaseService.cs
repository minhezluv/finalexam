using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Services
{
    public  interface IBaseService<MISAEntity>
    {
        #region Methods
        /// <summary>
        /// Xử lí nghiệp vụ lấy tất cả entity
        /// </summary>
        /// <returns></returns>
        ServiceResult GetAll();
        /// <summary>
        /// Xử lí nghiệp vụ lấy entity theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult GetById(Guid id);
        /// <summary>
        /// Xử lí nghiệp vụ thêm entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult Add(MISAEntity entity);
        /// <summary>
        /// Xử lí nghiệp vụ cập nhật entity sau chỉnh sửa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult Update(Guid id, MISAEntity entity);
        /// <summary>
        /// Xử lí nghiệp vụ xóa entity theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult Delete(Guid id);

      
        #endregion
    }
}
