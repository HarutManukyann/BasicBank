using FCBankBasicHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Core
{
    public class ConfigBl
    {
        public string GetBykey(ConfigType config)
        {
            using(var context = new FcbankBasicContext())
            {
                int key = (int)(config);
                var result = context.Configs.FirstOrDefault(item => item.Key == key);
                if (result is not null) return result.Value;
                return null;
            }
            
        }
    }
}
