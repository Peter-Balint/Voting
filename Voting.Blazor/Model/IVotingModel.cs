using Voting.Shared.DTOs;

namespace Voting.Blazor.Model
{
    public interface IVotingModel
    {
        Boolean IsUserLoggedIn { get; }
        List<PollDto> Polls { get; }
        PollDto ActiveDetailedPoll { get; }

        Task ReadActivePollsAsync();
        Task ReadPastPollsAsync();
        Task ReadActiveDetailedPollAsync(int id);

        Task<Boolean> LoginAsync(string userName, string userPassword, bool useCookies = true);
        Task<Boolean> RegisterAsync(UserDto userDto);
    }
}
