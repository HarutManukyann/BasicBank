using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BL.Core
{
    public class TransactionBL
    {
        public static TransactionScope CreateTransactionScope(TransactionScopeOption scopeOption = TransactionScopeOption.Required)
        {
            return new TransactionScope(scopeOption, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
        }
        public static TransactionScope CreateTransactionScope(IsolationLevel isolationLevel, TransactionScopeOption scopeOption = TransactionScopeOption.RequiresNew)
        {
            return new TransactionScope(scopeOption, new TransactionOptions { IsolationLevel = isolationLevel });
        }
    }
}
