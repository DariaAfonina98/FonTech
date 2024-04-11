using FonTech2.Domain.Entity;
using FonTech2.Domain.Interfaces.Databases;
using FonTech2.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FonTech2.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext, IBaseRepository<User> users, IBaseRepository<Role> roles, IBaseRepository<UserRole> userRoles)
    {
        _dbContext = dbContext;
        Users = users;
        Roles = roles;
        UserRoles = userRoles;
    }

   

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
      return await _dbContext.SaveChangesAsync();
    }

    public IBaseRepository<User> Users { get; set; }
    public IBaseRepository<Role> Roles { get; set; }
    
    public IBaseRepository<UserRole> UserRoles { get; set; }
}