using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeTypeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Emplpyee type
        //Get a employee type
        public async Task<EmployeeType> GetEmployeeTypeAsync(int EmployeeTypeId)
        {
            EmployeeType employeeType = await _appDbContext.EmployeeTypes
                .FirstOrDefaultAsync(x => x.employeeTypeId == EmployeeTypeId);

            return employeeType;
        }


        //Get all Employeetypes
        public async Task<EmployeeType[]> GetAllEmployeeTypesAsync()
        {
            IQueryable<EmployeeType> query = _appDbContext.EmployeeTypes;
            return await query.ToArrayAsync();
        }

        //Create Employeetype
        public async Task<int> AddEmployeeTypeAsync(EmployeeTypeViewModal employeeType)
        {
            try
            {
                if (employeeType.employeeTypeName.Any(char.IsDigit) ||
                    employeeType.employeeTypeName.Any(ch => !Char.IsLetterOrDigit(ch)))
                {
                    return 400;
                }
                else
                {
                    var employeeTypeAdd = new EmployeeType
                    {
                        employeeTypeName = employeeType.employeeTypeName,
                        employeeTypeDescription = employeeType.employeeTypeDescription
                    };

                    _appDbContext.EmployeeTypes.Add(employeeTypeAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        //Update Employeetype
        public async Task<int> UpdateEmployeeTypeAsync(int employeeTypeID, EmployeeTypeViewModal employeeType)
        {
            var existingEmployeeType = await _appDbContext.EmployeeTypes.FindAsync(employeeTypeID);

            if (existingEmployeeType == null)
            {
                return 404;
            }

            if (employeeType.employeeTypeName.Any(char.IsDigit) ||
                employeeType.employeeTypeName.Any(ch => !Char.IsLetterOrDigit(ch)))
            {
                return 400;
            }

            existingEmployeeType.employeeTypeName = employeeType.employeeTypeName;
            existingEmployeeType.employeeTypeDescription = employeeType.employeeTypeDescription;

            await _appDbContext.SaveChangesAsync();

            return 200;
        }


        //Delete Employeetype
        public async Task<int> DeleteEmployeeTypeAsync(int employeeTypeId)
        {
            var employeeType = await _appDbContext.EmployeeTypes.FindAsync(employeeTypeId);

            if (employeeType == null)
            {
                return 404;
            }

            _appDbContext.EmployeeTypes.Remove(employeeType);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        //Search employeeType
        public async Task<List<EmployeeType>> SearchEmployeeTypeAsync(string searchString)
        {
            List<EmployeeType> employeeTypes = await _appDbContext.EmployeeTypes.Where(x => x.employeeTypeName.Contains(searchString)).ToListAsync();

            return employeeTypes;
        }

        public async Task<IEnumerable<EmployeeType>> GetAllEmployeeTypesFromStoredProcedureAsync()
        {
            return await _appDbContext.EmployeeTypes.FromSqlRaw("GetAllEmployeeTypes").ToListAsync();
        }
    }
}
