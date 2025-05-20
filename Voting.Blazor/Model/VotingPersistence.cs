using System.Net.Http.Headers;
using Voting.Shared.DTOs;

namespace Voting.Blazor.Model
{
    public class VotingPersistence : IVotingPersistence
    {
        private HttpClient _client;

        /// <summary>
        /// Szolgáltatás alapú perszisztencia példányosítása.
        /// </summary>
        /// <param name="baseAddress">A szolgáltatás címe.</param>
        public VotingPersistence(string baseAddress)
        {
            _client = new HttpClient(); // a szolgáltatás kliense
            _client.BaseAddress = new Uri(baseAddress); // megadjuk neki a címet
        }
        public VotingPersistence()
        {
            _client = new HttpClient(); // a szolgáltatás kliense
        }
        public VotingPersistence(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PollDto>> ReadActivePollsAsync()
        {
            try
            {
                // a kéréseket a kliensen keresztül végezzük
                HttpResponseMessage response = await _client.GetAsync("api/Polls/");
                if (response.IsSuccessStatusCode) // amennyiben sikeres a művelet
                {
                    IEnumerable<PollDto> Polls = await response.Content.ReadAsAsync<IEnumerable<PollDto>>();
                    // a tartalmat JSON formátumból objektumokká alakítjuk

                    return Polls;
                }
                else
                {
                    throw new PersistenceUnavailableException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }
        public async Task<PollDto> ReadDetailedPollByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/Polls/{id}");
                if (response.IsSuccessStatusCode)
                {
                    PollDto Poll = await response.Content.ReadAsAsync<PollDto>();

                    return Poll;
                }
                else
                {
                    throw new PersistenceUnavailableException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="email">Email cím.</param>
        /// <param name="password">Jelszó.</param>
        public async Task<Boolean> LoginAsync(string email, string password)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/users/login", new LoginDto
                {
                    Email = email,
                    Password = password
                });
                return response.IsSuccessStatusCode; // a művelet eredménye megadja a bejelentkezés sikerességét
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }


        /// <summary>
        /// Bejelentkezés hozzáférési tokenért.
        /// </summary>
        /// <param name="email">Email cím.</param>
        /// <param name="password">Jelszó.</param>
        public async Task<Boolean> LoginTokenAsync(string email, string password)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/users/login-token", new LoginDto
                {
                    Email = email,
                    Password = password
                });

                if (response.IsSuccessStatusCode &&
                    response.Headers.TryGetValues("X-Access-Token", out var values))
                {
                    string? token = values.FirstOrDefault();
                    if (token != null)
                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                return response.IsSuccessStatusCode; // a művelet eredménye megadja a bejelentkezés sikerességét
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        public async Task<Boolean> RegisterAsync(UserDto userDto)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/users/", userDto);

                /*if (response.IsSuccessStatusCode &&
                    response.Headers.TryGetValues("X-Access-Token", out var values))
                {
                    string? token = values.FirstOrDefault();
                    if (token != null)
                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }*/

                return response.IsSuccessStatusCode; // a művelet eredménye megadja a bejelentkezés sikerességét
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }
    }
}
