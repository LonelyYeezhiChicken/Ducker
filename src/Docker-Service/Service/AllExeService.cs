using Docker_Service.Helper;
using Docker_Service.Model;
using Docker_Service.Repository.Interface;
using Docker_Service.Service.Interface;

namespace Docker_Service.Service
{

    public class AllExeService : IAllExeService
    {
        private readonly IAllExeRepository _allExeRepository;

        public AllExeService(IAllExeRepository allExeRepository)
        {
            _allExeRepository = allExeRepository;
        }

        /// <summary>
        /// 根據指定的識別碼取得對應的 AllExeDto 物件，如果檔案不存在則建立新的 AllExeDto 物件並儲存。
        /// </summary>
        /// <returns>對應的 AllExeDto 物件。</returns>
        public async Task<AllExeDto> GetOrCreateAllExe()
        {
            try
            {
                var allExeDto = await _allExeRepository.Get();
                return allExeDto;
            }
            catch (FileNotFoundException)
            {
                // 檔案不存在，建立新的 AllExeDto 物件並儲存到資料庫
                var newAllExeDto = new AllExeDto
                {
                    Id = UuidHelper.GenerateUuid()
                };
                await _allExeRepository.Save(newAllExeDto);
                return newAllExeDto;
            }
        }
    }

}
