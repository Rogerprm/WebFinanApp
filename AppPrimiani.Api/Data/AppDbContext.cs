using AppPrimiani.Api.Models;
using AppPrimiani.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppPrimiani.Api.Data
{
    public class AppDbContext : IdentityDbContext
        <User,IdentityRole<long>, long,
        IdentityUserClaim<long>, IdentityUserRole<long>, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long> >
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // esse construtor é obrigatório para o EntityFramework
        // pois ele precisa de um DbContextOptions para funcionar
        //visto que nao tem nada escrito dentro dele, podemos omitir
        //e escreve-lo da seguinte forma:
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new TransactionMapping());
            //modelBuilder.ApplyConfiguration(new CategoryMapping());

            //Pega todos assemblies de mapeamento
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly() );
        }
    }
}
