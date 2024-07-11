using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Request;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Helper.Interface;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using Microsoft.EntityFrameworkCore;


namespace CandidateManagementSystem.Repository
{
    public class CandidateManagementRepository<T> : ICandidateManagementRepository<T> where T : class
    {
        #region private variables
        private readonly IJwtUtils _jwt;
        private readonly ApplicationDbContext _context;
        
        #endregion

        #region constructor
        public CandidateManagementRepository(ApplicationDbContext Context,IJwtUtils jwt)
        {
            _jwt = jwt;
            _context = Context ?? throw new ArgumentNullException(nameof(Context));
        }

        public DbSet<T> Entities => _context.Set<T>();
        #endregion

        #region generic methods
        /// <summary>
        /// Generic mehtod for perform delete operation
        /// </summary>
        /// <param name="id">Id of the data</param>
        /// <returns>Return true or false</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _context.Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting data.", ex);
            }
        }

        /// <summary>
        /// Generic method for get list of record
        /// </summary>
        /// <returns>Return list of record</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await Entities.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving data.", ex);
            }
        }

        /// <summary>
        /// Generic method for get single record by id
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <returns>Return single record by id</returns>
        /// <exception cref="Exception"></exception>
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                // Assuming 'Id' is the primary key property in the entity type T
                return await Entities.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving data.", ex);
            }
        }
       
        /// <summary>
        /// Generic method for insert record
        /// </summary>
        /// <param name="source">Record that is inserted</param>
        /// <returns>Return inserted record</returns>
        /// <exception cref="Exception"></exception>

        public async Task<T> PostAsync(T source)
        {
            try
            {
                await Entities.AddAsync(source);
                await _context.SaveChangesAsync();
                return source;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving data.", ex);
            }
        }
        /// <summary>
        /// Generic method for update record
        /// </summary>
        /// <param name="source">Record that is updated</param>
        /// <param name="id">Record id</param>
        /// <returns>Return updated record</returns>
        /// <exception cref="Exception"></exception>

        public async Task<bool> PutAsync(T source, int id)
        {
            try
            {
               
                _context.Update(source);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating data.", ex);
            }
        }
        #endregion

        #region custom methods
        /// <summary>
        /// Use for sign in user
        /// </summary>
        /// <param name="auth">username and password</param>
        /// <returns></returns>


        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest auth)
        {
            try
            {
                var user = await _context.UserModels.FirstOrDefaultAsync(x => x.Email == auth.Email && x.Password == auth.Password);
                if (user == null)
                    return new AuthenticateResponse(new UserModel(), null, 0);

                var token = _jwt.GenerateJwtToken(user.Id); // Assuming _jwt is already injected
                return new AuthenticateResponse(user, token, user.Role);
            }
            catch (Exception ex)
            { 
                throw new Exception("Error occurred while authenticating user.", ex);
            }
        }
        #endregion
    }
}
