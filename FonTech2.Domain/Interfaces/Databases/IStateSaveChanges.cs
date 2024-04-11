namespace FonTech2.Domain.Interfaces.Databases;

public interface IStateSaveChanges
{
    Task<int> SaveChangesAsync();
}