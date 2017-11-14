using System.Collections.Generic;
using Medusa.Adapter.Interface;
using Medusa.Business;
using Medusa.Business.Entities;
using ProjectDto = Medusa.Adapter.Dto.ProjectDto;
using ServerDto = Medusa.Adapter.Dto.ServerDto;

namespace Medusa.Adapter
{
    public class ServerAdapter : IServerAdapter
    {
        private readonly ServerManager _serverManager;
        private readonly ProjectAdapter _projectAdapter;

        public ServerAdapter()
        {
            _projectAdapter = new ProjectAdapter();
            _serverManager = new ServerManager();
        }

        public Server ConvertDtoToEntity(ServerDto serverDto)
        {
            Server server = new Server()
            {
                Id = serverDto.Id,
                Status = serverDto.Status,
                Ip = serverDto.Ip,
                Type = serverDto.Type,
                Descriptons = serverDto.Descriptons,
                SystemOS = serverDto.SystemOS
            };
            foreach (var projectDto in serverDto.Projects)
            {
                server.Projects.Add(new Project() {
                    Id = projectDto.Id,
                    Name = projectDto.Name
                });
            }
            return server;
        }

        public ServerDto ConvertEntityToDto(Server server)
        {
            ProjectAdapter projectAdapter = new ProjectAdapter();
            ServerDto serverDto = new ServerDto
            {
                Id = server.Id,
                Status = server.Status,
                Ip = server.Ip,
                Type = server.Type,
                Descriptons = server.Descriptons,
                SystemOS = server.SystemOS
            };
            foreach (var project in server.Projects)
            {
                serverDto.Projects.Add(new ProjectDto()
                {
                    Id = project.Id,
                    Name = project.Name
                });
            }
            return serverDto;
        }

        /// <summary>
        /// Get Server by "Id"
        /// </summary>
        /// <param name="serverId">"Id" of Server</param>
        /// <returns></returns>
        public ServerDto GetServer(string serverId)
        {
            return ConvertEntityToDto(_serverManager.GetServer(serverId));
        }

        /// <summary>
        /// Get All Server from DB
        /// </summary>
        /// <returns>List of Server</returns>
        public List<ServerDto> GetAllServers()
        {
            List<ServerDto> serverDtos = new List<ServerDto>();
            List<Server> servers = _serverManager.GetAllServers();
            foreach (var server in servers)
            {
                serverDtos.Add(ConvertEntityToDto(server));
            }
            return serverDtos;
        }

        /// <summary>
        /// Update a exists Server
        /// </summary>
        /// <param name="serverDto"></param>
        /// <returns></returns>
        public ServerDto Update(ServerDto serverDto)
        {
            return ConvertEntityToDto(_serverManager.Update(ConvertDtoToEntity(serverDto)));
        }

        /// <summary>
        /// Add new server
        /// </summary>
        /// <param name="serverDto"></param>
        /// <returns></returns>
        public ServerDto Add(ServerDto serverDto)
        {
            return ConvertEntityToDto(_serverManager.Add(ConvertDtoToEntity(serverDto)));
        }

        /// <summary>
        /// Delete server by "Id"
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns>Return "true" if Server is Exists and success Deleted,
        /// return "false" if Server not found</returns>
        public bool Delete(string serverId)
        {
            return _serverManager.Delete(serverId);
        }

        /// <summary>
        /// Add a project into exists Server 
        /// </summary>
        /// <param name="serverId">Id of server</param>
        /// <param name="projectId">ID of Project that want to add</param>
        /// <returns></returns>
        public ServerDto AddProject(string serverId, int projectId)
        {
            return ConvertEntityToDto(_serverManager.AddProject(serverId, projectId));
        }

        /// <summary>
        /// Get All Projects of a server by ServerId
        /// </summary>
        /// <param name="serverId">Id of server</param>
        /// <returns>List of Projects</returns>
        public List<ProjectDto> GetAllProjects(string serverId)
        {
            List<ProjectDto> projectDtos = new List<ProjectDto>();
            List<Project> projects = new ProjectManager().GetAllProjects();
            foreach (var project in projects)
            {
                projectDtos.Add(_projectAdapter.ConvertEntityToDto(project));
            }
            return projectDtos;
        }

    }
}
