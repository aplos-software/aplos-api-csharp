using System;
using System.Collections.Generic;

namespace AplosApi.Contract
{
    public interface IContributionsService
    {
        ContributionsResponse GetContributions(ContributionsFilter filter);
        ContributionResponse GetContribution(int contributionId);
        //ContributionResponse PostContribution(ContributionInfo contribution);
        //ContributionResponse PutContribution(int contributionId, ContributionInfo contribution);
        ApiResponse DeleteContribution(int contributionId);
    }

    public class ContributionsResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public List<Contribution> Contributions { get; set; }
        }

        public class Contribution
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public DateTimeOffset Created { get; set; }
            public DateTimeOffset Modified { get; set; }
        }
    }

    public class ContributionsFilter : PagedFilter
    {
        /// <summary>
        /// Contact ID
        /// </summary>
        public int? ContactIdFilter { get; set; }

        /// <summary>
        /// Any part of any contact name(first, last, and/or company), case insensitive
        /// </summary>
        public string ContactNameFilter { get; set; }

        /// <summary>
        /// Last updated(created or modified)
        /// </summary>
        public DateTimeOffset? LastUpdatedFilter { get; set; }

        /// <summary>
        /// Minimum date range (inclusive). Format: yyyy-MM-dd
        /// </summary>
        public string RangeStartFilter { get; set; }

        /// <summary>
        /// Maximum date range (inclusive). Format: yyyy-MM-dd
        /// </summary>
        public string RangeEndFilter { get; set; }
    }

    public class ContributionResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public Contribution Contribution { get; set; }
        }

        public class Contribution
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Date { get; set; }
            public string SourceUrl { get; set; }

            public List<Line> Lines { get; set; }

            public DateTimeOffset Created { get; set; }
            public DateTimeOffset Modified { get; set; }
            public int DepositTransactionId { get; set; }

            public Account DepositAccount { get; set; }
            public Account ExpenseAccount { get; set; }
            public ExpenseContact ExpenseContact { get; set; }

            public decimal Amount { get; set; }
            public decimal ExpenseAmount { get; set; }

        }

        public class Line
        {
            public int Id { get; set; }
            public int ContributionId { get; set; }
            public Contact Contact { get; set; }
            public Purpose Purpose { get; set; }
            public string Note { get; set; }
            public decimal Amount { get; set; }
            public bool IsNtd { get; set; }
            public decimal ExpenseAmount { get; set; }
        }

        public class Contact
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Type { get; set; }
        }

        public class Purpose
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Account
        {
            public int AccountNumber { get; set; }
            public string Name { get; set; }
        }

        public class ExpenseContact
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string Type { get; set; }
        }
    }
}