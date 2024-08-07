using BW2_Team6.Context;
using BW2_Team6.Models;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
    public class DrawerService : IDrawerService
    {
        private readonly DataContext _db;

        public DrawerService(DataContext db)
        {
            _db = db;
        }
        public Task<Drawer> Create(Drawer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Drawer> Delete(int id)
        {
            var drawer = await Read(id);
            _db.Drawers.Remove(drawer);
            await _db.SaveChangesAsync();
            return drawer;
        }

        public async Task<IEnumerable<Drawer>> GetAll()
        {
            var drawers = await _db.Drawers
                .Include(d => d.Locker)
                .Include(d => d.Product)
                .ToListAsync();
            return drawers;
        }

        public async Task<Drawer> Read(int id)
        {
            var drawer = await _db.Drawers
                .Include(d => d.Locker)
                .Include(d => d.Product)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (drawer == null) {
                throw new Exception("Drawer not found");
            }
            return drawer;
        }

        public async Task<Drawer> Update(int id, Drawer entity)
        {
            var drawer = await Read(id);
            drawer.Locker = entity.Locker;
            drawer.Product = entity.Product;
            drawer.Locker = entity.Locker;
            _db.Drawers.Add(drawer);
            await _db.SaveChangesAsync();
            return drawer;
        }
    }
}
