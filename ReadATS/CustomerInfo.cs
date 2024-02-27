using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ReadATS
{
    internal class CustomerInfo : TableEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }
}