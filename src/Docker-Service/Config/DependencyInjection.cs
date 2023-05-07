namespace Docker_Service.Config
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 依賴注入容器
        /// </summary>
        /// <param name="builder"></param>
        public static void DIContainer(this IServiceCollection services)
        {

            // Helpers Di AddSingleton
            var diHelpers = typeof(Program).Assembly.GetExportedTypes()
             .Where(x => !x.IsAbstract && !x.IsInterface)
             .Where(x => x.Name.EndsWith("Helper"));

            var idHelpers = typeof(Program).Assembly.GetExportedTypes()
                  .Where(x => x.IsAbstract || x.IsInterface)
                  .Where(x => x.Name.EndsWith("Helper"));

            diHelpers.ToList().ForEach(obj =>
            {
                var intf = idHelpers.Where(x => x.Name.Contains(obj.Name)).First();
                services.AddSingleton(intf, obj);
            });


            //Dao Service Repo Di AddScoped
            var diObjs = typeof(Program).Assembly.GetExportedTypes()
             .Where(x => !x.IsAbstract && !x.IsInterface)
             .Where(x => x.Name.EndsWith("Dao")
                      || x.Name.EndsWith("Repository")
                      || x.Name.EndsWith("Service"))
             .Where(x => !x.Name.Equals("QuartzHostedService"));

            var idiObjs = typeof(Program).Assembly.GetExportedTypes()
                  .Where(x => x.IsAbstract || x.IsInterface)
                  .Where(x => x.Name.EndsWith("Dao")
                           || x.Name.EndsWith("Repository")
                           || x.Name.EndsWith("Service"));

            diObjs.ToList().ForEach(obj =>
            {
                var intf = idiObjs.Where(x => x.Name.Contains(obj.Name)).First();
                services.AddScoped(intf, obj);
            });
        }
    }
}
