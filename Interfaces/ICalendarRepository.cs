
using TFBackend.Entities.Dto.CalendarProjectStaff;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Models;

namespace TFBackend.Interfaces
{
    public interface ICalendarRepository

    {
        ICollection<CalendarProjectStaff> Gets();
       // CalendarProjectStaff Get(int id);

        bool Create(CalendarProjectStaffPostDto obj);
      //  bool Exist(int id);

      //  bool Update(ClientPutDto client, int id);
    //    bool Delete(Client client);

        bool Save();
    }
}
