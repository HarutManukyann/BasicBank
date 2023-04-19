using BL.Repositories;
using FCBankBasicHelper.Models;
using Models.BaseType;
using Models.DTO;
using Models.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Core
{
    public class RoleBl
    {
        private readonly RoleRepository roleRepository;
        public RoleBl(RoleRepository repository)
        {
            roleRepository = repository;
        }
        public ResponseBase<UserRolesMappingModel> AddRoleForUser(UserRolesMappingModel model)
        {
            try
            {
                UserRolesMapping mapping = Mapper<UserRolesMappingModel, UserRolesMapping>.Map(model);                
                roleRepository.Add(mapping);
                return new ResponseBase<UserRolesMappingModel>(true, "Role added successfully", model);
            }
            catch (Exception ex)
            {
                return new ResponseBase<UserRolesMappingModel>(false, ex.Message);

            }

        }
    }
}
