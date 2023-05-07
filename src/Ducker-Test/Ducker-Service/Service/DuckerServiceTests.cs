using Docker_Service.Model;
using Docker_Service.Repository.Interface;
using Docker_Service.Service;
using Docker_Service.Service.Interface;
using Moq;

namespace Ducker_Test.Ducker_Service.Service
{
    [TestFixture]
    public class DuckerServiceTests
    {
        private Mock<IAllExeRepository> _mockAllExeRepository;
        private IDuckerService _duckerService;

        [SetUp]
        public void Init()
        {
            _mockAllExeRepository = new Mock<IAllExeRepository>();
            _duckerService = new DuckerService(_mockAllExeRepository.Object);
        }

        //[Test]
        //public async Task 取得目前註冊的_EXE列表()
        //{
        //    // Arrange
        //    var exeDto = new ExeDto { Path = "Test/Ducker-Service.exe", Id = 1, StartTime = DateTime.Now };
        //    _duckerService.StartExe(exeDto.Path);

        //    // Act
        //    var result = await _duckerService.GetRegisteredExes();

        //    // Assert
        //    Assert.AreEqual(1, result.Count());
        //    Assert.AreEqual(exeDto.Path, result.First().Path);
        //}

        //[Test]
        //public async Task 啟動指定路徑的_EXE()
        //{
        //    // Arrange
        //    var exePath = "Test/Ducker-Service.exe";

        //    // Act
        //    await _duckerService.StartExe(exePath);

        //    // Assert
        //    var result = await _duckerService.GetRegisteredExes();
        //    Assert.AreEqual(1, result.Count());
        //    Assert.AreEqual(exePath, result.First().Path);
        //}

        //[Test]
        //public async Task 停止指定進程_ID的_EXE()
        //{
        //    // Arrange
        //    var exePath = "Test/Ducker-Service.exe";
        //    await _duckerService.StartExe(exePath);
        //    var exeInfo = (await _duckerService.GetRegisteredExes()).First();

        //    // Act
        //    await _duckerService.StopExe(exeInfo.Id);

        //    // Assert
        //    var result = await _duckerService.GetRegisteredExes();
        //    Assert.AreEqual(0, result.Count());
        //}
    }
}