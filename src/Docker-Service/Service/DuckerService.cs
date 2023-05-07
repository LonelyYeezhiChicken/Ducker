using Docker_Service.Model;
using Docker_Service.Repository.Interface;
using Docker_Service.Service.Interface;
using System.Diagnostics;

namespace Docker_Service.Service
{
    public class DuckerService : IDuckerService
    {
        private readonly IAllExeRepository _allExeRepository;
        private static List<ExeDto> _exes;

        public DuckerService(IAllExeRepository allExeRepository)
        {
            _allExeRepository = allExeRepository;

            if (_exes is null)
                _exes = new List<ExeDto>();
        }

        /// <summary>
        /// 取得目前註冊的 EXE 列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ExeDto>> GetRegisteredExes()
        {
            return await Task.FromResult(_exes);
        }

        /// <summary>
        /// 啟動指定路徑的 EXE
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public async Task StartExe(string exePath)
        {
            var process = CreateProcess(exePath);
            SubscribeToProcessEvents(process);

            StartProcess(process);

            var exeInfo = CreateExeInfo(exePath, process.Id);
            SaveExeInfo(exeInfo);

            await SaveToRepository(exeInfo);
        }

        /// <summary>
        /// 停止指定進程 ID 的 EXE
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public async Task StopExe(int processId)
        {
            var process = Process.GetProcessById(processId);
            CloseMainWindow(process);

            await WaitForProcessToExit();

            var exeInfo = GetExeInfoById(processId);
            if (exeInfo != null)
            {
                RemoveExeInfo(exeInfo);
                await RemoveFromRepository(exeInfo);
            }
        }

        /// <summary>
        /// 建立 Process 物件
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        private Process CreateProcess(string exePath)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
        }

        /// <summary>
        /// 訂閱 Process 事件
        /// </summary>
        /// <param name="process"></param>
        private void SubscribeToProcessEvents(Process process)
        {
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;
        }

        /// <summary>
        /// 啟動 Process
        /// </summary>
        /// <param name="process"></param>
        private void StartProcess(Process process)
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }

        /// <summary>
        /// 建立 EXE 資訊
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        private ExeDto CreateExeInfo(string exePath, int processId)
        {
            return new ExeDto
            {
                Path = exePath,
                Id = processId,
                StartTime = DateTime.Now
            };
        }

        /// <summary>
        /// 儲存已啟動的 EXE 資訊
        /// </summary>
        /// <param name="exeInfo"></param>
        private void SaveExeInfo(ExeDto exeInfo)
        {
            _exes.Add(exeInfo);
        }

        /// <summary>
        /// 將 EXE 資訊儲存到資料庫
        /// </summary>
        /// <param name="exeInfo"></param>
        /// <returns></returns>
        private async Task SaveToRepository(ExeDto exeInfo)
        {
            var allExeDto = await _allExeRepository.Get();
            allExeDto.ExeList.Add(exeInfo);
            await _allExeRepository.Save(allExeDto);
        }

        /// <summary>
        /// 關閉 Process 的主視窗
        /// </summary>
        /// <param name="process"></param>
        private void CloseMainWindow(Process process)
        {
            process.CloseMainWindow();
        }

        /// <summary>
        /// 等待 Process 結束
        /// </summary>
        /// <returns></returns>
        private async Task WaitForProcessToExit()
        {
            await Task.Delay(1000); // 等待 1
        }

        /// <summary>
        /// 根據進程 ID 取得對應的 EXE 資訊
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        private ExeDto GetExeInfoById(int processId)
        {
            return _exes.FirstOrDefault(e => e.Id == processId);
        }

        /// <summary>
        /// 移除已停止的 EXE 資訊
        /// </summary>
        /// <param name="exeInfo"></param>
        private void RemoveExeInfo(ExeDto exeInfo)
        {
            _exes.Remove(exeInfo);
        }

        /// <summary>
        /// 從資料庫刪除已停止的 EXE 資訊
        /// </summary>
        /// <param name="exeInfo"></param>
        /// <returns></returns>
        private async Task RemoveFromRepository(ExeDto exeInfo)
        {
            var allExeDto = await _allExeRepository.Get();
            allExeDto.ExeList.Remove(exeInfo);
            await _allExeRepository.Save(allExeDto);
        }

        /// <summary>
        /// 處理 EXE 的輸出資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // ...
            Console.WriteLine("EXE Output: " + e.Data);
        }

        /// <summary>
        /// 處理 EXE 的錯誤資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            // ...
            Console.WriteLine("EXE Output: " + e.Data);
        }
    }
}
