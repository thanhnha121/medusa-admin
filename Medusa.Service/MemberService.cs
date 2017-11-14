using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Medusa.Adapter;
using Medusa.Adapter.Dto;
using Medusa.Adapter.Interface;

namespace Medusa.Service
{
    [ServiceContract]
    public class MemberService
    {
        private readonly IMemberAdapter _memberAdapter;

        public MemberService()
        {
            _memberAdapter = new MemberAdapter();
        }
        /// <summary>
        /// Operation Contract Serivce for add a member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = ""
             )]
        public MemberDto AddMember(MemberDto member)
        {
            return _memberAdapter.AddMember(member);
        }
        /// <summary>
        /// Operation Contract Serivce for get a member by id
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{MemberId}"
            )]
        public MemberDto GetMember(string memberId)
        {
            return _memberAdapter.GetMember(int.Parse(memberId));
        }
        /// <summary>
        /// Operation Contract Serivce for get all members
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
           Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = ""
           )]
        public List<MemberDto> GetAllMembers()
        {
            return _memberAdapter.GetAllMembers();
        }
        /// <summary>
        /// Operation Contract Serivce for update a member
        /// </summary>
        /// <param name="memberDto"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
          Method = "PUT",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          UriTemplate = ""
          )]
        public MemberDto Update(MemberDto memberDto)
        {
            return _memberAdapter.UpdateMember(memberDto);
        }

        /// <summary>
        /// Operation Contract Serivce for delete a member
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/{memberId}"
            )]
        public bool Delete(string memberId)
        {
            return _memberAdapter.DeleteMember(int.Parse(memberId));
            
        }
        /// <summary>
        /// Operation Contract Serivce for add project 
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/{memberId}/Projects/{projectId}"
           )]
        public MemberDto AddProject(string memberId, string projectId)
        {
            return _memberAdapter.AddProject(memberId, int.Parse(projectId));
        }
        /// <summary>
        /// Operation Contract Serivce for get all project
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{memberId}/Projects"
            )]
        public List<ProjectDto> GetAllProjects(string memberId)
        {
            return _memberAdapter.GetAllProjects(memberId);
        }
    }
}
