using AutoBogus;
using Bogus;
using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Commands;
using Burls.Application.Core.Handlers;
using Burls.Application.Core.Services;
using Burls.Application.Core.State;
using Burls.Domain;
using Burls.Testing.Helpers;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Burls.Application.Tests.Core.Handlers
{
    public class ApplicationInitializeCommandHandlerTests
    {
        private readonly Mock<ILogger<ApplicationInitializeCommandHandler>> _fakeLogger;
        private readonly Mock<IApplicationState> _applicationStore;
        private readonly Mock<IBrowserState> _browserState;
        private readonly Mock<IPersistAndRestoreService> _persistAndRestoreService;
        private readonly Mock<IBrowserService> _browserService;

        private readonly ApplicationInitializeCommandHandler _handler;

        public ApplicationInitializeCommandHandlerTests()
        {
            _fakeLogger = new Mock<ILogger<ApplicationInitializeCommandHandler>>();

            // Init stores
            _applicationStore = new Mock<IApplicationState>();
            _browserState = new Mock<IBrowserState>();

            _applicationStore.SetupAllProperties();
            _browserState.SetupAllProperties();

            // Init services
            _persistAndRestoreService = new Mock<IPersistAndRestoreService>();
            _browserService = new Mock<IBrowserService>();

            // Init handler
            //_handler = new ApplicationInitializeCommandHandler(
            //    _fakeLogger.Object,
            //    _applicationStore.Object,
            //    _browserState.Object,
            //    _persistAndRestoreService.Object,
            //    _browserService.Object);
        }

        [Fact]
        public async Task Handler_WithValidRequestUrl_AndMatchingBrowserProfile_ShouldBeSelectMode_AndUseThatBrowserProfile()
        {
            // Arrange
            var requestUrl = new Faker().Internet.Url();
            var host = new Uri(requestUrl).Host;
            var command = new ApplicationInitializeCommand(new[] { requestUrl });
            var browserFaker = new AutoFaker<Browser>()
                .UsePrivateConstructor();
            var profileFaker = new AutoFaker<Profile>()
                .UsePrivateConstructor();
                //.RuleFor(p => p.Websites, new AutoFaker<Website>()
                //    .RuleFor(w => w.Domain, host)
                //    .Generate(1));
            var browserProfiles = new AutoFaker<BrowserProfile>()
                .CustomInstantiator(f => new BrowserProfile(browserFaker.Generate(), profileFaker.Generate(), f.Random.String()))
                .Generate(1);

            _browserService
                .Setup(x => x.GetBrowserProfilesAsync())
                .ReturnsAsync(browserProfiles);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _applicationStore.VerifySet(x => x.ApplicationMode = ApplicationMode.Select, Times.Once);
            _browserService.Verify(x => x.UseBrowserProfileAsync(browserProfiles.First(), requestUrl, false), Times.Once);
            result.Should().BeOfType<Unit>();
        }

        [Fact]
        public async Task Handler_WithValidRequestUrl_AndNoMatchingBrowserProfile_ShouldBeSelectMode()
        {
            // Arrange
            var requestUrl = new Faker().Internet.Url();
            var command = new ApplicationInitializeCommand(new[] { requestUrl });
            var browserProfiles = new List<BrowserProfile>();

            _browserService
                .Setup(x => x.GetBrowserProfilesAsync())
                .ReturnsAsync(browserProfiles);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _applicationStore.VerifySet(x => x.ApplicationMode = ApplicationMode.Select, Times.Once);
            _browserService.Verify(x => x.UseBrowserProfileAsync(It.IsAny<BrowserProfile>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
            result.Should().BeOfType<Unit>();
        }

        [Fact]
        public async Task Handler_WithInvalidRequestUrl_ShouldBeSettingMode()
        {
            // Arrange
            var command = new ApplicationInitializeCommand(new[] { "asdfasdf" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _applicationStore.VerifySet(x => x.ApplicationMode = ApplicationMode.Settings, Times.Once);
            result.Should().BeOfType<Unit>();
        }
    }
}
