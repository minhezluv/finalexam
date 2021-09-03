using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        public IEmployeeRepository _employeeRepository;

        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeResponsitory) : base(baseRepository)
        {
            this._employeeRepository = employeeResponsitory;
        }
    }
}
