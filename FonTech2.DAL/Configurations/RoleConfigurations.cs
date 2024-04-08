using FonTech2.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FonTech2.DAL.Configurations;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
   
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        
        builder.HasData(new List<Role>()
        {
            new Role()
            {
                Id=1,
                Name = "User"
                
            },
            new Role()
            {
                Id=2,
                Name = "Admin"
                
            },
            new Role()
            {
                Id=3,
                Name = "Moderator"
                
            }
        });

    }
}