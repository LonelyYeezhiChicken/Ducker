﻿using Docker_Service.Model;

namespace Docker_Service.Repository.Interface
{
    /// <summary>
    /// 定義操作包含多個 EXE 檔案的資料傳輸物件的存取介面。
    /// </summary>
    public interface IAllExeRepository
    {
        /// <summary>
        /// 取得對應的 AllExeDto 物件。
        /// </summary>
        /// <returns>對應的 AllExeDto 物件。</returns>
        /// <exception cref="NullReferenceException">當檔案不存在時拋出此例外。</exception>
        Task<AllExeDto> Get();

        /// <summary>
        /// 儲存指定的 AllExeDto 物件。
        /// </summary>
        /// <param name="allExeDto">要儲存的 AllExeDto 物件。</param>
        Task Save(AllExeDto allExeDto);
    }
}
