using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BurguerManiaAPI.Interfaces.Category;
using BurguerManiaAPI.Dto.Category;

namespace BurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categorys : ControllerBase
    {
        private readonly CategoryInterface _categoryService;

        /// <summary>
        /// Inicializa o controlador com o serviço de categorias.
        /// </summary>
        /// <param name="categoryService">Interface de serviços relacionados a categorias.</param>
        public Categorys(CategoryInterface categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retorna a lista de categorias.
        /// </summary>
        [HttpGet("GetCategorys")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var result = await _categoryService.GetCategories();

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        /// <summary>
        /// Retorna os dados de uma categoria específica pelo ID.
        /// </summary>
        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            var result = await _categoryService.GetCategory(id);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        /// <summary>
        /// Atualiza os dados de uma categoria.
        /// </summary>
        [HttpPut("PutCategorys/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] CategoryRequest categoryRequest)
        {
            var result = await _categoryService.PutCategories(id, categoryRequest);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        /// <summary>
        /// Adiciona uma nova categoria.
        /// </summary>
        [HttpPost("PostCategorys")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryRequest categoryRequest)
        {
            var result = await _categoryService.PostCategories(categoryRequest);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        /// <summary>
        /// Remove uma categoria pelo ID.
        /// </summary>
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var result = await _categoryService.DeleteCategories(id);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }
    }
}
