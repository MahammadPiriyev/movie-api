using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Domain.Entities;
using MovieAPI.Infrastructure.IRepository;

namespace MovieAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	// [Authorize]
	public class CategoryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var movieFromDb = await _unitOfWork.Categories.GetAsync(c => c.CategoryId == id);
			return Ok(movieFromDb);
		}

		[HttpGet("SALAMMM")]
		public async Task<IActionResult> GetAll()
		{
			var categoryList = await _unitOfWork.Categories.GetAllAsync();
			return Ok(categoryList);
		}

		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] Category category)
		{
			_unitOfWork.Categories.Add(category);
			_unitOfWork.Save();
			return Ok(new { message = "Movie has successfully created!" });
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> Update([FromBody] Category category, int id)
		{
			var categoryFromDb = await _unitOfWork.Categories.GetAsync(c => c.CategoryId == id);
			categoryFromDb.Name = category.Name;

			_unitOfWork.Categories.Update(categoryFromDb);
			_unitOfWork.Save();
			return Ok(new { message = $"Category {id} has successfully updated!", category });
		}

		[HttpDelete("remove/{id}")]
		public async Task<IActionResult> Remove(int id)
		{
			var categoryFromDb = await _unitOfWork.Categories.GetAsync(m => m.CategoryId == id);
			_unitOfWork.Categories.Remove(categoryFromDb);
			_unitOfWork.Save();
			return Ok(new { message = $"Category {id} has successfully removed!" });

		}
	}
}
