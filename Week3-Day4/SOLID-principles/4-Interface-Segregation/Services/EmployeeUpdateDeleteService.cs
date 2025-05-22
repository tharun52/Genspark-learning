namespace WholeApplication.Services
{
    public class EmployeeUpdateDeleteService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeUpdateDeleteService(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            return _repository.Update(updatedEmployee);
        }

        public Employee DeleteEmployee(int id)
        {
            return _repository.Delete(id);
        }

        public Employee GetEmployeeById(int id)
        {
            return _repository.GetById(id);
        }

        
    }
}
