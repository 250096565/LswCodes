using SharpSvn;
using svnOper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Configuration;
using System.Collections;
namespace OSSWinForm
{
    public class Manager
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
        /// <summary>zz
        /// 
        /// 获取某个版本之间的上传静态文件,并备份后上传本地的文件
        /// </summary>
        /// <returns></returns>
        public static OutputModel GetStaticFile(string startNum, string endNum, string localPath, string serviceUrl)
        {
            OutputModel output = new OutputModel();
            if (string.IsNullOrEmpty(startNum) || string.IsNullOrEmpty(endNum))
            {
                output.Status = "-1";
                output.Message = "请指定版本号";
                return output;
            }
            if (string.IsNullOrEmpty(localPath) || string.IsNullOrEmpty(serviceUrl))
            {
                output.Status = "-1";
                output.Message = "缺少参数";
                return output;
            }
            try
            {
                //如果需要添加静态文件格式,可以在appConfig添加文件格式

                IDictionary formats = (IDictionary)ConfigurationManager.GetSection("formats");
                List<string> lformats = new List<string>();
                foreach (DictionaryEntry temp in formats)
                {
                    lformats.Add(temp.Value.ToString());
                }
                Dictionary<string, string> logaction = new Dictionary<string, string>();
                Dictionary<string, int> paths = GetLogFPath(localPath, Convert.ToInt32(startNum), Convert.ToInt32(endNum), lformats, out logaction);
                if (paths == null)
                {
                    output.Status = "-1";
                    output.Message = "搜索错误: " + operSVN.lastErrMsg;
                    return output;
                }
                if (paths.Count <= 0)
                {
                    output.Status = "-1";
                    output.Message = "没有需要上传的文件";
                    return output;
                }
                output.Data = paths;
                //检测是否已经下载,默认地址为E盘下
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择备份文件路径";
                string filePath = null;
                string[] localpaths = localPath.Split('\\');
                string endlo = localpaths[localpaths.Length - 1];
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.SelectedPath + "/" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + endlo + "备份/";
                    MessageBox.Show("已选择文件夹:" + filePath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    filePath = @"d:/OSSBackup/" + DateTime.Now.Year + "/" + DateTime.Now.Month + DateTime.Now.Day + "/" + endlo + "/";
                    MessageBox.Show("未选择文件夹,默认放到D盘下OSSBackup文件夹下");

                }

                StringBuilder sb = new StringBuilder();
                using (WebClient webClient = new WebClient())
                {

                    foreach (var temp in paths)
                    {

                        try
                        {
                            if (!logaction[temp.Key].Equals("Add", StringComparison.OrdinalIgnoreCase))
                            {
                                sb.Append("/" + temp.Key.Substring(temp.Key.IndexOf(endlo) + (endlo.Length + 1)));

                                //备份
                                string file = "http://shianyun-oss.oss-cn-beijing.aliyuncs.com/" + serviceUrl + sb.ToString();
                                string[] files = file.Split('/');
                                string endpath = (Path.GetDirectoryName(sb.ToString()) + "/").TrimStart('\\');
                                if (!Directory.Exists(filePath + endpath))
                                {
                                    Directory.CreateDirectory(filePath + endpath);
                                }

                                webClient.DownloadFile(file, filePath + endpath + files[files.Length - 1]);
                                sb.Remove(0, sb.Length);
                            }

                        }
                        catch
                        {
                            sb.Remove(0, sb.Length);
                            continue;

                        }

                    }

                }


                foreach (var temp in paths)
                {

                    sb.Append("/" + temp.Key.Substring(temp.Key.IndexOf(endlo) + (endlo.Length + 1)));
                    //上传
                    //
                    string urlpath = serviceUrl + sb.ToString();
                    string readerpath = localPath + @"/" + sb.ToString();
                    StreamReader stream = new StreamReader(readerpath);
                    if (Upload.UploadFile(stream.BaseStream, urlpath, Path.GetExtension(readerpath)) != "1")
                    {
                        output.Status = "-1";
                        output.Message = "上传文件出错";
                        return output;
                    }
                    sb.Remove(0, sb.Length);

                }
                output.Status = "1";
                output.Message = "成功";



            }
            catch (Exception ex)
            {
                output.Status = "-1";
                output.Message = ex.Message;
            }


            return output;

        }


        /// <summary>
        /// 获取日志上改变的文件路径
        /// </summary>
        /// <param name="path">本地路径</param>
        /// <param name="startVersion">开始版本</param>
        /// <param name="endVersion">结束版本</param>
        /// <returns></returns>
        public static Dictionary<string, int> GetLogFPath(string path, int startVersion, int endVersion, List<string> formats, out Dictionary<string, string> logaction)
        {
            logaction = new Dictionary<string, string>();
            Dictionary<string, int> paths = new Dictionary<string, int>();
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
                            if ((log.Revision >= startVersion && log.Revision <= endVersion))
                            {
                                foreach (var temp in formats)
                                {

                                    if (Path.GetExtension(svnChangeItem.Path.ToLower()).Contains(temp.ToLower()))
                                    {
                                        if (paths.ContainsKey(svnChangeItem.Path))
                                        {
                                            if (paths[svnChangeItem.Path] < (int)log.Revision)
                                            {
                                                paths.Remove(svnChangeItem.Path);
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                        else if (svnChangeItem.Action.ToString().Equals("delete",StringComparison.OrdinalIgnoreCase))
                                        {
                                            continue;
                                        }
                                        paths.Add(svnChangeItem.Path, (int)log.Revision);
                                        logaction.Add(svnChangeItem.Path, svnChangeItem.Action.ToString());
                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    operSVN.lastErrMsg = ex.Message;
                    StackFrame frame = new StackTrace(new StackFrame(true)).GetFrame(0);
                    operSVN.wlog(ex.Message, frame.GetFileName() + "|" + frame.GetMethod() + "|" + frame.GetFileLineNumber() + "|" + frame.GetFileColumnNumber() + "|");
                    return null;
                }

            }
            return paths;
        }


        public static OutputModel GetLofInfo(string localPath)
        {
            OutputModel output = new OutputModel();
            List<String> result = new List<string>();
            using (SvnClient svn = new SvnClient())
            {
                try
                {
                    //拿到所有的日志
                    Collection<SvnLogEventArgs> logItems = new Collection<SvnLogEventArgs>();
                    svn.GetLog(localPath, out logItems);
                    result.Add(logItems[0].LogMessage);
                    result.Add(logItems[0].Revision.ToString());
                    output.Status = "1";
                    output.Data = result;

                }
                catch (Exception ex)
                {
                    operSVN.lastErrMsg = ex.Message;
                    StackFrame frame = new StackTrace(new StackFrame(true)).GetFrame(0);
                    operSVN.wlog(ex.Message, frame.GetFileName() + "|" + frame.GetMethod() + "|" + frame.GetFileLineNumber() + "|" + frame.GetFileColumnNumber() + "|");
                    output.Status = "-1";
                    output.Message = ex.Message;
                    return output;
                }

                return output;
            }
        }
    }


}
