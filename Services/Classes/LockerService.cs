using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
    public class LockerService : ILockerService
    {
        private readonly DataContext _db;

        public LockerService(DataContext db)
        {
            _db = db;
        }
        public async Task<Locker> Create(Locker entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Locker> Delete(int id)
        {
            var locker = await Read(id);
            _db.Locker.Remove(locker);
            await _db.SaveChangesAsync();
            return locker;
        }

        public async Task<IEnumerable<Locker>> GetAll()
        {
               var lockers = await _db.Locker
                .ToListAsync();
            foreach(var locker in lockers) {
                locker.Drawer = await _db.Drawers.Include(x => x.Locker)
                    .Where(x => x.Locker.Id == locker.Id)
                    .ToListAsync();
            }
            return lockers;
        }

        public async Task<Locker> Read(int id)
        {
            var locker = await _db.Locker.FirstOrDefaultAsync(locker => locker.Id == id);
            if (locker == null) 
            {
                throw new Exception("Locker not found");
            }
            return locker;
        }

        public async Task<Locker> Update(int id, Locker entity)
        {
            var locker = await Read(id);
            locker.NumberLocker = entity.NumberLocker;
            _db.Locker.Update(locker);
            await _db.SaveChangesAsync();
            return locker;
        }

    }
}
