using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
   public  class DepartmentService :BaseService<Department>, IDepartmentService
    {
        public IDepartmentRepository _departmentRepository;

        public DepartmentService(IBaseRepository<Department> baseRepository, IDepartmentRepository departmentResponsitory) : base(baseRepository)
        {
            this._departmentRepository = departmentResponsitory;
        }
    }
}
