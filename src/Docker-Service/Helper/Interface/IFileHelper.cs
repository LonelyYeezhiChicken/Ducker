namespace Docker_Service.Helper.Interface
{
    public interface IFileHelper
    {
        /// <summary>
        /// 輸出檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        Task WriteToFile(string filePath, string content);
        /// <summary>
        /// 讀取檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<string> ReadFromFile(string filePath);
        /// <summary>
        /// 檢查檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<bool> FileExists(string filePath);
        /// <summary>
        /// 建立檔案
        /// </summary>
        /// <param name="filePath"></param>
        Task CreateFile(string filePath);
    }
}
