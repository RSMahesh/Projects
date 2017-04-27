using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using System.Net;


namespace StatusMaker.Business.Test
{
    [TestFixture]
    [Category("Integration ")]
    public class HttpEngineFixture
    {
        private string url = "http://md5.jsontest.com/?text=example_text";

        private string authorization = "";
        [Test]
        public void CreateInstance()
        {
            var sut = new HttpEngine();
            Assert.IsNotNull(sut);
        }

        [Test]
        public void ShouldHaveGetHttpResponseMethod()
        {
            var sut = new HttpEngine();
            MethodInfo methodInfo = sut.GetType().GetMethod("GetHttpResponseAsync");
            Assert.IsNotNull(methodInfo);
        }

        [Test]
        public void ShouldTakeUrlAsArument()
        {
            var sut = new HttpEngine();
            sut.Authorization = authorization;
            var ff = sut.GetHttpResponseAsync(url ).Result;
        }

        public void ShouldTakeAuthorizationAsAgrument()
        {
            var sut = new HttpEngine();
            sut.Authorization = authorization;
            var ff = sut.GetHttpResponseAsync(url).Result;
        }

        [Test]
        public void ShouldRetunDictionaryWithValues()
        {
            var sut = new HttpEngine();
            sut.Authorization = authorization;
            var dic = sut.GetHttpResponseAsync(url).Result;
            Assert.IsTrue(dic.Keys.Count > 0);
        }

        [Test]
        public void ShouldPassAutorizationAsHeader()
        {
            var sut = new HttpEngine();
            sut.Authorization = authorization;
            var dic = (Dictionary<string, object>)sut.GetHttpResponseAsync("http://headers.jsontest.com/").Result;
            Assert.IsNotNull(dic["User-Agent"]);
        }

        [Test]
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task ShouldThowExcetionInCaseUrliSInavlid()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            
            Assert.ThrowsAsync<WebException>(
                async () =>
                    {
                            await
                            new HttpEngine().GetHttpResponseAsync("http://headers.jsontest.comsas/");
                    });
        }
    }
}
