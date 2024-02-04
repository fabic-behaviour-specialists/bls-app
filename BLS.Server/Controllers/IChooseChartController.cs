using BLS.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BLS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IChooseChartController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<IChooseChartController> _logger;

        public IChooseChartController(ILogger<IChooseChartController> logger,
                               IDatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        //[HttpGet()]
        //[Route("id/{guid}")]
        //public Task<Book?> Get(string guid)
        //{
        //    return _bookService.GetBookById(guid);
        //}

        //[HttpPost()]
        //[Route("latest-books")]
        //public async Task<IEnumerable<Book>> GetLatestBooks(int pageSize = 15, int offset = 0, string? language = null)
        //{
        //    var books = await _bookService.GetBookList(language);
        //    return books.OrderByDescending(x => x.ReleaseDate).LimitToPage(pageSize, offset);
        //}
    }
}