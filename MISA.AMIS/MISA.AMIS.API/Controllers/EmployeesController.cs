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
    public class EmployeesController : BaseEntitiesController<Employee>
    {
        IEmployeeService _EmployeeService;
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepository employeeRepository) : base(employeeService, employeeRepository)
        {
            //_employeeRepository = employeeRepository;
            _EmployeeService = employeeService;
        }
    }
}
