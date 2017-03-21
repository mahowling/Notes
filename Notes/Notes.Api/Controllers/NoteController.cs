using Notes.Services;
using Notes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.Api.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : Controller
    {

        private readonly INoteRepository _noteRepository;
        private readonly ILogger _logger;

        public NoteController(INoteRepository repository, ILogger<NoteController> logger)
        {
            _noteRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            _logger.LogInformation("Retrieving all notes");
            return _noteRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult GetById(Guid id)
        {
            _logger.LogInformation("Retrieving note {0}", id);
            var item = _noteRepository.GetById(id);
            if (item == null)
            {
                _logger.LogError("Note (Id:{0}) does not exist.", id);
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Note item)
        {
            _logger.LogInformation("Creating new note");
            if (item == null)
            {
                _logger.LogError("No note passed.");
                return BadRequest();
            }
            if (item.Id!=null)
            {
                //check to see the object hasn't already been created
                if (_noteRepository.GetById(item.Id) != null)
                {
                    _logger.LogError("Note (Id:{0}) already exists.", item.Id);
                    return BadRequest();
                }
            }

            _noteRepository.Insert(item);

            _logger.LogInformation("Created new note {0}", item.Id);

            return CreatedAtRoute("GetNote", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody]Note item)
        {
            
            if (item==null||item.Id!=id)
            {
                _logger.LogError("Update with no specified note. (Id:{0})", id);
                return BadRequest();
            }
            _logger.LogInformation("Updating note {0}", id);
            var existingItem = _noteRepository.GetById(id);
            if (existingItem == null)
            {
                _logger.LogError("Note does not exists (Id:{0})", id);
                return NotFound();
            }

            existingItem.Content = item.Content;
            existingItem.Title = item.Title;

            _noteRepository.Update(existingItem);

            _logger.LogInformation("Updated note {0}", id);
            return new NoContentResult();            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existingItem = _noteRepository.GetById(id);
            if (existingItem == null)
            {
                _logger.LogError("Note (Id:{0}) does not exist.", id);
                return NotFound();
            }

            _noteRepository.Delete(existingItem);
            _logger.LogInformation("Note (Id:{0}) Deleted.", id);
            return new NoContentResult();
        }
    }
}