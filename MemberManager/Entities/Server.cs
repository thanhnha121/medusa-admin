using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.Business.Entities
{
    public class Server
    {
        public int Id { get; set; }

        public string Ip { get; set; }

        public string SystemOS { get; set; }

        public string Descriptons { get; set; }

        public string Type { get; set; }

        public List<Project> Projects { get; set; }

        // return server status, still alive or not
        [NotMapped]
        public string Status { get; set; }

        public Server()
        {
            Projects = new List<Project>();
        }
    }
}
