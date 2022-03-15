using Aspnetcore.DapperResilience.Domain.Entities;


namespace Aspnetcore.DapperResilience.Domain.Repositories
{
    public interface IAdressRepository
    {
        Task SaveAddressAsync(Address address);
        Task<IEnumerable<Address>> GetAddressesAsync();
        Task<Address> GetAddressesAsync(Guid id);
    }
}
