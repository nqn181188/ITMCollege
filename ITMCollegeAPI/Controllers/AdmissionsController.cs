using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITMCollegeAPI.Models;
using System.IO;

namespace ITMCollegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionsController : ControllerBase
    {
        private readonly ITMCollegeContext _context;

        public AdmissionsController(ITMCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Admissions
        [HttpGet]
        public async Task<ActionResult<List<Admissions>>> GetAdmissions()
        {
            var listAdmission = await _context.Admissions.ToListAsync();
            List<Admissions> list = new List<Admissions>();
            list = GetFullInfoAdmissions(listAdmission);
            return list;
        }

        // GET: api/Admissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admission>> GetAdmission(long id)
        {
            var admission = await _context.Admissions.FindAsync(id);

            if (admission == null)
            {
                return NotFound();
            }

            return admission;
        }

        // PUT: api/Admissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmission(long id, byte status)
        {

            try
            {
                var admission = await _context.Admissions.FindAsync(id);
                if (admission == null)
                {
                    return NotFound();
                }
                admission.Status = status;
                _context.Entry(admission).State = EntityState.Modified;
                _context.Admissions.Update(admission);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Admissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admission>> PostAdmission(Admission admission)
        {
            _context.Admissions.Add(admission);
            await _context.SaveChangesAsync();
            return Ok(admission);
        }
        // DELETE: api/Admissions/5
        private bool AdmissionExists(long id)
        {
            return _context.Admissions.Any(e => e.AdmissionId == id);
        }
        [HttpGet("CheckRegNumber/{regnum}")]
        public bool CheckRegNumberExits(string regnum)
        {
            var admission = _context.Admissions.Where(a => a.RegNum.Equals(regnum)).FirstOrDefault();
            if (admission == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //Select List<Admissions> from List<Admisson>
        private List<Admissions> GetFullInfoAdmissions(List<Admission> listAdmission)
        {
            List<Admissions> list = new List<Admissions>();
            
            foreach (var item in listAdmission)
            {
                Admissions ad = new Admissions();
                GetFullAdmission(ad, item);
                list.Add(ad);
            }
            return list;
        }
        [HttpGet("GetAdmissionByRegNum/{regnum}")]
        public async Task<ActionResult<Admissions>> GetAdmissionByRegNum(string regnum)
        {
            var admission = await _context.Admissions.FirstOrDefaultAsync(a => a.RegNum.Equals(regnum));

            if (admission == null)
            {
                return Ok(null);
            }
            Admissions ad = new Admissions();
            return Ok(GetFullAdmission(ad,admission));
        }
        //Select Admissions form Admission
        private Admissions GetFullAdmission(Admissions ad, Admission item)
        {
            StreamsController streamsController = new StreamsController(_context);
            FieldsController fieldsController = new FieldsController(_context);
            ad.AdmissionId = item.AdmissionId;
            ad.RegNum = item.RegNum;
            ad.FullName = item.FullName;
            ad.FatherName = item.FatherName;
            ad.MotherName = item.MotherName;
            ad.DateOfBirth = item.DateOfBirth;
            ad.Gender = item.Gender;
            ad.Email = item.Email;
            ad.ResAddress = item.ResAddress;
            ad.PerAddress = item.PerAddress;
            ad.StreamId = item.StreamId;
            ad.FieldId = item.FieldId;
            ad.Sport = item.Sport;
            ad.ExUniversity = item.ExUniversity;
            ad.ExCenter = item.ExCenter;
            ad.ExStream = item.ExStream;
            ad.ExField = item.ExField;
            ad.ExClass = item.ExClass;
            ad.ExEnrollNum = item.ExEnrollNum;
            ad.ExOutOfDate = item.ExOutOfDate;
            ad.ExMarks = item.ExMarks;
            ad.Stream = streamsController.GetStreamByStreamId(ad.StreamId);
            ad.Field = fieldsController.GetFieldByFieldId(ad.FieldId);
            ad.Status = item.Status;
            return ad;
        }
        [HttpHead("GetAdmissionStatus/{regnum}")]
        public async Task<ActionResult<byte>> GetAdmissionStatus(string regNum)
        {
            var admission = await _context.Admissions.FirstOrDefaultAsync(a=>a.RegNum.Equals(regNum));
            if (admission == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(admission.Status);
            }
        }
    }
}
