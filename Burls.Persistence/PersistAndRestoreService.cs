using System;
using System.Collections;
using System.IO;
using Burls.Core.Services;
using Burls.Windows.Models;

namespace Burls.Persistence
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private readonly IFileService _fileService;
        private readonly IPropertyManager _propertiesProvider;
        private readonly AppConfig _appConfig;
        private readonly BurlsDbContext _context;
        private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public PersistAndRestoreService(
            IFileService fileService,
            IPropertyManager propertiesProvider,
            AppConfig appConfig,
            BurlsDbContext context)
        {
            _fileService = fileService;
            _propertiesProvider = propertiesProvider;
            _appConfig = appConfig;
            _context = context;
        }

        public void PersistData()
        {
            if (_propertiesProvider.GetProperties() != null)
            {
                var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
                var fileName = _appConfig.AppPropertiesFileName;
                _fileService.Save(folderPath, fileName, _propertiesProvider.GetProperties());
            }
        }

        public void RestoreData()
        {
            RestoreProperties();
            _context.Database.EnsureCreated();
        }

        private void RestoreProperties()
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.AppPropertiesFileName;
            var properties = _fileService.Read<IDictionary>(folderPath, fileName);
            if (properties != null)
            {
                foreach (DictionaryEntry property in properties)
                {
                    _propertiesProvider.AddProperty(property.Key, property.Value);
                }
            }
        }
    }
}
