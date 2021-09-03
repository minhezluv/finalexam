using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseEntitiesController<Department>
    {
        IDepartmentService _DepartmentService;
        public DepartmentsController(IDepartmentService departmentService, IBaseRepository<Department> baseRepository) : base(departmentService, baseRepository)
        {
            //_customerRepository = customerRepository;
            _DepartmentService = departmentService;
        }
    }
}
