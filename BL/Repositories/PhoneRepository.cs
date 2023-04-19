using BL.Repositories.Interfaceis;
using BL.Repository;
using FCBankBasicHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
		public PhoneRepository(FcbankBasicContext context) : base(context)
		{

		}
        public Phone GetPhone(int user_id)
        {
			try
			{
				var phone = context.Phones.AsNoTracking().FirstOrDefault(x => x.UserId == user_id);
				if (phone != null) { return phone; }

			}
			catch (Exception ex)
			{
				throw new Exception(ex.StackTrace);
			}
			return null;
        }
    }
}
