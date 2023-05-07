using Docker_Service.Helper;

namespace Ducker_Test.Ducker_Service.Helper
{
    [TestFixture]
    public class FileHelperTests
    {
        private const string TestFilePath = "test.txt";

        [Test]
        public async Task 寫入檔案_應將內容寫入檔案中()
        {
            // Arrange
            var fileHelper = new FileHelper();
            var content = "測試內容";

            // Act
            await fileHelper.WriteToFile(TestFilePath, content);

            // Assert
            Assert.IsTrue(File.Exists(TestFilePath));
            Assert.AreEqual(content, File.ReadAllText(TestFilePath));

            // Clean up
            File.Delete(TestFilePath);
        }

        [Test]
        public async Task 讀取檔案_存在的檔案_應傳回檔案內容()
        {
            // Arrange
            var fileHelper = new FileHelper();
            var content = "測試內容";
            await File.WriteAllTextAsync(TestFilePath, content);

            // Act
            var result = await fileHelper.ReadFromFile(TestFilePath);

            // Assert
            Assert.AreEqual(content, result);

            // Clean up
            File.Delete(TestFilePath);
        }

        [Test]
        public async Task 讀取檔案_不存在的檔案_應拋出FileNotFoundException()
        {
            // Arrange
            var fileHelper = new FileHelper();

            // Act & Assert
            Assert.ThrowsAsync<FileNotFoundException>(() => fileHelper.ReadFromFile(TestFilePath));
        }

        [Test]
        public async Task 檔案存在_存在的檔案_應傳回True()
        {
            // Arrange
            var fileHelper = new FileHelper();
            await File.WriteAllTextAsync(TestFilePath, "測試內容");

            // Act
            var result = await fileHelper.FileExists(TestFilePath);

            // Assert
            Assert.IsTrue(result);

            // Clean up
            File.Delete(TestFilePath);
        }

        [Test]
        public async Task 檔案存在_不存在的檔案_應傳回False()
        {
            // Arrange
            var fileHelper = new FileHelper();

            // Act
            var result = await fileHelper.FileExists(TestFilePath);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task 建立檔案_應建立空檔案()
        {
            // Arrange
            var fileHelper = new FileHelper();

            // Act
            await fileHelper.CreateFile(TestFilePath);

            // Assert
            Assert.IsTrue(File.Exists(TestFilePath));
            Assert.AreEqual(string.Empty, File.ReadAllText(TestFilePath));

            // Clean up
            File.Delete(TestFilePath);
        }
    }
}
