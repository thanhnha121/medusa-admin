using System.Collections.Generic;
using ProjectDto = Medusa.Adapter.Dto.ProjectDto;
using ServerDto = Medusa.Adapter.Dto.ServerDto;

namespace Medusa.Adapter.Interface
{
    public interface IServerAdapter
    {
        ServerDto GetServer(string serverId);
        List<ServerDto> GetAllServers();
        ServerDto Update(ServerDto serverDto);
        ServerDto Add(ServerDto serverDto);
        bool Delete(string serverId);
        ServerDto AddProject(string serverId, int projectId);
        List<ProjectDto> GetAllProjects(string serverId);
    }
}
