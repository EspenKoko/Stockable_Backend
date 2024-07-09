using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Employee

        //get all employees
        public async Task<Employee[]> GetAllEmployeesAsync()
        {
            IQueryable<Employee> query = _appDbContext.Employees.Include(e => e.employeeType).Include(e => e.user);
            return await query.ToArrayAsync();
        }

        //Get employee
        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {

            Employee employee = await _appDbContext.Employees.Include(e => e.user).Include(e => e.employeeType).FirstOrDefaultAsync(x => x.employeeId == employeeId);

            return employee;
        }

        //Create employee
        public async Task<int> AddEmployeeAsync(EmployeeViewModal employee)
        {
            try
            {

                EmployeeType employeeType = await _appDbContext.EmployeeTypes.FindAsync(employee.employeeTypeId);
                User user = await _appDbContext.Users.FindAsync(employee.userId);

                if (employeeType != null && user != null)
                {
                    Employee employeeAdd = new Employee
                    {
                        empHireDate = employee.empHireDate,
                        employeeTypeId = employee.employeeTypeId,
                        userId = employee.userId,
                    };

                    await _appDbContext.Employees.AddAsync(employeeAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success

                }
                
                return 404; // Invalid employee type
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }
        }

        //update employee
        public async Task<int> UpdateEmployeeAsync(int employeeId, EmployeeViewModal employee)
        {
            try
            {
                Employee attemptToFindInDb = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.employeeId == employeeId);
                if (attemptToFindInDb == null)
                {
                    return 404; // Employee not found
                }

                attemptToFindInDb.empHireDate = employee.empHireDate;

                EmployeeType employeeType = await _appDbContext.EmployeeTypes.FindAsync(employee.employeeTypeId);
                User user = await _appDbContext.Users.FindAsync(employee.userId);

                if (employeeType != null && user != null)
                {
                    attemptToFindInDb.employeeTypeId = employee.employeeTypeId;
                    attemptToFindInDb.userId = employee.userId;

                    _appDbContext.Employees.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();


                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid employee type
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }
        }


        //Delete employee
        public async Task<int> DeleteEmployeeAsync(int employeeId)
        {
            Employee employeeToDelete = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.employeeId == employeeId);

            if (employeeToDelete == null)
            {
                return 404; // Employee not found
            }

            _appDbContext.Employees.Remove(employeeToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        //Search employee
        public async Task<List<Employee>> SearchEmployeeAsync(string searchString)
        {
            List<Employee> employees = await _appDbContext.Employees.Include(c => c.employeeType).Include(c => c.user).Where(x => x.user.userFirstName.Contains(searchString)).ToListAsync();

            return employees;
        }
    }
}
