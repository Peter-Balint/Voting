using Microsoft.AspNetCore.Mvc;
using Voting.DataAccess.Services;
using AutoMapper;
using Voting.Shared.DTOs;

namespace Voting.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Polls/")]
    public class PollsController : ControllerBase
    {
        private IMapper _mapper; 
        private PollsService _pollsService;
        private AnswersService _answersService;

        public PollsController(IMapper mapper, PollsService pollsService, AnswersService answersService)
        {
            _pollsService = pollsService;
            _mapper = mapper;
            _answersService = answersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivePolls()
        {
            var polls = await _pollsService.GetActivePolls();
            var pollDtos = _mapper.Map<List<PollDto>>(polls);

            return Ok(pollDtos);
        }

        [HttpGet]
        [Route("past")]
        public async Task<IActionResult> GetPastPolls()
        {
            var polls = await _pollsService.GetPastPolls();
            var pollDtos = _mapper.Map<List<PollDto>>(polls);

            return Ok(pollDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDetailedPollById(int id)
        {
            var poll = await _pollsService.GetDetailedPollById(id);
            var answers = await _answersService.GetAnswersByPollId(id);

            poll.AnswerOptions = answers.ToList();
            
            var pollDto = _mapper.Map<PollDto>(poll);

            return Ok(pollDto);
        }

        [HttpGet]
        [Route("answerstest")]
        public async Task<IActionResult> GetAllAnswers()
        {
            var answers = await _answersService.GetAllAnswers();

            var answersDto = _mapper.Map<List<AnswerDto>>(answers);

            return Ok(answersDto);
        }
    }
}
