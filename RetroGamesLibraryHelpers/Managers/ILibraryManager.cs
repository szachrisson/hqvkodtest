using RetroGamesLibraryHelpers.Models;

namespace RetroGamesLibraryHelpers.Managers;

public interface ILibraryManager
{
    public Library GetLibraryJsonData(string jsonDataPath, out string errorMessage);
}