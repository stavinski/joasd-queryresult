using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Joasd.QueryResult.Library;
using System.Diagnostics;

namespace Joasd.QueryResult.Test
{
    [TestClass]
    public class QueryResultTests
    {
        [TestMethod]
        public void HasResultIsFalseWhenProvidedNullResult()
        {
            var sut = QueryResult<Object>.Create((Object)null);
            Assert.IsFalse(sut.HasResult);
        }

        [TestMethod]
        public void HasResultIsTrueWhenProvidedResult()
        {
            var sut = QueryResult<Object>.Create(new Object());
            Assert.IsTrue(sut.HasResult);
        }

        [TestMethod]
        public void ResultIsNullWhenProvidedNullResult()
        {
            var sut = QueryResult<Object>.Create((Object)null);
            Assert.IsNull(sut.Result);
        }

        [TestMethod]
        public void ResultIsNotNullWhenProvidedResult()
        {
            var sut = QueryResult<Object>.Create(new Object());
            Assert.IsNotNull(sut.Result);
        }

        [TestMethod]
        public void ShouldExecuteFoundWhenProvidedResult()
        {
            bool foundExecuted = false;

            var sut = QueryResult<Object>.Create(new Object());
            sut.Found(x => foundExecuted = true);

            Assert.IsTrue(foundExecuted);
        }

        [TestMethod]
        public void ShouldUseCorrectResultWhenExecutingFound()
        {
            var result = new Object();
            var sut = QueryResult<Object>.Create(result);
            sut.Found(x => Assert.AreSame(result, x));
        }

        [TestMethod]
        public void ShouldNotExecuteFoundWhenProvidedNullResult()
        {
            bool foundExecuted = false;

            var sut = QueryResult<Object>.Create((Object)null);
            sut.Found(x => foundExecuted = true);

            Assert.IsFalse(foundExecuted);
        }

        [TestMethod]
        public void ShouldExecuteMissingFoundWhenProvidedNullResult()
        {
            bool missingExecuted = false;

            var sut = QueryResult<Object>.Create((Object)null);
            sut.Missing(() => missingExecuted = true);

            Assert.IsTrue(missingExecuted);
        }

        [TestMethod]
        public void ShouldNotExecuteMissingdWhenProvidedResult()
        {
            bool missingExecuted = false;

            var sut = QueryResult<Object>.Create(new Object());
            sut.Missing(() => missingExecuted = true);

            Assert.IsFalse(missingExecuted);
        }

        [TestMethod]
        public void ImperativeUsage()
        {
            var repository = new ThingamajigRepository();
            var queryResult = repository.Get();

            if (queryResult.HasResult)
            {
                Debug.WriteLine("found a thingamajig named: {0}", queryResult.Result.Name);
            }
            else
            {
                Debug.WriteLine("no thingamajig found");
            }
        }

        [TestMethod]
        public void FunctionalUsage()
        {
            var repository = new ThingamajigRepository();
            var queryResult = repository.Get();

            queryResult.Found(thingamajig => Debug.WriteLine("found a thingamajig named: {0}", thingamajig));
            queryResult.Missing(() => Debug.WriteLine("no thingamajig found"));
        }

    }
}
