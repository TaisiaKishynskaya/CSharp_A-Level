using Microsoft.EntityFrameworkCore;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;
using Mod4.Lection4.Hw1.Infrastructure.Context;

namespace Mod4.Lection4.Hw1.Infrastructure.Repositories;

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(EFCoreContext eFContext) : base(eFContext) { }

    public async Task CreateAddressAsync(Address address) => await CreateAsync(address);

    public async Task UpdateAddressAsync(Address address) => await UpdateAsync(address);

    public async Task DeleteAddressAsync(Address address) => await DeleteAsync(address);

    public async Task<IReadOnlyCollection<Address>> GetAllAddressesAsync() => await FindAllAsync().ToListAsync();

    public async Task<Address?> GetAddressAsync(Guid orderId)
    {
        return await FindByConditionAsync(x => x.Id == orderId).FirstOrDefaultAsync();
    }

    public async Task AddUserAsync(Guid id, User user)
    {
        var address = await GetAddressAsync(id);
        
        if (address == null) return;

        address.User = user;
    }
}
