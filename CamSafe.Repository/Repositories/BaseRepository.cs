using Microsoft.Extensions.Configuration;

namespace CamSafe.Repository.Repositories
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; set; }

        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
