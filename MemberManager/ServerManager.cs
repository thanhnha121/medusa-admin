using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Medusa.Business.Entities;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Medusa.Business
{

    public class ServerManager 
    {
        private readonly MedusaManagerDbContext _ctx;

        public ServerManager()
        {
            _ctx = new MedusaManagerDbContext();
        }

        /// <summary>
        /// Get Server by "Id"
        /// </summary>
        /// <param name="serverId">"Id" of Server</param>
        /// <returns></returns>
        public Server GetServer(string serverId)
        {
            int id = int.Parse(serverId);
            Server server = _ctx.Servers
                .Where(s => s.Id == id)
                .Include(s => s.Projects)
                .FirstOrDefault();
            if (server != null)
            {
                Ping ping = new Ping();
                PingReply pr = ping.Send(server.Ip);
                if (pr != null)
                {
                    server.Status = pr.Status == IPStatus.Success ? "Ping OK!" : "No Response!";
                }
                return server;
            }
            return null;
        }

        /// <summary>
        /// Get All Server from DB
        /// </summary>
        /// <returns>List of Server</returns>
        public List<Server> GetAllServers()
        {
            List<Server> servers = new List<Server>(_ctx.Set<Server>().Include(s => s.Projects));
            for (int i = 0; i < servers.Count; i++)
            {
                if (!string.IsNullOrEmpty(servers[i].Ip))
                {
                    Ping ping = new Ping();
                    PingReply pr = ping.Send(servers[i].Ip);
                    if (pr != null)
                    {
                        servers[i].Status = pr.Status == IPStatus.Success ? "Ping OK!" : "No Response!";
                    }
                }
            }
            return servers;
        }

        /// <summary>
        /// Update a exists Server
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public Server Update(Server server)
        {
            Server returnServer = _ctx.Servers
                .Where(s => s.Id == server.Id)
                .Include(s => s.Projects)
                .FirstOrDefault();
            if (returnServer != null)
            {
                returnServer.Ip = server.Ip;
                returnServer.SystemOS = server.SystemOS;
                returnServer.Descriptons = server.Descriptons;
                returnServer.Type = server.Type;

                returnServer.Projects = new List<Project>();
                if (server.Projects.Count > 0)
                {
                    for (int i = 0; i < server.Projects.Count; i++)
                    {
                        Project p = _ctx.Projects.Find(server.Projects[i].Id);
                        returnServer.Projects.Add(p);
                    }
                }
                _ctx.SaveChanges();
                return returnServer;
            }
            return null;
        }

        /// <summary>
        /// Add new server
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public Server Add(Server server)
        {
            Server returnServer = new Server() {
                Ip = server.Ip,
                Descriptons = server.Descriptons,
                SystemOS = server.SystemOS,
                Type = server.Type
            };
            if (server.Projects != null || server.Projects.Count != 0)
            {
                foreach (var project in server.Projects)
                {
                    returnServer.Projects.Add(_ctx.Projects.Find(project.Id));
                }
            }
            returnServer = _ctx.Servers.Add(returnServer);
            _ctx.SaveChanges();
            return returnServer;
        }

        /// <summary>
        /// Delete server by "Id"
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns>Return "true" if Server is Exists and success Deleted,
        /// return "false" if Server not found</returns>
        public bool Delete(string serverId)
        {
            int id = int.Parse(serverId);
            Server server = _ctx.Servers
                .Where(s => s.Id == id)
                .Include(s => s.Projects)
                .FirstOrDefault();
            if (server != null)
            {
                server.Projects.Clear();
                _ctx.Servers.Remove(server);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add a project into exists Server 
        /// </summary>
        /// <param name="serverId">Id of server</param>
        /// <param name="projectId">ID of Project that want to add</param>
        /// <returns></returns>
        public Server AddProject(string serverId, int projectId)
        {
            int id = int.Parse(serverId);
            Server server = _ctx.Servers.Where(s => s.Id == id)
                .Include(s => s.Projects)
                .FirstOrDefault();
            if (server != null)
            {
                server.Projects.Add(_ctx.Projects.Find(projectId));
                _ctx.SaveChanges();
                return server;
            }
            return null;
        }

        /// <summary>
        /// Get All Projects of a server by ServerId
        /// </summary>
        /// <param name="serverId">Id of server</param>
        /// <returns>List of Projects</returns>
        public List<Project> GetAllProjects(string serverId)
        {
            int id = int.Parse(serverId);
            Server server = _ctx.Servers.Where(s => s.Id == id)
                .Include(s => s.Projects)
                .FirstOrDefault();
            List<Project> projects = server.Projects;
            return projects;
        }
    }
}
