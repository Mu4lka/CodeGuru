using Api.Contracts.Tests.Dto;
using Api.Contracts.Tests.Requests;
using Api.Persistence;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class TestsController(ApplicationDbContext _context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTestRequest request)
    {
        var questions = request.Questions
            .Select(question => new Question()
            {
                Text = question.Text,
                DifficultyLevel = question.DifficultyLevel,
                NumberOfPoints = question.NumberOfPoints,
                Answers = question.Answers
                    .Select(answer => new Answer()
                    {
                        Text = answer.Text,
                        IsCorreсt = answer.IsCorreсt
                    }).ToList(),
            }).ToList();

        var test = new Test()
        {
            Name = request.Name,
            Description = request.Description,
            TravelTime = request.TravelTime,
            Questions = questions,
        };
        await _context.AddAsync(test);
        await _context.SaveChangesAsync();

        return Ok(test.Id);
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var test = _context.Tests
            .Where(t => t.Id == id)
            .Include(test => test.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefault();

        if (test == null) return BadRequest("Test not found");

        var testDto = new GetTestDto()
        {
            Id = test.Id,
            Name = test.Name,
            Description = test.Description,
            TravelTime = test.TravelTime,
            Questions = test.Questions
                .Select(q => new GetQuestionDto()
                {
                    Id = q.Id,
                    Text = q.Text,
                    NumberOfPoints = q.NumberOfPoints,
                    DifficultyLevel = q.DifficultyLevel,
                    Answers = q.Answers.Select(a => new GetAnswerDto()
                    {
                        Id = a.Id, Text = a.Text,
                    }).ToList(),
                }).ToList(),
        };
        return Ok(testDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var test =  await _context.Tests.Where(t => t.Id == id).FirstOrDefaultAsync();

        if (test == null) return BadRequest("Test not found for to delete");

        _context.Tests.Remove(test);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("NamesAndIds")]
    public async Task<IActionResult> GetTestNamesAndIdsAsync()
        => Ok(await _context.Tests
            .Select(t => new GetTestNameAndIdDto()
            {
                Id = t.Id, Name = t.Name
            }).ToListAsync());
}
