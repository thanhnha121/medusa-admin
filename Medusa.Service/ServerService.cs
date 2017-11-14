using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Medusa.Adapter;
using Medusa.Adapter.Dto;
using Medusa.Adapter.Interface;

namespace Medusa.Service
{
    [ServiceContract]
    public class ServerService
    {
        private readonly IServerAdapter _serverAdapter;

        /// <summary>
        /// Constructor
        /// </summary>
        public ServerService()
        {
            _serverAdapter = new ServerAdapter();
        }

        /// <summary>
        /// Get a server by ServerId
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{serverId}"
            )]
        public ServerDto GetServer(string serverId)
        {
            return _serverAdapter.GetServer(serverId);
        }

        /// <summary>
        /// Get all Servers
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public List<ServerDto> GetAllServers()
        {
            return _serverAdapter.GetAllServers();
        }

        /// <summary>
        /// Edit a Project
        /// </summary>
        /// <param name="serverDto">
        /// A JSON object with at least ServerId
        /// </param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public ServerDto Update(ServerDto serverDto)
        {
            return _serverAdapter.Update(serverDto);
        }

        /// <summary>
        /// Add new Project
        /// </summary>
        /// <param name="serverDto">
        /// </param>
        /// <returns>New Project</returns>
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public ServerDto Add(ServerDto serverDto)
        {
            return _serverAdapter.Add(serverDto);
        }

        /// <summary>
        /// Delete A Server
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns>
        /// 'true' if server found and 'false' in other cases
        /// </returns>
        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{serverId}"
            )]
        public bool Delete(string serverId)
        {
            return _serverAdapter.Delete(serverId);
        }

        /// <summary>
        /// Assign a Project to a Server
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{serverId}/Projects/{projectId}"
            )]
        public ServerDto AddProject(string serverId, string projectId)
        {
            return _serverAdapter.AddProject(serverId, int.Parse(projectId));
        }

        /// <summary>
        /// Get All Project of a Server
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{ServerId}/Projects"
            )]
        public List<ProjectDto> GetAllProjects(string serverId)
        {
            return _serverAdapter.GetAllProjects(serverId);
        }
    }
}
