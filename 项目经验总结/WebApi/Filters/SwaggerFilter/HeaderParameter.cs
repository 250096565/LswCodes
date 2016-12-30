using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace RestfulApi.SwaggerFilter
{
    public class HeaderParameter : IOperationFilter
    {
        /// <summary>
        /// 设置请求参数,放到header中
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) operation.parameters = new List<Parameter>();

            operation.parameters.Add(new Parameter
            {
                name = "version",
                @in = "header",
                description = "版本",
                required = false,
                type = "string"
            });
            operation.parameters.Add(new Parameter
            {
                name = "source",
                @in = "header",
                description = "来源",
                required = false,
                type = "string"
            });
            operation.parameters.Add(new Parameter
            {
                name = "token",
                @in = "header",
                description = "用户登陆状态token",
                required = false,
                type = "string"
            });
            operation.parameters.Add(new Parameter
            {
                name = "timestamp",
                @in = "header",
                description = "时间戳",
                required = false,
                type = "string"
            });
            operation.parameters.Add(new Parameter
            {
                name = "signature",
                @in = "header",
                description = "签名",
                required = false,
                type = "string"
            });
        }
    }
}