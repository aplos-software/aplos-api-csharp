using System.Collections.Generic;

namespace AplosApi.Contract
{
    public interface IFundsService
    {
        FundsResponse GetFunds(FundsFilter filter);
        FundResponse GetFund(int fundId);
    }

    public class FundsResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public List<FundModel> Funds { get; set; }
        }
    }

    public class FundsFilter : PagedFilter
    {
        public int? AccountNumberFilter { get; set; }

        public string NameFilter { get; set; }
    }

    public class FundResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public FundModel Fund { get; set; }
        }
    }

    public class FundModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BalanceAccountName { get; set; }
        public int BalanceAccountNumber { get; set; }
    }
}