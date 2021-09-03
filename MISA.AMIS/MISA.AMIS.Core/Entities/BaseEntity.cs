using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class BaseEntity
    {
        #region BaseProperty
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Được tạo bởi
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Chỉnh sửa bởi
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}
