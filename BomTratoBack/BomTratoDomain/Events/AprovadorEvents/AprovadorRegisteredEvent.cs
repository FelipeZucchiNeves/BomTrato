using NetDevPack.Messaging;
using System;

namespace BomTratoDomain.Events.AprovadorEvents
{
    public class AprovadorRegisteredEvent : Event
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public AprovadorRegisteredEvent(Guid id, string name,string lastName, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }
    }
}
