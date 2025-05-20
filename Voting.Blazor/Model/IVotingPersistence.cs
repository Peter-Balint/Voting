using Voting.Shared.DTOs;

namespace Voting.Blazor.Model
{
    public interface IVotingPersistence
    {
        Task<IEnumerable<PollDto>> ReadActivePollsAsync();
        Task<PollDto> ReadDetailedPollByIdAsync(int id);

        Task<Boolean> LoginAsync(string email, string password);
        Task<Boolean> LoginTokenAsync(string email, string password);
        Task<Boolean> RegisterAsync(UserDto userDto);
    }
}
