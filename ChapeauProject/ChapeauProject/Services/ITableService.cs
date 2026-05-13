using ChapeauProject.Models;

namespace ChapeauProject.Services
{
    public interface ITableService
    {
        List<Table> GetAll();
        Table? GetByTableNumber(int tableNumber);
        void ToggleOccupied(int tableNumber);
    }
}