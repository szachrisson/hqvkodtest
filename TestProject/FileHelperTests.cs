using RetroGamesLibraryHelpers.Helpers;

namespace RetroGameLibraryTests
{
    public class FileHelperTests
    {
        [Fact]
        public void ShouldHandleAllFileNames()
        {
            var fileHelper = new FileHelper();
            var fileIsTheSame = fileHelper.FileIsTheSame("hello", DateTime.MinValue, out var fileWriteTime, out var errorMessage);
            Assert.False(fileIsTheSame);
        }
        [Fact]
        public void ShouldHandleInvalidFiles()
        {
            var fileHelper = new FileHelper();
            fileHelper.GetFileAsString("hello", out string errorMessage);
            Assert.Contains("Could not find", errorMessage);
        }
    }
}