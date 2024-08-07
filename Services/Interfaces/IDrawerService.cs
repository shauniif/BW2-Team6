using BW2_Team6.Models;

namespace BW2_Team6.Services.Classes
{
    public interface IDrawerService
    {
        Task<IEnumerable<Drawer>> GetAll();
        
        Task<Drawer> Read(int id);
        Task<Drawer> Create(Drawer entity);

        Task<Drawer> Update(int id, Drawer entity);

        Task<Drawer> Delete(int id);


    }
}
