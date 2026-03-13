using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Group(string name)
    {
        [Key]
        public string Name { get; set; }= name;

        // NAp property for the connections in the group
        public ICollection<Connection> Connections { get; set; } = [];
    }
}
