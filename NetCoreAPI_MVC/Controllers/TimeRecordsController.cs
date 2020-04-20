using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreInterface;
using NetCoreModels;
using NetCoreModels.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPI.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class TimeRecordsController : ControllerBase
    {
        private readonly ITimeRecordService _timeRecordService;

        public TimeRecordsController(ITimeRecordService timeRecordService)
        {
            _timeRecordService = timeRecordService;
        }

        // GET: api/TimeRecords
        [HttpGet]
        public async Task<IEnumerable<TimeRecordViewModel>> GetTimeRecord()
        {
            return await _timeRecordService.GetAllWithUser();
        }
        // GET: api/TimeRecords/5
        [HttpGet("[action]/{id}")]
 
        public async Task<IEnumerable<TimeRecord>> EmployeeTimeRecord(int id)
        {
            return  await _timeRecordService.Entity.Where(w=> w.UserID == id).ToListAsync();             
        }

        // GET: api/TimeRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeRecord>> GetTimeRecord(int id)
        {
            var timeRecord = await _timeRecordService.Get(id);

            if (timeRecord == null)
            {
                return NotFound();
            }

            return timeRecord;
        }

        // PUT: api/TimeRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeRecord(int id, TimeRecord timeRecord)
        {
            if (id != timeRecord.TimeRecordID)
            {
                return BadRequest();
            }

            if (await _timeRecordService.Update(timeRecord))
            {
                return NoContent();
            }
            return NotFound();

        }

        // POST: api/TimeRecords
        [HttpPost]
        public async Task<ActionResult<TimeRecord>> PostTimeRecord(TimeRecord timeRecord)
        {
            await _timeRecordService.Create(timeRecord);

            return CreatedAtAction("GetTimeRecord", new { id = timeRecord.TimeRecordID }, timeRecord);
        }

        // DELETE: api/TimeRecords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TimeRecord>> DeleteTimeRecord(int id)
        {
            var timeRecord = await _timeRecordService.Get(id);
            if (timeRecord == null)
            {
                return NotFound();
            }

            await _timeRecordService.Delete(timeRecord);

            return timeRecord;
        }

        private async Task<bool> TimeRecordExists(int id)
        {
            return await _timeRecordService.Exists(e => e.TimeRecordID == id);
        }
    }
}
