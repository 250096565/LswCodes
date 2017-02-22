
namespace Dm.AiMa.Domain
{
    public class StringLengthConsts
    {
        /// <summary>
        /// 密码库
        /// </summary>
        public class Bank
        {
            /// <summary>
            /// 名称
            /// </summary>
            public const int Name = 32;
            /// <summary>
            /// 图标
            /// </summary>
            public const int Image = 256;
            /// <summary>
            /// 描述
            /// </summary>
            public const int Description = 512;

            /// <summary>
            /// 密码库项
            /// </summary>
            public class Item
            {
                /// <summary>
                /// 数据的名称
                /// </summary>
                public const int Name = 32;
                /// <summary>
                /// 数据的Url
                /// </summary>
                public const int Url = 256;
                /// <summary>
                /// 数据的描述
                /// </summary>
                public const int Description = 512;
            }
        }

        public class Tag
        {
            /// <summary>
            /// 名称
            /// </summary>
            public const int Name = 32;
        }

        public class Notification
        {
            /// <summary>
            /// 内容
            /// </summary>
            public const int Content = 256;

            ///// <summary>
            ///// 操作
            ///// </summary>
            //public const int Action = 64;

            /// <summary>
            /// 类型
            /// </summary>
            public const int Type = 16;
        }

        public class Activity
        {
            /// <summary>
            /// 操作关键字
            /// </summary>
            public const int Action = 32;

            /// <summary>
            /// 操作名称
            /// </summary>
            public const int Action_Msg = 64;

            /// <summary>
            /// 操作内容名称
            /// </summary>
            public const int ActionData_Name = 32;

            /// <summary>
            /// 类型
            /// </summary>
            public const int Type = 16;

            /// <summary>
            /// 活动类型
            /// </summary>
            public const int Target_Type = 16;

        }
    }
}
