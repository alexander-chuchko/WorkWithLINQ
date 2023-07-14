// See https://aka.ms/new-console-template for more information





using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.UI;

ApiSevice apiSevice = new ApiSevice(new DataProvider());

var res = await apiSevice.GetProjectsAsync();

Console.WriteLine("Hello, World!");


