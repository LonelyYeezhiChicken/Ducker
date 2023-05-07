using Docker_Service.Helper.Interface;
using Docker_Service.Model;
using Docker_Service.Repository.Interface;
using Newtonsoft.Json;

public class AllExeRepository : IAllExeRepository
{
    private readonly IFileHelper _fileHelper;
    private const string FilePath = "AppData/exeList.json";

    public AllExeRepository(IFileHelper fileHelper)
    {
        _fileHelper = fileHelper;
    }

    /// <summary>
    /// 根據指定的識別碼取得對應的 AllExeDto 物件。
    /// </summary>
    /// <param name="id">識別碼。</param>
    /// <returns>對應的 AllExeDto 物件。</returns>
    /// <exception cref="NullReferenceException">當檔案不存在時拋出此例外。</exception>
    public async Task<AllExeDto> Get(string id)
    {
        var data = await _fileHelper.ReadFromFile(FilePath);

        if (data == null)
            throw new NullReferenceException("檔案不存在");


        var allExeDto = JsonConvert.DeserializeObject<AllExeDto>(data);
        return allExeDto;
    }

    /// <summary>
    /// 儲存指定的 AllExeDto 物件。
    /// </summary>
    /// <param name="allExeDto">要儲存的 AllExeDto 物件。</param>
    public async Task Save(AllExeDto allExeDto)
    {
        var data = JsonConvert.SerializeObject(allExeDto);
        await _fileHelper.WriteToFile(FilePath, data);
    }
}
