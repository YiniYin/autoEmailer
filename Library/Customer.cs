using System;

namespace Konex.AutoEmailer.Library
{
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime NextServiceDate { get; set; }
        public bool IsEmailSent { get; set; }
    }
}
