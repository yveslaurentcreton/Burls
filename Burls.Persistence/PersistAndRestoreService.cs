using System;
using System.Collections;
using System.IO;
using Burls.Application.Core.Commands;
using Burls.Application.Core.Queries;
using Burls.Application.Core.Services;
using Burls.Core.Services;
using Burls.Windows.Models;
using MediatR;

namespace Burls.Persistence
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;
        private readonly AppConfig _appConfig;
        private readonly BurlsDbContext _context;
        private readonly string _localAppData;
        private readonly string _localBurlsData;

        public PersistAndRestoreService(
            IMediator mediator,
            IFileService fileService,
            AppConfig appConfig,
            BurlsDbContext context)
        {
            _mediator = mediator;
            _fileService = fileService;
            _appConfig = appConfig;
            _context = context;

            _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _localBurlsData = Path.Combine(_localAppData, "Burls");
        }

        public void InitDataDirectory()
        {
            if (!Directory.Exists(_localBurlsData))
                Directory.CreateDirectory(_localBurlsData);

            AppDomain.CurrentDomain.SetData("DataDirectory", _localBurlsData);
        }

        public void PersistData()
        {
            var properties = _mediator.Send(new GetAllPropertiesQuery()).Result.Properties;

            if (properties != null)
            {
                var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
                var fileName = _appConfig.AppPropertiesFileName;
                _fileService.Save(folderPath, fileName, properties);
            }
        }

        public void RestoreData()
        {
            //RestoreProperties();
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
                    _mediator.Send(new AddPropertyCommand(property.Key, property.Value)).Wait();
                }
            }
        }
    }
}
