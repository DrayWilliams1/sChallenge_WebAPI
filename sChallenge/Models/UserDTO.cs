using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sChallenge.Models
{
    /*
     * Data Transfer Object (DTO) class used to provide a subset of data
     * from the User model
     */
    public class UserDTO
    {
        public long Id { get; set; }
        public string Username { get; set; }
    }
}
