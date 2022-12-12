using Newtonsoft.Json;
using RetroGamesLibraryHelpers.Helpers;
using RetroGamesLibraryHelpers.Models;

namespace RetroGamesLibraryHelpers.Managers
{
    public class LibraryManager : ILibraryManager
    {
        private readonly IFileHelper _fileHelper;

        public LibraryManager(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public Library GetLibraryJsonData(string jsonDataPath, out string errorMessage)
        {
            var library = new Library(new List<Game>());
            var libraryString = _fileHelper.GetFileAsString(jsonDataPath, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
                return library;
            
            library = DeserializeLibrary(libraryString, out errorMessage);
            return library;
        }

        /// <summary>
        /// NOTE: Deserialize could be generalized and put in JsonHelper
        /// </summary>
        /// <returns></returns>
        private static Library DeserializeLibrary(string libraryString, out string errorMessage)
        {
            var library = new Library(new List<Game>());
            errorMessage = string.Empty;
            try
            {
                library = JsonConvert.DeserializeObject<Library>(libraryString) ?? new Library(new List<Game>());
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return library;
        }
    }
}
