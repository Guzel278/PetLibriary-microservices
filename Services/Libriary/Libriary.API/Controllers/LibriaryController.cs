using Microsoft.AspNetCore.Mvc;
using Libriary.API.Repositories;
using System;
using Microsoft.Extensions.Logging;
using Libriary.API.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Libriary.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LibriaryController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<LibriaryController> _logger;

        public LibriaryController(IBookRepository repository, ILogger<LibriaryController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var Book = await _repository.GetBook();
            return Ok(Book);
        }

        [HttpGet("{id:length(25)}", Name = "GetBook")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var Book = await _repository.GetBook(id);
            if (Book == null)
            {
                _logger.LogError($"Book with id {id}, not found");
                return NotFound();
            }
            return Ok(Book);
        }

        [Route("[action]/{category}", Name = "GetBookByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByCategory(string category)
        {
            var Books = await _repository.GetBookByCategory(category);
            return Ok(Books);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book Book)
        {
            await _repository.CreateBook(Book);

            return CreatedAtRoute("GetBook", new { id = Book.Id }, Book);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBook([FromBody] Book Book)
        {
            return Ok(await _repository.UpdateBook(Book));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteBook")]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBookById(string id)
        {
            return Ok(await _repository.DeleteBook(id));
        }
    }
}
