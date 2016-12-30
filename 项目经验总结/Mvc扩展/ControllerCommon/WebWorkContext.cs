using DTO;

namespace MvcCustommade.ControllerCommon
{
    public class WebWorkContext
    {
        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        public bool IsLogin { get; set; }


        /// <summary>
        /// 当前登录用户
        /// </summary>
        public UserDTO User { get; set; }

        /// <summary>
        /// 云码
        /// </summary>
        public string CloudId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否是Get请求
        /// </summary>
        public bool IsGet { get; set; }

        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        public bool IsAjax { get; set; }
    }
}