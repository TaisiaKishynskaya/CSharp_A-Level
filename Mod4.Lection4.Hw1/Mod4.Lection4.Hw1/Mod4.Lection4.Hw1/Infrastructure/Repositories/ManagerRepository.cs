using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Infrastructure.Context;


namespace Mod4.Lection4.Hw1.Infrastructure.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly Lazy<IOrderItemRepository> _orderItemRepository;
    private readonly Lazy<IOrderRepository> _orderRepository;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly Lazy<IAddressRepository> _addressRepository;
    private readonly Lazy<ISaveChangesRepository> _saveChangesRepository;

    public ManagerRepository(EFCoreContext efContext)
    {

        _saveChangesRepository = new Lazy<ISaveChangesRepository>(() => new SaveChangesRepository(efContext));
        _orderItemRepository = new Lazy<IOrderItemRepository>(() => new OrderItemRepository(efContext));
        _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(efContext));
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(efContext));
        _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(efContext));
        _addressRepository = new Lazy<IAddressRepository>(() => new AddressRepository(efContext));
    }

    public IOrderItemRepository OrderItemRepository => _orderItemRepository.Value;

    public IOrderRepository OrderRepository => _orderRepository.Value;

    public IUserRepository UserRepository => _userRepository.Value;

    public IProductRepository ProductRepository => _productRepository.Value;

    public IAddressRepository AddressRepository => _addressRepository.Value;
    
    public ISaveChangesRepository SaveChangesRepository => _saveChangesRepository.Value;
}