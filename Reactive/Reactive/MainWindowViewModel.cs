using GalaSoft.MvvmLight;
using Oracle.ManagedDataAccess.Client;
using Reactive.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reactive
{
    public sealed class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private bool _disposedValue;
        private readonly AzureService _azureService = new AzureService();

        private string _searchString;

        private readonly IDisposable _searchSubscription;
        public string SearchString
        {
            get => _searchString;
            set
            {
                if (Set(ref _searchString, value))
                {
                    StringObservable.StringChanged(value);
                }
            }
        }

        private StringObservable StringObservable { get; }

        public List<AzureWorkItem> Results { get; private set; } = new List<AzureWorkItem>();

        public ObservableCollection<AzureWorkItem> Drugs { get; } = new ObservableCollection<AzureWorkItem>();

        public IDrugSearch DrugSearch { get; }


        public MainWindowViewModel()
        {
            StringObservable = new StringObservable();

            var searchTextObservable = StringObservable
                .Where(text => text.Length > 2 || text.Length == 0)
                .Throttle(TimeSpan.FromMilliseconds(300))
                .DistinctUntilChanged()
                .PairWithPrevious()
                .Where(pair => !AlreadyEmptySearch(pair.previous, pair.current, Results))
                .Select(pair => DoSearchAsync(pair.previous, pair.current))
                .Merge()
                .ObserveOnDispatcher();

            _searchSubscription = searchTextObservable.Subscribe(list =>
            {
                Drugs.Clear();
                foreach (var drug in list)
                {
                    Drugs.Add(drug);
                }
            });
        }

        private void Dispose(bool disposing)
        {
            if (!_disposedValue && disposing)
            {
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private static bool AlreadyEmptySearch(string previous, string current, IEnumerable<AzureWorkItem> list) =>
                previous != null && current.StartsWith(previous, StringComparison.OrdinalIgnoreCase) && !list.Any();

        private async Task<IEnumerable<AzureWorkItem>> DoSearchAsync(string previous, string current)
        {
            if (string.IsNullOrEmpty(current))
            {
                Debug.WriteLine("## Clear");
                return Enumerable.Empty<AzureWorkItem>();
            }

            if (previous != null && current.StartsWith(previous, StringComparison.OrdinalIgnoreCase))
            {
                Debug.WriteLine("+++ Reusing...");
                Results = Results.Where(s => s.Title.StartsWith(current, StringComparison.OrdinalIgnoreCase)).ToList();
                return Results;
            }


            Debug.WriteLine("*** Searching...");
            var results = await _azureService.GetWorkItems(current);
            Results = results.ToList();

            return Results;
        }
    }
}
