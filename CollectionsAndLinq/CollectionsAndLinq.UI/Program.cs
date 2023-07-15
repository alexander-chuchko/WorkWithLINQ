// See https://aka.ms/new-console-template for more information





using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.UI;

ApiSevice apiSevice = new ApiSevice(new DataProvider(), new DataProcessingService(new DataProvider()));

/*
var tasks1 = await apiSevice.GetTasksCountInProjectsByUserIdAsync(26);
var tasks2 = await apiSevice.GetCapitalTasksByUserIdAsync(1);
var tasks3 = await apiSevice.GetProjectsByTeamSizeAsync(20);
var tasks4 = await apiSevice.GetSortedTeamByMembersWithYearAsync(1994);
var tasks5 = await apiSevice.GetSortedUsersWithSortedTasksAsync();
var tasks6 = await apiSevice.GetUserInfoAsync(1000);
var tasks7 = await apiSevice.GetProjectsInfoAsync();
var tasks8 = await apiSevice.GetSortedFilteredPageOfProjectsAsync();*/



Console.WriteLine("Hello, World!");


