using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestfulApi.Filters.SwaggerFilter
{
    /// <summary>
    /// 上传参数配置
    /// </summary>
    public class UploadParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, System.Web.Http.Description.ApiDescription apiDescription)
        {
            //如果方法名里包含upload,则提供一个参数名为file类型为file的参数
            if (apiDescription.ActionDescriptor.ActionName.Contains("Upload"))
            {
                operation.consumes.Add("application/form-data");
                operation.parameters.Add(new Parameter
                {
                    name = "file",
                    @in = "formData",
                    required = true,
                    type = "file"
                });
            }

        }
    }
}