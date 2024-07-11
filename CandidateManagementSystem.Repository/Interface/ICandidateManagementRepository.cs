using CandidateManagementSystem.Model.Request;
using CandidateManagementSystem.Model.Response;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface ICandidateManagementRepository<T> where T:class 
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> PostAsync(T source);
        Task<bool> DeleteAsync(int id);
        Task<bool> PutAsync(T source, int id);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest auth);
    }
}
