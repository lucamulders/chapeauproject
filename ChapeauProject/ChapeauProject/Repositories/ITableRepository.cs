using ChapeauProject.Models;

namespace ChapeauProject.Repositories
{
    public interface ITableRepository
    {
        List<Table> GetAll();
        Table? GetByTableNumber(int tableNumber);
        void ToggleOccupied(int tableNumber);
    }
}