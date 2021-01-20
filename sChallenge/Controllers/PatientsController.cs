using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sChallenge.Models;

namespace sChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApiContext _context;

        public PatientsController(ApiContext context)
        {
            _context = context;
        }

        /*
         * Converts a Patient object to a Patient Data Transfer Object containing a 
         * subset of fields.
         */
        private static PatientDTO PatientToDTO(Patient patient) => new PatientDTO
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Street = patient.Street,
            City = patient.City,
            Province = patient.Province,
            Country = patient.Country,
            HasCovid = patient.HasCovid,
            Phone = patient.Phone,
            Email = patient.Email,
            Health_Notes = patient.Health_Notes,
            Call_Date = patient.Call_Date,
            AgentId = patient.AgentId
        };

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatient()
        {
            return await _context.Patient.Select(patient => PatientToDTO(patient)).ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(long id)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return PatientToDTO(patient);
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(long id, Patient patientDTO)
        {
            if (id != patientDTO.Id)
            {
                return BadRequest();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            patient.FirstName = patientDTO.FirstName;
            patient.LastName = patientDTO.LastName;
            patient.BirthDate = patientDTO.BirthDate;
            patient.Street = patientDTO.Street;
            patient.City = patientDTO.City;
            patient.Province = patientDTO.Province;
            patient.Country = patientDTO.Country;
            patient.HasCovid = patientDTO.HasCovid;
            patient.Phone = patientDTO.Phone;
            patient.Email = patientDTO.Email;
            patient.Health_Notes = patientDTO.Health_Notes;
            patient.Call_Date = patientDTO.Call_Date;
            patient.AgentId = patientDTO.AgentId;

            _context.Entry(patientDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(PatientDTO patientDTO)
        {
            var patient = new Patient
            {
                FirstName = patientDTO.FirstName,
                LastName = patientDTO.LastName,
                BirthDate = patientDTO.BirthDate,
                Street = patientDTO.Street,
                City = patientDTO.City,
                Province = patientDTO.Province,
                Country = patientDTO.Country,
                HasCovid = patientDTO.HasCovid,
                Phone = patientDTO.Phone,
                Email = patientDTO.Email,
                Health_Notes = patientDTO.Health_Notes,
                Call_Date = patientDTO.Call_Date,
                AgentId = patientDTO.AgentId
            };

            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, PatientToDTO(patient));
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(long id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private bool PatientExists(long id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
