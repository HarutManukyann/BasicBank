using BL.Repositories.Interfaceis;
using BL.Repository;
using FCBankBasicHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class RoleRepository : Repository<UserRolesMapping>,IRoleRepository
    {
        public RoleRepository(FcbankBasicContext context) : base(context)
        {

        }
    }
}
