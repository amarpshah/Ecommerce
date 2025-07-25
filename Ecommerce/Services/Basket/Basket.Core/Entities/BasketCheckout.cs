namespace Basket.Core.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        public decimal TotalAmount { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public string AddressLine { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Cvv { get; set; }
    }
}
