using AcmeGroup.API.Data;
using AcmeGroup.API.Models.Domain;
using AcmeGroup.API.Models.DTO;
using AcmeGroup.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcmeGroup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCQsController : ControllerBase
    {
        private readonly AcmeDbContext dbContext;
        private readonly IMCQRepository mcqRepository;

        public MCQsController(AcmeDbContext dbContext, IMCQRepository mcqRepository)
        {
            this.dbContext = dbContext;
            this.mcqRepository = mcqRepository;
        }


        // method related to Get all mcqs
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domain Models
            var mcqsDomainModel = await mcqRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var mcqsDto = new List<MCQDto>();

            foreach (var mcq in mcqsDomainModel)
            {
                mcqsDto.Add(new MCQDto()
                {
                    Id = mcq.Id,
                    McqNumber = mcq.McqNumber,
                    Question = mcq.Question,
                    Option1 = mcq.Option1,
                    Option2 = mcq.Option2,
                    Option3 = mcq.Option3,
                    Option4 = mcq.Option4,
                    RightAnswer = mcq.RightAnswer,
                });
            }

            return Ok(mcqsDto);
        }

        // method related to Get mcq by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // get data in domain model from database
            var mcqDomainModel = await mcqRepository.GetByIdAsync(id);

            if (mcqDomainModel == null)
            {
                return NotFound();
            }

            // map domain model to dto

            var mcqDto = new MCQDto
            {
                Id = mcqDomainModel.Id,
                McqNumber = mcqDomainModel.McqNumber,
                Question = mcqDomainModel.Question,
                Option1 = mcqDomainModel.Option1,
                Option2 = mcqDomainModel.Option2,
                Option3 = mcqDomainModel.Option3,
                Option4 = mcqDomainModel.Option4,
                RightAnswer = mcqDomainModel.RightAnswer,
            };

            return Ok(mcqDto);
        }

        // method related to Create mcq
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddMCQRequestDto addMCQRequestDto)
        {
            if(ModelState.IsValid)
            {
                // covert dto to domain model
                var mcqDomainModel = new MCQ
                {
                    McqNumber = addMCQRequestDto.McqNumber,
                    Question = addMCQRequestDto.Question,
                    Option1 = addMCQRequestDto.Option1,
                    Option2 = addMCQRequestDto.Option2,
                    Option3 = addMCQRequestDto.Option3,
                    Option4 = addMCQRequestDto.Option4,
                    RightAnswer = addMCQRequestDto.RightAnswer,
                };

                // use domain model to create mcq
                mcqDomainModel = await mcqRepository.CreateAsync(mcqDomainModel);

                // map domain model to dto
                var mcqDto = new MCQDto
                {
                    Id = mcqDomainModel.Id,
                    McqNumber = mcqDomainModel.McqNumber,
                    Question = mcqDomainModel.Question,
                    Option1 = mcqDomainModel.Option1,
                    Option2 = mcqDomainModel.Option2,
                    Option3 = mcqDomainModel.Option3,
                    Option4 = mcqDomainModel.Option4,
                    RightAnswer = mcqDomainModel.RightAnswer,
                };

                return CreatedAtAction(nameof(GetById), new { id = mcqDomainModel.Id }, mcqDto);
            }else
            {
                return BadRequest(ModelState);
            }
        }
            

        // method related to Update mcq
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMCQRequestDto updateMCQRequestDto)
        {
            if (ModelState.IsValid)
            {
                // convert dto to domain model
                var mcqDomainModel = new MCQ
                {
                    McqNumber = updateMCQRequestDto.McqNumber,
                    Question = updateMCQRequestDto.Question,
                    Option1 = updateMCQRequestDto.Option1,
                    Option2 = updateMCQRequestDto.Option2,
                    Option3 = updateMCQRequestDto.Option3,
                    Option4 = updateMCQRequestDto.Option4,
                    RightAnswer = updateMCQRequestDto.RightAnswer,
                };

                mcqDomainModel = await mcqRepository.UpdateAsync(id, mcqDomainModel);

                if (mcqDomainModel == null)
                {
                    return NotFound();
                }


                // convert domain model to dto
                var mcqDto = new MCQDto
                {
                    Id = mcqDomainModel.Id,
                    McqNumber = mcqDomainModel.McqNumber,
                    Question = mcqDomainModel.Question,
                    Option1 = mcqDomainModel.Option1,
                    Option2 = mcqDomainModel.Option2,
                    Option3 = mcqDomainModel.Option3,
                    Option4 = mcqDomainModel.Option4,
                    RightAnswer = mcqDomainModel.RightAnswer,
                };

                return Ok(mcqDto);
            }else
            {
                return BadRequest(ModelState);
            }
        }
            

        // method related to Delete mcq
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var mcqDomainModel = await mcqRepository.DeleteAsync(id);

            if(mcqDomainModel == null)
            {
                return NotFound();
            }

            return Ok("MCQ Deleted");
        }
    }
}
