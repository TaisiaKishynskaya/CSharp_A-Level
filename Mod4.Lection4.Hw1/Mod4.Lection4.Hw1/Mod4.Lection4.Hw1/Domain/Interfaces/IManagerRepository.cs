namespace Mod4.Lection4.Hw1.Domain.Interfaces;

public interface IManagerRepository
{
    public IUserRepository UserRepository { get; }
    public IAddressRepository AddressRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderItemRepository OrderItemRepository { get; }
    public ISaveChangesRepository SaveChangesRepository { get; }
}
