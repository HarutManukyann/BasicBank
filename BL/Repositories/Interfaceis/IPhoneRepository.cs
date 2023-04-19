using FCBankBasicHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories.Interfaceis
{
    public interface IPhoneRepository :IRepository<Phone>
    {
        Phone GetPhone(int user_id);
    }
}
