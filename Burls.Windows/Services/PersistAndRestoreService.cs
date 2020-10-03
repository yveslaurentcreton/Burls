using System;
using System.Collections;
using System.IO;
using Burls.Core.Services;
using Burls.Windows.Contracts.Services;
using Burls.Windows.Models;

namespace Burls.Windows.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private readonly IFileService _fileService;
        private readonly AppConfig _appConfig;
        private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public PersistAndRestoreService(IFileService fileService, AppConfig appConfig)
        {
            _fileService = fileService;
            _appConfig = appConfig;
        }

        public void PersistData()
        {
            if (App.Current.Properties != null)
            {
                var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
                var fileName = _appConfig.AppPropertiesFileName;
                _fileService.Save(folderPath, fileName, App.Current.Properties);
            }
        }

        public void RestoreData()
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.AppPropertiesFileName;
            var properties = _fileService.Read<IDictionary>(folderPath, fileName);
            if (properties != null)
            {
                foreach (DictionaryEntry property in properties)
                {
                    App.Current.Properties.Add(property.Key, property.Value);
                }
            }
        }
    }
}
