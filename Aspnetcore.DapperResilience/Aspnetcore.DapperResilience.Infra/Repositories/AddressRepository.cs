using Aspnetcore.DapperResilience.Domain.Entities;
using Aspnetcore.DapperResilience.Domain.Repositories;
using Aspnetcore.DapperResilience.Infra.DataContexts;
using System.Text;

namespace Aspnetcore.DapperResilience.Infra.Repositories
{
    public class AddressRepository : IAdressRepository
    {
        private readonly ISqlDapperClient _sqlDapperClient;

        public AddressRepository(ISqlDapperClient sqlDapperClient)
        {
            _sqlDapperClient = sqlDapperClient;
        }

        public async Task<IEnumerable<Address>> GetAddressesAsync()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT [ID]");
            query.AppendLine(",[CEP],[STREET],[DISTRICT],[CITY],[STATE]");
            query.AppendLine(" FROM[apirobustas].[dbo].[ADDRESS]");

            var address = await _sqlDapperClient.QueryAsync<Address>(query.ToString());
            return address;
        }

        public async Task<Address> GetAddressesAsync(Guid id)
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT [ID]");
            query.AppendLine(",[CEP],[STREET],[DISTRICT],[CITY],[STATE]");
            query.AppendLine(" FROM[apirobustas].[dbo].[ADDRESS]");
            query.AppendLine(" WHERE ID = @ID");

            var param = new { ID = id };

            var address = await _sqlDapperClient.QueryFirstOrDefaultAsync<Address>(query.ToString(), param);

            return address;
        }

        public async Task SaveAddressAsync(Address address)
        {
            var query = new StringBuilder();
            query.AppendLine(" INSERT INTO ADDRESS (ID,CEP,STREET,DISTRICT,CITY,STATE) VALUES(@ID,@CEP,@STREET,@DISTRICT,@CITY,@STATE)");

            var param = new
            {
                ID = address.Id,
                CEP = address.Cep,
                STREET = address.Street,
                DISTRICT = address.District,
                CITY = address.City,
                STATE = address.State
            };

            await _sqlDapperClient.ExecuteAsync(query.ToString(), param);
        }
    }
}
