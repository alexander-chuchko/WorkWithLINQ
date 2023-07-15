// See https://aka.ms/new-console-template for more information


using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.UI;

UserInterface userInterface = new UserInterface(new DataProcessingService(new DataProvider()));
userInterface.RunApplication();



