using Docker_Service.Helper.Interface;

namespace Docker_Service.Helper
{
    public class FileHelper : IFileHelper
    {
        /// <summary>
        /// 輸出檔案
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        /// <param name="content">檔案內容</param>
        public async Task WriteToFile(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }

        /// <summary>
        /// 讀取檔案
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        /// <returns>檔案內容</returns>
        /// <exception cref="FileNotFoundException">當檔案不存在時拋出異常</exception>
        public async Task<string> ReadFromFile(string filePath)
        {
            if (await FileExists(filePath))
            {
                return await File.ReadAllTextAsync(filePath);
            }
            else
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
        }

        /// <summary>
        /// 檢查檔案是否存在
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        /// <returns>如果檔案存在則為 true，否則為 false</returns>
        public async Task<bool> FileExists(string filePath)
        {
            return await Task.FromResult(File.Exists(filePath));
        }

        /// <summary>
        /// 建立檔案
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        public async Task CreateFile(string filePath)
        {
            await File.WriteAllTextAsync(filePath, string.Empty);
        }
    }
}