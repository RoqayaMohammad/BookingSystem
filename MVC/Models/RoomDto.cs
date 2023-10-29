using DAL.Models;

namespace MVC.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public RoomType Type { get; set; }

        public string BranchName { get; set; }
    }
}
