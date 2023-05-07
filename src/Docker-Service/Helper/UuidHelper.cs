namespace Docker_Service.Helper
{
    public static class UuidHelper
    {
        /// <summary>
        /// 取得UUID
        /// </summary>
        /// <returns></returns>
        public static string GenerateUuid()
        {
            return Guid.NewGuid().ToString();
        }
    }

}
