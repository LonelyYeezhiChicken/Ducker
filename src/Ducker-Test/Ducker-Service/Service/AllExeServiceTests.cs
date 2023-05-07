using Docker_Service.Model;
using Docker_Service.Repository.Interface;
using Docker_Service.Service;
using Docker_Service.Service.Interface;
using Moq;

namespace Ducker_Test.Ducker_Service.Service
{
    [TestFixture]
    public class AllExeServiceTests
    {
        private Mock<IAllExeRepository> allExeRepositoryMock;
        private IAllExeService allExeService;

        [SetUp]
        public void Init()
        {
            allExeRepositoryMock = new Mock<IAllExeRepository>();
            allExeService = new AllExeService(allExeRepositoryMock.Object);
        }

        [Test]
        public async Task 取得或建立AllExe_檔案存在_應回傳AllExeDto()
        {
            // Arrange
            var expectedDto = new AllExeDto()
            {
                Id = "123",
            };

            allExeRepositoryMock.Setup(mock => mock.Get())
                .ReturnsAsync(expectedDto);

            // Act
            var result = await allExeService.GetOrCreateAllExe();

            // Assert
            Assert.AreEqual(expectedDto.Id, result.Id);
        }
    }
}
