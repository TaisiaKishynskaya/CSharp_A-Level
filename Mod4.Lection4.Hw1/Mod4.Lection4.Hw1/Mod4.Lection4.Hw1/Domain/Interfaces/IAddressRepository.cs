using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Domain.Interfaces;

public interface IAddressRepository
{
    Task CreateAddressAsync(Address address);
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(Address address);
    Task<Address> GetAddressAsync(Guid addressId);
    Task<IReadOnlyCollection<Address>> GetAllAddressesAsync();
    Task AddUserAsync(Guid id, User user);
}
