using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;

namespace CollectionsAndLinq.UI
{
    public class UserInterface
    {
        private readonly IDataProcessingService _dataProcessingService;
        private readonly Dictionary<int, Action> methodDictionary;
        private string? key;

        public UserInterface(IDataProcessingService dataProcessingService)
        {
            _dataProcessingService = dataProcessingService;
            methodDictionary = GetInitializedMenuItems();
        }

        private Dictionary<int, Action> GetInitializedMenuItems()
        {
            return new Dictionary<int, Action>()
            {
                {1, GetTasksCountInProjectsByUserId},
                {2, GetCapitalTasksByUserId},
                {3, GetProjectsByTeamSize},
                {4, GetSortedTeamByMembersWithYear},
                {5, GetSortedUsersWithSortedTasks},
                {6, GetUserInfo},
                {7, GetProjectsInfo},
            };
        }

        public void GetTasksCountInProjectsByUserId()
        {
            ClearConsole();
            DisplayInfo();

            Console.WriteLine("\tEnter user ID:");
            string? id = Console.ReadLine();

            if (Validation.IsValidNumber(id)) 
            {
                var result = _dataProcessingService.GetTasksCountInProjectsByUserIdAsync(int.Parse(id)).GetAwaiter().GetResult();
                if (result.Count != 0)
                {
                    result.ToList().ForEach(i => Console.WriteLine($"\t{i.Key} {i.Value}"));
                }
                else 
                {
                    Console.WriteLine($"\tUser ID {id} has no tasks");
                }
            }
            else
            {
                Console.WriteLine("\tEnter incorrect data");
            }
        }

        public void GetCapitalTasksByUserId()
        {
            ClearConsole();
            DisplayInfo();

            Console.WriteLine("\tEnter user ID:");
            string? id = Console.ReadLine();

            if (Validation.IsValidNumber(id))
            {
                var result = _dataProcessingService.GetCapitalTasksByUserIdAsync(int.Parse(id)).GetAwaiter().GetResult();
                if (result.Count != 0)
                {
                    result.ToList().ForEach(i => Console.WriteLine($"\t{nameof(i.Id)} : {i.Id}" +
                        $"\n\t{nameof(i.Name)} : {i.Name}" +
                        $"\n\t{nameof(i.State)} : {i.State}" +
                        $"\n\t{nameof(i.CreatedAt)} : {i.CreatedAt}" +
                        $"\n\t{nameof(i.Description)} : {i.Description}"));
                }
                else
                {
                    Console.WriteLine($"\tUser ID {id} has no tasks");
                }
            }
            else
            {
                Console.WriteLine("\tEnter incorrect data");
            }
        }

        public void GetProjectsByTeamSize()
        {
            ClearConsole();
            DisplayInfo();

            Console.WriteLine("\tEnter the number of participants:");
            string? quantity = Console.ReadLine();

            if (Validation.IsValidNumber(quantity))
            {
                var result = _dataProcessingService.GetProjectsByTeamSizeAsync(int.Parse(quantity)).GetAwaiter().GetResult();
                if (result.Count != 0)
                {
                    result.ToList().ForEach(i => Console.WriteLine($"\t{nameof(i.Id)} - {i.Id}, {nameof(i.Name)} - {i.Name}"));
                }
                else
                {
                    Console.WriteLine($"\tThere are no teams with more than {quantity} people");
                }
            }
            else 
            {
                Console.WriteLine("\tEnter incorrect data");
            }
        }

        public void GetSortedTeamByMembersWithYear()
        {
            ClearConsole();
            DisplayInfo();

            Console.WriteLine("\tEnter year of birth:");
            string? year = Console.ReadLine();
            if (Validation.IsValidDateOfBirth(year))
            {
                var result = _dataProcessingService.GetSortedTeamByMembersWithYearAsync(int.Parse(year)).GetAwaiter().GetResult();
                if (result.Count != 0)
                {
                    result.ToList().ForEach(i=> PrintTeamWithMembers(i));

                }
                else 
                {
                    Console.WriteLine($"\tThere are no members in the team who were born before {year}");
                }
            }
            else 
            {
                Console.WriteLine("\tEnter incorrect data");
            }
        }

        public void PrintTeamWithMembers(TeamWithMembersDto team)
        {
            Console.WriteLine($"\t{nameof(team.Id)} : {team.Id}\n\t{nameof(team.Name)} : {team.Name}\n\tMembers:\n\t");
            team.Members.ForEach(member =>
            {
                Console.WriteLine($"\t{nameof(member.Id)} : {member.Id}\n\t" +
                    $"{nameof(member.FirstName)} : {member.FirstName}\n\t" +
                    $"{nameof(member.LastName)} : {member.LastName}\n\t" +
                    $"{nameof(member.BirthDay)} : {member.BirthDay}");
            });
        }

        public void GetSortedUsersWithSortedTasks()
        {
            ClearConsole();
            DisplayInfo();
            var result = _dataProcessingService.GetSortedUsersWithSortedTasksAsync().GetAwaiter().GetResult();
            if (result.Count != 0)
            {
                result.ToList().ForEach(i => PrintUserWithTask(i));
            }
            else 
            {
                Console.WriteLine($"\tNon-participants who meet the conditions");
            }
        }

        public void PrintUserWithTask(UserWithTasksDto userWithTasksDto)
        {
            Console.WriteLine($"\t{nameof(userWithTasksDto.Id)} : {userWithTasksDto.Id}\n\t" +
                $"{nameof(userWithTasksDto.FirstName)} : {userWithTasksDto.FirstName}\n\t" +
                $"{nameof(userWithTasksDto.LastName)} : {userWithTasksDto.LastName}\n\t" +
                $"{nameof(userWithTasksDto.BirthDay)} : {userWithTasksDto.BirthDay}\n\t" +
                $"{nameof(userWithTasksDto.Email)} : {userWithTasksDto.Email}\n\t" +
                $"{nameof(userWithTasksDto.RegisteredAt)} : {userWithTasksDto.RegisteredAt}\n\t" +
                $"Tasks:\n\t");
            userWithTasksDto.Tasks.ForEach(tasks =>
            {
                Console.WriteLine($"\t{nameof(tasks.Id)} : {tasks.Id}\n\t" +
                    $"{nameof(tasks.State)} : {tasks.State}\n\t" +
                    $"{nameof(tasks.Name)} : {tasks.Name}"+
                    $"{nameof(tasks.Description)} : {tasks.Description}\n\t" +
                    $"{nameof(tasks.CreatedAt)} : {tasks.CreatedAt}\n\t" +
                    $"{nameof(tasks.FinishedAt)} : {tasks.FinishedAt}\n\t");
            });
        }

        public void GetUserInfo()
        {
            ClearConsole();
            DisplayInfo();

            Console.WriteLine("\tEnter user ID:");
            string? id = Console.ReadLine();

            if (Validation.IsValidNumber(id))
            {
                var result = _dataProcessingService.GetUserInfoAsync(int.Parse(id)).GetAwaiter().GetResult();
                if (result != null)
                {
                    Console.WriteLine($"\t{nameof(result.User.Id)} : {result.User.Id}\n\t" +
                        $"{nameof(result.User.FirstName)} : {result.User.FirstName}\n\t" +
                        $"{nameof(result.User.LastName)} : {result.User.LastName}\n\t" +
                        $"{nameof(result.User.Email)} : {result.User.Email}\n\t" +
                        $"{nameof(result.LastProject)} : {result.LastProject.Name}\n\t" +
                        $"{nameof(result.LastProjectTasksCount)} : {result.LastProjectTasksCount}\n\t" +
                        $"{nameof(result.LongestTask)} : {result.LongestTask.Name}\n\t" + 
                        $"{nameof(result.NotFinishedOrCanceledTasksCount)} : {result.NotFinishedOrCanceledTasksCount}");
                }
                else
                {
                    Console.WriteLine($"\tThis user does not exist");
                }
            }
            else
            {
                Console.WriteLine("\tEnter incorrect data");
            }
        }

        public void GetProjectsInfo()
        {
            var result = _dataProcessingService.GetProjectsInfoAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                result.ToList().ForEach(p => Console.WriteLine(
                    $"\t{nameof(p.Project.Name)} : {p.Project.Name}\n\t" +
                    $"{nameof(p.ShortestTaskByName)} : {p?.ShortestTaskByName?.Name}\n\t" +
                    $"{nameof(p.ShortestTaskByName)} : {p?.ShortestTaskByName?.Name}\n\t" +
                    $"{nameof(p.TeamMembersCount)} : {p?.TeamMembersCount}\n\t"));
            }
            else 
            {
                Console.WriteLine($"\tNo such project exists");
            }
        }

        private void DisplayInfo()
        {
            ClearConsole();
            ChangedColor(ConsoleColor.Red);

            Console.WriteLine("\n\t\t\t\tCollections And Linq");
            ChangedColor(ConsoleColor.Yellow);

            Console.WriteLine("\n\tMENU");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n\t" +
                "1 - Get Tasks Count In Projects By UserId \n\t" +
                "2 - Get Capital Tasks By UserId\n\t" +
                "3 - Get Projects By Team Size\n\t" +
                "4 - Get Sorted Team By Members With Year\n\t" +
                "5 - Get Sorted Users With Sorted Tasks\n\t" +
                "6 - Get User Info\n\t" +
                "7 - Get Projects Info\n\t");

            ChangedColor(ConsoleColor.Yellow);
            Console.WriteLine("\n\tSelect the desired item:\n");
            ChangedColor(ConsoleColor.White);
        }

        private void ChangedColor(ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
        }

        private void ClearConsole()
        {
            Console.Clear();
        }

        public void RunApplication()
        {
            DisplayInfo();

            do
            {
                key = Console.ReadLine();

                if (Validation.IsValidMenuItem(key, methodDictionary.Count))
                {
                    methodDictionary[int.Parse(key)].Invoke();
                }
                else if (key != "e")
                {
                    Console.WriteLine("Invalid value specified!");
                }

                ChangedColor(ConsoleColor.Red);
                Console.WriteLine("\n\tEXIT THE APPLICATION - 'e'\n");
                ChangedColor(ConsoleColor.White);

            } while (key != "e");
        }
    }
}
