using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Linq;
using RetroGamesLibraryHelpers.Helpers;
using RetroGamesLibraryHelpers.Managers;
using RetroGamesLibraryHelpers.Models;
using RetroGamesLibraryHelpers.Services;

namespace RetroGameLibrary
{
    public partial class MainWindow
    {
        public readonly TimerService TimerService;
        private readonly IFileHelper _fileHelper;
        private readonly ILibraryManager _libraryManager;
        private readonly string _jsonDataPath;

        private Library _library = new(new List<Game>());
        private DateTime _dataLastUpdated = DateTime.MinValue;
        private readonly TimeSpan _timeFrequency = TimeSpan.FromSeconds(1);

        public MainWindow(ILibraryManager libraryManager, IFileHelper fileHelper)
        {
            InitializeComponent();

            _libraryManager = libraryManager;
            _fileHelper = fileHelper;

            TimerService = new TimerService(new PeriodicTimer(_timeFrequency));

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var rootAppPath = $"{assembly.Location.Replace(assembly.ManifestModule.Name, "")}data";
            // The data are copied in the build to output location
            _jsonDataPath = $"{rootAppPath}\\games.json";

            UpdateDataContext(_library);
            ResetTimerJob();
        }

        #region Events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResetTimerJob();
        }
        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            UpdateDataContext(new Library(new List<Game>()));
            TimerService.Cancel();
        }
        private void OnUnsubscribeClick(object sender, RoutedEventArgs e)
        {
            TimerService.Cancel();
        }
        #endregion

        /// <summary>
        /// This code should be integrated with the TimerService but isn't at the moment. Not sure of how to break out the local varíables for MainWindow
        /// and also trigger the method update the GUI from another class.
        /// It is not thread safe but not needed FOR NOW as the code is run only once at a time.
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        private async Task MonitorGameData(CancellationToken cancel)
        {
            while (await TimerService.Timer.WaitForNextTickAsync(TimerService.CancellationTokenSource.Token))
            {
                if (await DisposeIfCancelled(cancel))
                    return;
                var fileIsSame = _fileHelper.FileIsTheSame(_jsonDataPath, _dataLastUpdated, out var fileWriteTime, out var errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    HandleErrorMessage(errorMessage);
                    return;
                }

                if (fileIsSame)
                    continue;
                _dataLastUpdated = fileWriteTime;
                var library = GetLibrary();
                /* I guess it would be preferred to separate the json data fetching
                   and the gui update to two separate threads but I'm not sure how to
                   do that properly yet.  */
                await Dispatcher.BeginInvoke(new Action(delegate
                {
                    UpdateDataContext(library);
                }));
            }
        }
        private void ResetTimerJob()
        {
            _dataLastUpdated = DateTime.MinValue;

            TimerService.CancellationTokenSource = new CancellationTokenSource();
            TimerService.Timer = new PeriodicTimer(_timeFrequency);

            var task = MonitorGameData(TimerService.CancellationTokenSource.Token);
            TimerService.TimerTask = TimerService.HandleAsyncTask(task);
        }
        private Library GetLibrary()
        {
            var library = _libraryManager.GetLibraryJsonData(_jsonDataPath, out var errorMessage);

            HandleErrorMessage(errorMessage);
            if (string.IsNullOrEmpty(errorMessage) && !library.Games.Any())
            {
                MessageBox.Show("No valid game data found in games.json");
            }
            return library;
        }

        private void HandleErrorMessage(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage)) return;
            MessageBox.Show($"ERROR (SUBSCRIPTION CANCELLED): {errorMessage}");
            TimerService.Cancel();
        }

        private async Task<bool> DisposeIfCancelled(CancellationToken cancel)
        {
            if (!cancel.IsCancellationRequested) return false;
            await TimerService.DisposeAsync();
            return true;

        }
        /// <summary>
        /// Not sure if this is the right way to trigger updates of the DataContext
        /// but in this case I want to update everything as I do not check for changes
        /// and just update everything to be read from the file
        /// </summary>
        private void UpdateDataContext(Library library)
        {
            _library = library;
            DataContext = _library;
        }
    }
}
