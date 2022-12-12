namespace RetroGamesLibraryHelpers.Helpers;

public interface IFileHelper
{
    public bool FileIsTheSame(string filePath, DateTime fileLastUpdated, out DateTime fileWriteTime, out string errorMessage);
    public string GetFileAsString(string filePath, out string errorMessage);
}