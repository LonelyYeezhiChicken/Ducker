using Docker_Service.Model;

namespace Docker_Service.Service.Interface
{
    /// <summary>
    /// 定義 Docker 服務的操作介面。
    /// </summary>
    public interface IDuckerService
    {
        /// <summary>
        /// 取得目前註冊的 EXE 列表。
        /// </summary>
        /// <returns>目前註冊的 EXE 列表。</returns>
        Task<IEnumerable<ExeDto>> GetRegisteredExes();

        /// <summary>
        /// 啟動指定路徑的 EXE。
        /// </summary>
        /// <param name="exePath">EXE 的路徑。</param>
        Task StartExe(string exePath);

        /// <summary>
        /// 停止指定進程 ID 的 EXE。
        /// </summary>
        /// <param name="processId">要停止的進程 ID。</param>
        Task StopExe(int processId);
    }
}
