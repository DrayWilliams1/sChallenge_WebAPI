﻿using System;

namespace sChallenge.Models
{
    public class Patient
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public bool HasCovid { get; set; } // Hidden in the patient DTO
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Health_Notes { get; set; }
        public DateTime Call_Date { get; set; }
        public long AgentId { get; set; }
    }
}
