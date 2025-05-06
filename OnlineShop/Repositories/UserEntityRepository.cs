using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Features;
using OnlineShop.Helpers;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public class UserEntityRepository(OnlineShopDBContext db) : IUserEntityRepository
    {
        private readonly DbSet<UserEntity> set = db.Set<UserEntity>();
        public async Task AddAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            await set.AddAsync(userEntity, cancellationToken);
        }

        public void Delete(UserEntity userEntity)
        {
            set.Remove(userEntity);
        }

        public async Task<List<UserEntity>> GetAllAsync(BaseSpecification<UserEntity> specification, CancellationToken cancellationToken)
        {
            var query = set.AsNoTracking().Specify(specification);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<UserEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await set.AsNoTracking().FirstOrDefaultAsync(x => x.UserEntityId == id, cancellationToken);

        }

        public void Update(UserEntity userEntity)
        {
            set.Update(userEntity);
        }
    }
}
