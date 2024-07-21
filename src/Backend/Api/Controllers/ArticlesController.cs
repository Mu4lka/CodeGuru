﻿using Api.Contracts;
using Api.Contracts.Articles;
using Api.Core.Extensions;
using Api.Persistence;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateArticle(CreateArticleRequest request)
        {
            if (request.CheckIfNull() is false)
            {
                return BadRequest("Одна или несколько полей пустое");
            }
            var creator = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var findCreator = _context.Users.FirstOrDefault(c => c.Name == creator);
            if (findCreator != null)
            {
                _context.Articles.Add(new Articles
                {
                    Title = request.Title,
                    Text = request.Text,
                    Creator = findCreator.Name
                });
                _context.SaveChanges();
                return Ok("Статья создана");
            }

            return BadRequest("Ошибка");

        }


        [HttpPost("getarticle")]
        public async Task<IActionResult> GetArticleById(Guid guid)
        {
            return Ok(await _context.Articles.FirstAsync(u => u.Id == guid));
        }
        [HttpPost("getpaginated")]
        public async Task<IActionResult> GetArticlesPaginated(PageRequest page)
        {
            var articles = await _context.Articles
                .OrderBy(date => date.CreatedAt)
                .Skip((page.Number - 1) * page.Size)
                .Take(page.Size)
                .Select(article => new GetArticlesRequest
                {
                    Title = article.Title,
                    Text = article.Text,
                })
                .ToListAsync();

            var count = await _context.Articles.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)page.Size);
            ListPaginations<GetArticlesRequest> result = new ListPaginations<GetArticlesRequest>
            (articles, count,totalPages);

            return Ok(result);
        }
        [HttpPost("getall")]
        public async Task<IActionResult> GetAllArticleTemp()
        {
            return Ok(_context.Articles.ToList());
        }
    }
}
