using Notes.Services;
using Notes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System;
using System.Collections.Generic;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.Api.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : Controller
    {

        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository repository)
        {
            _noteRepository = repository;
        }

        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            return _noteRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult GetById(Guid id)
        {
            var item = _noteRepository.GetById(id);
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Note item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (item.Id!=null)
            {
                //check to see the object hasn't already been created
                if (_noteRepository.GetById(item.Id) != null)
                    return BadRequest();
            }

            _noteRepository.Insert(item);

            return CreatedAtRoute("GetNote", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody]Note item)
        {
            if (item==null||item.Id!=id)
            {
                return BadRequest();
            }

            var existingItem = _noteRepository.GetById(id);
            if (existingItem == null)
                return NotFound();

            existingItem.Content = item.Content;
            existingItem.Title = item.Title;

            _noteRepository.Update(existingItem);
            return new NoContentResult();            
        }
    }
}