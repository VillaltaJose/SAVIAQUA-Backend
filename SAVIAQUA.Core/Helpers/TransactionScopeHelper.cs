using System.Transactions;

namespace SAVIAQUA.Core.Helpers;

public static class TransactionScopeHelper
{
    public static TransactionScope StartTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        => new(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = isolationLevel },
            TransactionScopeAsyncFlowOption.Enabled);

    public static TransactionScope IgnoreTransactions(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        => new (
            TransactionScopeOption.Suppress,
            new TransactionOptions { IsolationLevel = isolationLevel },
            TransactionScopeAsyncFlowOption.Enabled);
}