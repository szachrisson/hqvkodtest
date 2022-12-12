using Newtonsoft.Json;

namespace RetroGamesLibraryHelpers.Models
{
    public class Library
    {
            [JsonProperty(PropertyName = "games")]
            public List<Game> Games { get; set; }

            public Library(List<Game> games)
            {
                Games = games;
            }
    }
    public class Game
    {
        public Game(string gameTitle, string gameCategory, bool gameWorking)
        {
            GameTitle = gameTitle;
            GameCategory = gameCategory;
            GameWorking = gameWorking;
        }
        // Note: Properties are supposed to be validated by a schema file.
        [JsonProperty(PropertyName = "gameTitle")]
        public string GameTitle { get; set; }
        [JsonProperty(PropertyName = "gameCategory")]
        public string GameCategory { get; set; }
        [JsonProperty(PropertyName = "gameWorking")]
        public bool GameWorking { get; set; }
    }
}
