namespace Docker_Service.Model
{
    /// <summary>
    /// 表示包含多個 EXE 檔案的資料傳輸物件。
    /// </summary>
    public class AllExeDto
    {
        public AllExeDto()
        {
            ExeList = new List<ExeDto>();
        }

        /// <summary>
        /// 取得或設定多個 EXE 檔案的唯一識別碼。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 取得或設定包含多個 EXE 檔案的清單。
        /// </summary>
        public List<ExeDto> ExeList { get; set; }
    }
}
