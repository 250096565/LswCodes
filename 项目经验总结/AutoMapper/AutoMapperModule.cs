using System.Reflection;
using AutoMapper;

namespace AutoMapperExt.AutoMapper
{
    public class AutoMapperModule
    {
        public static void Init()
        {
            //拿到dto程序集
            var asm = Assembly.Load("DTO");

            //拿到自定义的类型
            var types = asm.GetExportedTypes();
            //创建映射
            Mapper.Initialize(o =>
            {
                foreach (var type in types)
                {
                    //判断是否贴上了AutoMapAttribute
                    if (!type.IsDefined(typeof(AutoMapAttribute))) continue;
                    var autoMapper = type.GetCustomAttribute<AutoMapAttribute>();
                    foreach (var source in autoMapper.ToSource)
                    {
                        o.CreateMap(type, source);
                        o.CreateMap(source, type);
                        o.CreateMap(source, source);

                    }
                    
                }
            });
        }
    }

}
