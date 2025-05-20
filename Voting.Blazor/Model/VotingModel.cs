using Voting.Shared.DTOs;

namespace Voting.Blazor.Model
{
    public class VotingModel : IVotingModel
    {
        private IVotingPersistence _persistence;
        private List<PollDto> _polls = new();

        public VotingModel(IVotingPersistence votingPersistence)
        {
            _persistence = votingPersistence;
            IsUserLoggedIn = false;
        }

        public Boolean IsUserLoggedIn { get; private set; }
        public List<PollDto> Polls { get { return _polls; } }
        public PollDto ActiveDetailedPoll { get; private set; }

        public async Task ReadActivePollsAsync()
        {
            _polls = (await _persistence.ReadActivePollsAsync()).ToList();
        }
        public async Task ReadPastPollsAsync()
        {
            _polls = (await _persistence.ReadPastPollsAsync()).ToList();
        }
        public async Task ReadActiveDetailedPollAsync(int id) 
        { 
            ActiveDetailedPoll = await _persistence.ReadDetailedPollByIdAsync(id);
        }

        public async Task<Boolean> LoginAsync(string userName, string userPassword, bool useCookies = true)
        {
            IsUserLoggedIn = useCookies
                ? await _persistence.LoginAsync(userName, userPassword)
                : await _persistence.LoginTokenAsync(userName, userPassword);
            return IsUserLoggedIn;
        }
        public async Task<Boolean> RegisterAsync(UserDto userDto)
        {
            return await _persistence.RegisterAsync(userDto);
        }
    }
}
