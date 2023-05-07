using Docker_Service.Helper;
using Docker_Service.Model;

namespace Docker_Service.Service.Interface
{
    /// <summary>
    /// 定義操作包含多個 EXE 檔案的資料傳輸物件的服務介面。
    /// </summary>
    public interface IAllExeService
    {
        /// <summary>
        /// 根據指定的識別碼取得對應的 AllExeDto 物件，如果檔案不存在則建立新的 AllExeDto 物件並儲存。
        /// </summary>
        /// <returns>對應的 AllExeDto 物件。</returns>
        Task<AllExeDto> GetOrCreateAllExe();
    }

}
