using Burls.Windows.Models;
using Burls.Windows.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Burls.Windows.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IBrowserService _browserService;

        public IReadOnlyList<Browser> Browsers { get; set; }

        private Browser _browser;
        public Browser Browser
        {
            get { return _browser; }
            set 
            { 
                _browser = value;
                RaisePropertyChanged();
            }
        }

        public ICommand UseCommand { get; set; }

        public MainViewModel(IBrowserService browserService)
        {
            _browserService = browserService;
            Browsers = browserService.GetBrowsers();

            UseCommand = new DelegateCommand(() => Use(), () => Browser != null)
                .ObservesProperty(() => Browser);
        }

        private void Use()
        {
            throw new NotImplementedException();
        }
    }
}
