using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Xunit;

namespace dotnet_etcd.tests
{
    public class kvClientTests:IDisposable
    {
        private Process _etcdProcess = null;
        EtcdClient _etcdClient;
        public kvClientTests()
        {
            var processStartInfo = new ProcessStartInfo(Path.Combine(AppContext.BaseDirectory, "etcd.exe"));
            processStartInfo.UseShellExecute = true;
            _etcdProcess = Process.Start(processStartInfo);
            Thread.Sleep(10 * 1000);
            _etcdClient = new EtcdClient("127.0.0.1", 2379);
        }

        
        [Fact]
        public void Test1()
        {
            _etcdClient.Put("foo/bar", "barfoo");
            Assert.Equal("barfoo", _etcdClient.GetVal("foo/bar"));
        }

        public void Dispose()
        {
            if (_etcdProcess != null && !_etcdProcess.HasExited)
            {
                _etcdProcess.Close();
                _etcdProcess = null;
            }
        }
        
    }
}
