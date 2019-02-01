using System;
using System.Collections.Generic;

namespace AplosApi.Contract
{
    public interface IContactsService
    {
        ContactsResponse GetContacts(ContactsFilter filter);
        ContactResponse GetContact(int contactId);
    }

    public class ContactsResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public List<Contact> Contacts { get; set; }
        }

        public class Contact
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string CompanyName { get; set; }
            public string Type { get; set; }
            public string Email { get; set; }
            public DateTimeOffset Created { get; set; }
            public DateTimeOffset Modified { get; set; }
        }
    }

    public class ContactsFilter : PagedFilter
    {
        /// <summary>
        /// Any part of any email, case insensitive
        /// </summary>
        public string EmailFilter { get; set; }

        /// <summary>
        /// Any part of any name(first, last and/or company), case insensitive
        /// </summary>
        public string NameFilter { get; set; }

        /// <summary>
        /// Last updated(created or modified)
        /// </summary>
        public DateTimeOffset? LastUpdatedFilter { get; set; }

        /// <summary>
        /// individual or company
        /// </summary>
        public ContactType? TypeFilter { get; set; }
    }

    public enum ContactType
    {
        Individual,
        Company
    }

    public class ContactResponse : ApiResponse
    {
        public DataModel Data { get; set; }

        public class DataModel
        {
            public Contact Contact { get; set; }
        }

        public class Contact
        {
            public Contact()
            {
                Emails = new List<Email>();
                Phones = new List<Phone>();
                Addresses = new List<Address>();
            }

            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string CompanyName { get; set; }
            public string Type { get; set; }
            public string Email { get; set; }
            public DateTimeOffset Created { get; set; }
            public DateTimeOffset Modified { get; set; }
            public List<Email> Emails { get; set; }
            public List<Phone> Phones { get; set; }
            public List<Address> Addresses { get; set; }
        }

        public class Email
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public bool IsPrimary { get; set; }
        }

        public class Phone
        {
            public string Name { get; set; }
            public string Telnum { get; set; }
            public bool IsPrimary { get; set; }
        }

        public class Address
        {
            public string Name { get; set; }
            public bool IsPrimary { get; set; }
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
        }
    }
}