using System;
using System.IO;
using System.Linq;
using AplosApi.Contract;
using AplosApi.Services;
using FluentAssertions;
using Xunit;

namespace AplosApi.Tests
{
    public class ServiceExampleTests
    {
        private readonly IApiGateway _gateway;

        public ServiceExampleTests()
        {
            // NOTE: put your Aplos client *.key file in the root folder of the github repository

            var filePath = Directory.GetCurrentDirectory();
            filePath = Path.Combine(filePath.Substring(0, filePath.IndexOf("aplos-api-csharp")), "aplos-api-csharp");
            var keyFile = Directory.GetFiles(filePath, "*.key").First();

            var gatewayFactory = new ApiGatewayFactory(cfg =>
            {
                cfg.LoadPrivateKeyFromFile(keyFile);
            });

            _gateway = gatewayFactory.BuildGateway();
        }

        [Fact]
        public void test_fetching_contacts()
        {
            var service = new ContactsService(_gateway);

            var filter = new ContactsFilter
            {
                PageSize = 1,
                PageNumber = 1,
                //LastUpdatedFilter = new DateTimeOffset(new DateTime(2018, 1, 1), TimeSpan.FromMinutes(120)),
                TypeFilter = ContactType.Company,
                EmailFilter = "aplos",
                NameFilter = "software"
            };

            var contacts = service.GetContacts(filter);

            contacts.Data.Contacts.Count.Should().BeGreaterThan(0);

            var contact = service.GetContact(contacts.Data.Contacts[0].Id);
        }

        [Fact]
        public void test_fetching_accounts()
        {
            var service = new AccountsService(_gateway);

            var filter = new AccountsFilter
            {
                PageSize = 50,
                PageNumber = 1,
                EnabledFilter = true,
                //NameFilter = ""
            };

            var accounts = service.GetAccounts(filter);

            filter.CategoryFilter = AccountCategory.Asset;
            var assetAccounts = service.GetAccounts(filter);
            assetAccounts.Data.Accounts.Count.Should().BeGreaterThan(0);
            assetAccounts.Data.Accounts.All(x => x.Category == AccountCategory.Asset).Should().BeTrue();
            filter.CategoryFilter = null;

            if (accounts.Links.Next != null)
            {
                filter.PageNumber += 1;
                var nextPage = service.GetAccounts(filter);
            }

            accounts.Data.Accounts.Count.Should().BeGreaterThan(0);

            var account = service.GetAccount(accounts.Data.Accounts[0].AccountNumber);
        }

        [Fact]
        public void test_fetching_funds()
        {
            var service = new FundsService(_gateway);

            var filter = new FundsFilter
            {
                PageSize = 10,
                PageNumber = 1,
                //AccountNumberFilter = 1,
                //NameFilter = ""
            };

            var funds = service.GetFunds(filter);

        }

        [Fact]
        public void test_fetching_purposes()
        {
            var service = new PurposesService(_gateway);

            var filter = new PurposesFilter
            {
                PageSize = 10,
                PageNumber = 1,
                EnabledFilter = true,
                //NameFilter = ""
            };

            var purposes = service.GetPurposes(filter);

            var newPurpose = new PurposeInfo
            {
                Name = "My new purpose",
                Description = "My new purpose created via the Aplos API",
                IsEnabled = true,
            };

            newPurpose.IncomeAccount.AccountNumber = 4000;
            newPurpose.Fund.Id = 173676;

            var postResult = service.PostPurpose(newPurpose);

            postResult.Data.Purpose.Name.Should().Be(newPurpose.Name);

            newPurpose.Name = "My renamed purpose";

            var putResult = service.PutPurpose(postResult.Data.Purpose.Id, newPurpose);

            putResult.Data.Purpose.Name.Should().Be(newPurpose.Name);

            var updatedPurpose = service.GetPurpose(putResult.Data.Purpose.Id);

            updatedPurpose.Data.Purpose.Name.Should().Be(newPurpose.Name);

            var deleteResult = service.DeletePurpose(postResult.Data.Purpose.Id);

            deleteResult.Message.Should().Contain(postResult.Data.Purpose.Id.ToString());

            var deletedPurpose = service.GetPurpose(putResult.Data.Purpose.Id);

            deletedPurpose.Data.Purpose.Should().BeNull();
            deletedPurpose.Meta.ResourceCount.Should().Be(0);
        }

        [Fact]
        public void test_fetching_contributions()
        {
            var service = new ContributionsService(_gateway);

            var contributions = service.GetContributions(new ContributionsFilter());
        }
    }
}