using System.Collections.Generic;

namespace AplosApi.Contract
{
    public interface IAccountsService
    {
        AccountsResponse GetAccounts(AccountsFilter filter);
        AccountResponse GetAccount(int accountNumber);
    }

    public class AccountsResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public List<Account> Accounts { get; set; }
        }

        public class Account
        {
            public int AccountNumber { get; set; }
            public string Name { get; set; }
            public AccountCategory Category { get; set; }
            public AccountGroup AccountGroup { get; set; }
            public bool IsEnabled { get; set; }
            public string Type { get; set; }
            public string Activity { get; set; }
        }

        public class AccountGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Seq { get; set; }
        }
    }

    public class AccountsFilter : PagedFilter
    {
        /// <summary>
        /// Whether the account is enabled: y, n
        /// </summary>
        public bool? EnabledFilter { get; set; }

        /// <summary>
        /// Any part of the name, case insensitive
        /// </summary>
        public string NameFilter { get; set; }

        /// <summary>
        /// asset, liability, equity, income, expense
        /// </summary>
        public AccountCategory? CategoryFilter { get; set; }
    }

    public enum AccountCategory
    {
        Asset,
        Liability,
        Equity,
        Income,
        Expense
    }

    public class AccountResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public Account Account { get; set; }
        }

        public class Account
        {
            public int AccountNumber { get; set; }
            public string Name { get; set; }
            public AccountCategory Category { get; set; }
            public AccountGroup AccountGroup { get; set; }
            public bool IsEnabled { get; set; }
            public string Type { get; set; }
            public string Activity { get; set; }
        }

        public class AccountGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}