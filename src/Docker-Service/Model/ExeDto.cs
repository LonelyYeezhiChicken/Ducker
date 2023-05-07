namespace Docker_Service.Model
{
    /// <summary>
    /// 表示一個 EXE 檔案的資料傳輸物件。
    /// </summary>
    public class ExeDto
    {
        /// <summary>
        /// 取得或設定 EXE 檔案的唯一識別碼。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 取得或設定 EXE 檔案的名稱。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 取得或設定 EXE 檔案的描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 取得或設定 EXE 檔案的路徑
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 取得或設定 EXE 檔案的狀態。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 取得或設定 EXE 檔案的啟動時間
        /// </summary>
        public DateTime StartTime { get; set; }
    }

}
