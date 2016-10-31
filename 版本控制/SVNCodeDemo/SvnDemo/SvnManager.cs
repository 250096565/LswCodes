using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using svnOper;
using SharpSvn;

namespace SvnDemo
{
    public class SvnManager
    {
        //初始化对象
        public static bool Initialize(string userName, string passWord)
        {
            svnStru s = new svnStru("", "", userName, passWord);
            operSVN.svnS = s;
            if (operSVN.init(operSVN.svnS))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检出
        /// </summary>
        /// <param name="serviceUrl">服务器Url</param>
        /// <param name="localPath">本地路径</param>
        /// <returns></returns>
        public static bool CheckOut(string serviceUrl, string localPath)
        {
            operSVN.svnS.urlPath = serviceUrl;
            operSVN.svnS.localPath = localPath;
            if (operSVN.checkout())
            {
                return true; ;
            }
            return false;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Update(string path)
        {
            bool result = true;
            using (SvnClient svn = new SvnClient())
            {
                try
                {
                    result = svn.Update(path);
                }
                catch (Exception ex)
                {
                    operSVN.lastErrMsg = ex.Message;
                    StackFrame frame = new StackTrace(new StackFrame(true)).GetFrame(0);
                    operSVN.wlog(ex.Message, frame.GetFileName() + (object)"|" + (string)(object)frame.GetMethod() + "|" + (string)(object)frame.GetFileLineNumber() + "|" + (string)(object)frame.GetFileColumnNumber());
                    return false;
                }
            }

            if (operSVN.update(path))
            {
                return true;
            }
            return result;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Commit(string path)
        {
            if (operSVN.commit(path))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取日志上改变的文件路径
        /// </summary>
        /// <param name="path">本地路径</param>
        /// <param name="version">某个具体版本</param>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public static List<string> GetLogFPath(string path, int version, string action)
        {
            List<string> paths = new List<string>();
            using (SvnClient svn = new SvnClient())
            {
                try
                {
                    Collection<SvnLogEventArgs> logItems = new Collection<SvnLogEventArgs>();
                    svn.GetLog(path, out logItems);
                    //过滤版本
                    //logItems = (Collection<SvnLogEventArgs>)logItems.Where(o => o.Revision == version);
                    foreach (var log in logItems)
                    {
                        foreach (SvnChangeItem svnChangeItem in (Collection<SvnChangeItem>)log.ChangedPaths)
                        {
                            if (svnChangeItem.Action.ToString() == action && log.Revision == version)
                            {
                                paths.Add(svnChangeItem.Path);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    operSVN.lastErrMsg = ex.Message;
                    StackFrame frame = new StackTrace(new StackFrame(true)).GetFrame(0);
                    operSVN.wlog(ex.Message, frame.GetFileName() + (object)"|" + (string)(object)frame.GetMethod() + "|" + (string)(object)frame.GetFileLineNumber() + "|" + (string)(object)frame.GetFileColumnNumber());
                    return null;
                }

            }
            return paths;
        }


        /// <summary>
        /// 获取日志上改变的文件路径
        /// </summary>
        /// <param name="path">本地路径</param>
        /// <param name="startVersion">开始版本</param>
        /// <param name="endVersion">结束版本</param>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public static List<string> GetLogFPath(string path, int startVersion, int endVersion, string action)
        {
            List<string> paths = new List<string>();
            using (SvnClient svn = new SvnClient())
            {
                try
                {
                    //拿到所有的日志
                    Collection<SvnLogEventArgs> logItems = new Collection<SvnLogEventArgs>();
                    svn.GetLog(path, out logItems);
                    foreach (var log in logItems)
                    {
                        foreach (SvnChangeItem svnChangeItem in (Collection<SvnChangeItem>)log.ChangedPaths)
                        {        //过滤版本
                            if ((svnChangeItem.Action.ToString() == action) && (log.Revision >= startVersion && log.Revision <= endVersion))
                            {
                                paths.Add(svnChangeItem.Path);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    operSVN.lastErrMsg = ex.Message;
                    StackFrame frame = new StackTrace(new StackFrame(true)).GetFrame(0);
                    operSVN.wlog(ex.Message, frame.GetFileName() + (object)"|" + (string)(object)frame.GetMethod() + "|" + (string)(object)frame.GetFileLineNumber() + "|" + (string)(object)frame.GetFileColumnNumber());
                    return null;
                }

            }
            return paths;
        }

    }
}
