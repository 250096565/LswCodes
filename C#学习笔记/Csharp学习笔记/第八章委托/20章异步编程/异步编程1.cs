using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 第八章委托._20章异步编程
{
    class 异步编程1
    {
        private delegate string AsyncMethodCaller(string fileUrl);
        public void Main()
        {
            AsyncMethodCaller methodCaller = new AsyncMethodCaller(DownLoadFile);
            methodCaller.BeginInvoke("aaa", GetResult, "ok");
        }


        public string DownLoadFile(string url)
        {
            int buffer = 2048;
            byte[] buffReader = new byte[buffer];
            string savePath = "C/aaa.jpg";

            FileStream stream = null;
            HttpWebResponse myWebResponse = null;

            try
            {
                stream = new FileStream(savePath, FileMode.Create);
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
                {
                    myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                    Stream responStream = myWebRequest.GetRequestStream();
                    int readSize = responStream.Read(buffReader, 0, buffer);
                    while (readSize > 0)
                    {
                        stream.Write(buffReader, 0, readSize);
                        readSize = responStream.Read(buffReader, 0, buffer);
                    }

                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
                if (myWebResponse != null)
                {
                    myWebResponse.Dispose();
                }
            }
            return "ok";
       }

        private void GetResult(IAsyncResult result)
        {
            AsyncMethodCaller caller = (AsyncMethodCaller) ((IAsyncResult) result).AsyncState;
            string resultstring = caller.EndInvoke(result);
        }

    }
}
