
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IClientUserRequestRepository
    {
        //ClientUserRequest
        Task<ClientUserRequest> GetClientUserRequestAsync(int clientUserRequestId);
        Task<ClientUserRequest[]> GetAllClientUserRequestsAsync();
        Task<int> AddClientUserRequestAsync(ClientUserRequestViewModal clientUserRequest);
        Task<int> UpdateClientUserRequestAsync(int clientUserRequestId, ClientUserRequestViewModal clientUserRequest);
        Task<int> DeleteClientUserRequestAsync(int clientUserRequestId);
        Task<List<ClientUserRequest>> SearchClientUserRequestAsync(string searchString);
    }
}
