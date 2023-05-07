using Docker_Service.Helper.Interface;
using Docker_Service.Model;
using Moq;
using Newtonsoft.Json;

namespace Ducker_Test.Repository.Tests
{
    [TestFixture]
    public class AllExeRepositoryTests
    {
        private Mock<IFileHelper> fileHelperMock;
        private AllExeRepository allExeRepository;
        private const string FilePath = "AppData/";

        [SetUp]
        public void Init()
        {
            fileHelperMock = new Mock<IFileHelper>();
            allExeRepository = new AllExeRepository(fileHelperMock.Object);
        }

        [Test]
        public async Task 取得_有效的Id_檔案存在_應回傳AllExeDto()
        {
            // Arrange
            string id = "123";
            var expectedDto = new AllExeDto()
            {
                Id = id,
            };

            fileHelperMock.Setup(mock => mock.ReadFromFile($"{FilePath}{id}.json"))
                .ReturnsAsync(JsonConvert.SerializeObject(expectedDto));

            // Act
            var result = await allExeRepository.Get(id);

            // Assert
            Assert.AreEqual(expectedDto.Id, result.Id);
        }

        [Test]
        public async Task 取得_有效的Id_檔案不存在_應拋出NullReferenceException()
        {
            // Arrange
            string id = "123";

            fileHelperMock.Setup(mock => mock.ReadFromFile($"{FilePath}{id}.json"))
                .ReturnsAsync((string)null);

            // Act and Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await allExeRepository.Get(id));
        }


        [Test]
        public async Task 儲存_應寫入檔案()
        {
            // Arrange
            var dto = new AllExeDto();

            // Act
            await allExeRepository.Save(dto);

            // Assert
            fileHelperMock.Verify(mock => mock.WriteToFile($"{FilePath}{dto.Id}.json", JsonConvert.SerializeObject(dto)), Times.Once);
        }
    }
}
