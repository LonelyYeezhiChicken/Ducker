using Docker_Service.Helper.Interface;
using Docker_Service.Model;
using Docker_Service.Service.Interface;
using Moq;
using Newtonsoft.Json;

namespace Ducker_Test.Repository.Tests
{
    [TestFixture]
    public class AllExeRepositoryTests
    {
        private Mock<IFileHelper> fileHelperMock;
        private AllExeRepository allExeRepository;
        private const string FilePath = "AppData/exeList.json";

        [SetUp]
        public void Init()
        {
            fileHelperMock = new Mock<IFileHelper>();
            allExeRepository = new AllExeRepository(fileHelperMock.Object);
        }

        [Test]
        public async Task Get_檔案存在_應回傳AllExeDto()
        {
            // Arrange
            var expectedDto = new AllExeDto()
            {
                Id = "123",
            };

            fileHelperMock.Setup(mock => mock.ReadFromFile(FilePath))
                .ReturnsAsync(JsonConvert.SerializeObject(expectedDto));

            // Act
            var result = await allExeRepository.Get();

            // Assert
            Assert.AreEqual(expectedDto.Id, result.Id);
        }

        [Test]
        public async Task Save_應寫入檔案()
        {
            // Arrange
            var dto = new AllExeDto();

            // Act
            await allExeRepository.Save(dto);

            // Assert
            fileHelperMock.Verify(mock => mock.WriteToFile(FilePath, JsonConvert.SerializeObject(dto)), Times.Once);
        }
    }
}
