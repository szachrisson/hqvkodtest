namespace RetroGamesLibraryHelpers.Helpers;

public class FileHelper : IFileHelper
{
    public bool FileIsTheSame(string filePath, DateTime fileLastUpdated, out DateTime fileWriteTime, out string errorMessage)
    {
        errorMessage = string.Empty;
        fileWriteTime = DateTime.MinValue;
        var fileIsSame = false;
        try
        {
            // NOTE: This code is not thread safe as the file could be read by another thread at the same time.
            fileWriteTime = File.GetLastWriteTime(filePath);
            fileIsSame = fileWriteTime == fileLastUpdated;
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
        }
        return fileIsSame;
    }

    public string GetFileAsString(string filePath, out string errorMessage)
    {
        var fileString = string.Empty;
        errorMessage = string.Empty;
        try
        {
            // NOTE: This code is not thread safe as the file could be read by another thread at the same time.
            using var r = new StreamReader(filePath);
            fileString = r.ReadToEnd();
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
        }

        if (!string.IsNullOrEmpty(errorMessage))
            return fileString;
        if (string.IsNullOrEmpty(fileString))
            errorMessage = "Data file is empty";
        return fileString;
    }
}