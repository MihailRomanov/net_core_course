using FluentAssertions;
using ImageProcessingService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.IO.Abstractions.TestingHelpers;

namespace ImageProcessingService.Tests
{
    public class BarcodeReaderTests
    {

        [Test]
        public void ReadBarcodeFromImage()
        {
            var fileName = "Tes_page.png";

            var fileSystem = new MockFileSystem();
            fileSystem.AddFile(fileName, new MockFileData(TestData.Test_page));

            var fileHelper = new FileHelper(fileSystem);

            var barcoderReader = new BarcodeReader(
                fileHelper, new Mock<ILogger<BarcodeReader>>().Object);

            var text = barcoderReader.Read(fileName);

            text.Should().Be("My Test Barcode");
        }
    }
}
