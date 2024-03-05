using Domain.Abstractions;

namespace Domain.Entities
{
    public sealed class Customer : Entity
    {
        private Customer() : base(Guid.NewGuid())
        {
        }

        private Customer(
            Guid id,
            Name name,
            Dni dni,
            Address address,
            ContactInfo contactInfo)
        : base(id)
        {
            Name = name;
            Dni = dni;
            Address = address;
            ContactInfo = contactInfo;
        }

        public Name Name { get; private set; }
        public Dni Dni { get; private set; }
        public Address Address { get; private set; }
        public ContactInfo ContactInfo { get; private set; }

        // Factory method
        public static Customer Create(Guid id, Name name, Dni dni, Address address, ContactInfo contactInfo)
        {
            var customer = new Customer(id, name, dni, address, contactInfo);
            return customer;
        }

        // Update method
        public void Update(Name name, Dni dni, Address address, ContactInfo contactInfo)
        {            
            Name = name;
            Dni = dni;
            Address = address;
            ContactInfo = contactInfo;
        }
    }
}