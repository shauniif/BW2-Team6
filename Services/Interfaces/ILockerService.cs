using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
    public interface ILockerService
    {
        Task<IEnumerable<Locker>> GetAll();

        Task<Locker> Create(Locker entity);
        Task<Locker> Update(int id, Locker entity);
       
        Task<Locker> Read(int id);
        Task<Locker> Delete(int id);
    }
}
