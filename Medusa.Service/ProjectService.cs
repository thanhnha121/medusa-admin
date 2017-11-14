using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Medusa.Adapter;
using Medusa.Adapter.Dto;
using Medusa.Adapter.Interface;

namespace Medusa.Service
{
    [ServiceContract]
    public class ProjectService
    {
        private readonly IProjectAdapter _projectAdapter;

        public ProjectService()
        {
            _projectAdapter = new ProjectAdapter();
        }

        /// <summary>
        /// Add new Project
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public ProjectDto Add(ProjectDto projectDto)
        {
            return _projectAdapter.Add(projectDto);
        }

        /// <summary>
        /// Update a exists Project
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public ProjectDto Update(ProjectDto projectDto)
        {
            return _projectAdapter.Update(projectDto);
        }

        /// <summary>
        /// Get a Project by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{projectId}"
            )]
        public ProjectDto GetProject(string projectId)
        {
            return _projectAdapter.GetProject(int.Parse(projectId));
        }

        /// <summary>
        /// Delete a Project by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>
        /// 'true' if Project found and 'false' if not
        /// </returns>
        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{projectId}"
            )]
        public bool Delete(string projectId)
        {
            return _projectAdapter.DeleteProject(int.Parse(projectId));
        }

        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public List<ProjectDto> GetAllProjects()
        {
            return _projectAdapter.GetAllProjects();
        }

        /// <summary>
        /// Assign a skill to Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="skillId"></param>
        /// <returns>Updated Project</returns>
        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{projectId}/Skills/{skillId}"
            )]
        public ProjectDto AddSkill(string projectId, string skillId)
        {
            return _projectAdapter.AddSkill(int.Parse(projectId), int.Parse(skillId));
        }

        /// <summary>
        /// Assign Member to a Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="memberId"></param>
        /// <returns>Updated Project</returns>
        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{projectId}/Members/{memberId}"
            )]
        public ProjectDto AddMember(string projectId, string memberId)
        {
            return _projectAdapter.AddMember(int.Parse(projectId), int.Parse(memberId));
        }

        /// <summary>
        /// Assign Skill to Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>Updated Project</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{projectId}/Skills"
            )]
        public List<SkillDto> GetAllSkills(string projectId)
        {
            return _projectAdapter.GetAllSkills(int.Parse(projectId));
        }

        /// <summary>
        /// Get all members of a Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>
        /// List members if project found/exists
        /// </returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{projectId}/Members"
            )]
        public List<MemberDto> GetAllMembers(string projectId)
        {
            return _projectAdapter.GetAllMembers(int.Parse(projectId));
        }

    }
}
