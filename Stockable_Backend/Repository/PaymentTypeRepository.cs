using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly AppDbContext _appDbContext;

        public PaymentTypeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //PaymentType
        //Get all paymentTypes
        public async Task<PaymentType[]> GetAllPaymentTypeAsync()
        {
            IQueryable<PaymentType> query = _appDbContext.PaymentTypes;

            return await query.ToArrayAsync();
        }

        //Get paymentType
        public async Task<PaymentType> GetPaymentTypeAsync(int paymentTypeId)
        {
            PaymentType paymentType = await _appDbContext.PaymentTypes
                .FirstOrDefaultAsync(x => x.paymentTypeId == paymentTypeId);

            return paymentType;
        }

        //Create paymentType
        public async Task<int> AddPaymentTypeAsync(PaymentTypeViewModal paymentType)
        {
            try
            {
                PaymentType paymentTypeAdd = new PaymentType
                {
                    paymentTypeName = paymentType.paymentTypeName,
                };

                await _appDbContext.PaymentTypes.AddAsync(paymentTypeAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
                
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update paymentType
        public async Task<int> UpdatePaymentTypeAsync(int paymentTypeId, PaymentTypeViewModal paymentType)
        {
            try
            {
                // Find the object in the db 
                PaymentType attemptToFindInDb = await _appDbContext.PaymentTypes.FirstOrDefaultAsync(x => x.paymentTypeId == paymentTypeId);

                if (attemptToFindInDb == null)
                {
                    return 404; // PaymentType not found
                }

                attemptToFindInDb.paymentTypeName = paymentType.paymentTypeName;
              
                _appDbContext.PaymentTypes.Update(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success

            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete paymentType
        public async Task<int> DeletePaymentTypeAsync(int paymentTypeId)
        {
            // Find the object in the db 
            PaymentType paymentTypeToDelete = await _appDbContext.PaymentTypes.FirstOrDefaultAsync(x => x.paymentTypeId == paymentTypeId);

            if (paymentTypeToDelete == null)
            {
                return 404; // PaymentType not found
            }

            _appDbContext.PaymentTypes.Remove(paymentTypeToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search paymentType
        public async Task<List<PaymentType>> SearchPaymentTypeAsync(string searchString)
        {
            List<PaymentType> paymentTypees = await _appDbContext.PaymentTypes.Where(x => x.paymentTypeName.Contains(searchString)).ToListAsync();

            return paymentTypees;
        }
    }
}
