using System.Collections.Generic;
using RetroGamesLibraryHelpers.Models;

namespace RetroGameLibrary.Context
{
    public class LibraryContext
    {
        public LibraryContext(List<Game> games)
        {
            Games = games;
        }

        public List<Game> Games { get; set; }
    }
}
