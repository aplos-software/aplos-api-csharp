using System;
using System.Collections.Generic;

namespace AplosApi.Contract
{
    public interface IPurposesService
    {
        PurposesResponse GetPurposes(PurposesFilter filter);
        PurposeResponse GetPurpose(int purposeId);
        PurposeResponse PostPurpose(PurposeInfo purpose);
        PurposeResponse PutPurpose(int purposeId, PurposeInfo purpose);
        ApiResponse DeletePurpose(int purposeId);
    }

    public class PurposesResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public List<PurposeModel> Purposes { get; set; }
        }
    }

    public class PurposesFilter : PagedFilter
    {
        public bool? EnabledFilter { get; set; }
        public string NameFilter { get; set; }
    }

    public class PurposeResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public PurposeModel Purpose { get; set; }
        }
    }

    public class PurposeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public int Seq { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
        public IncomeAccountModel IncomeAccount { get; set; }
        public FundModel Fund { get; set; }

        public class IncomeAccountModel
        {
            public int AccountNumber { get; set; }
            public string Name { get; set; }
        }

        public class FundModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

    public class PurposeInfo
    {
        public PurposeInfo()
        {
            IncomeAccount = new IncomeAccountModel();
            Fund = new FundModel();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public IncomeAccountModel IncomeAccount { get; set; }
        public FundModel Fund { get; set; }

        public class IncomeAccountModel
        {
            public int AccountNumber { get; set; }
        }

        public class FundModel
        {
            public int Id { get; set; }
        }
    }
}