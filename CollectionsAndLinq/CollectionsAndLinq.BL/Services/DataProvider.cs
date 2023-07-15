using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace CollectionsAndLinq.BL.Services
{
    public class DataProvider : IDataProvider
    {
        private HttpClient _client;
        public DataProvider()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://bsa-dotnet.azurewebsites.net/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //api/Projects
        public async Task<List<Project>> GetProjectsAsync()
        {
            List<Project> projects = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.WEB_API_PROJECTS);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    projects = await response.Content.ReadFromJsonAsync<List<Project>>();
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tError:{ex.Message}");
            }

            return projects;
        }
        //api/Tasks
        public async Task<List<Entities.Task>> GetTasksAsync()
        {
            List<Entities.Task> tasks = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.WEB_API_TASKS);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    tasks = await response.Content.ReadFromJsonAsync<List<Entities.Task>>();
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tError:{ex.Message}");
            }

            return tasks;
        }
        //api/Teams
        public async Task<List<Team>> GetTeamsAsync()
        {
            List<Team> teams = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.WEB_API_TEAMS);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    teams = await response.Content.ReadFromJsonAsync<List<Team>>();
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tError:{ex.Message}");
            }

            return teams;
        }
        //api/Users
        public async Task<List<User>> GetUsersAsync()
        {
            List<User> users = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.WEB_API_USERS);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    users = await response.Content.ReadFromJsonAsync<List<User>>();
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tError:{ex.Message}");
            }

            return users;
        }
    }
}
