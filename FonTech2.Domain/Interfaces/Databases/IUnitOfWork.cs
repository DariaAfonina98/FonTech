using FonTech2.Domain.Entity;
using FonTech2.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FonTech2.Domain.Interfaces.Databases;

public interface IUnitOfWork : IStateSaveChanges
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    

    IBaseRepository<User> Users { get; set; }
    
    IBaseRepository<Role> Roles { get; set; }
    
    IBaseRepository<UserRole> UserRoles { get; set; }
}