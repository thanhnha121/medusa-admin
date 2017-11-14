using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Medusa.Adapter;
using Medusa.Adapter.Dto;
using Medusa.Adapter.Interface;

namespace Medusa.Service
{
    [ServiceContract]
    public class SkillService
    {
        private readonly ISkillAdapter _skillAdapter;
        public SkillService()
        {
            _skillAdapter = new  SkillAdapter();
        }

        /// <summary>
        /// Add new Skill
        /// </summary>
        /// <param name="skillDto"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public SkillDto AddSkill(SkillDto skillDto)
        {
            return _skillAdapter.AddSkill(skillDto);
        }

        /// <summary>
        /// Get All Skills
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public List<SkillDto> GetAllSkill()
        {
            return _skillAdapter.GetAllSkill();
        }

        /// <summary>
        /// Get A Skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{SkillId}"
            )]
        public SkillDto GetSkill(string skillId)
        {
            return _skillAdapter.GetSkill(int.Parse(skillId));
        }

        /// <summary>
        /// Delete A Skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns>
        /// 'true' if Skill found or 'false' in other cases
        /// </returns>
        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/{SkillId}"
            )]
        public bool Delete(string skillId)
        {
            return _skillAdapter.DeleteSkill(int.Parse(skillId));
        }

        /// <summary>
        /// Update a Skill
        /// </summary>
        /// <param name="skillDto"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public SkillDto Update(SkillDto skillDto)
        {
            return _skillAdapter.UpdateSkill(skillDto);
        }

    }
}