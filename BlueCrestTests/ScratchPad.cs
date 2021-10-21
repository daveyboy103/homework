using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BlueCrestHomework.Extensions;
using BlueCrestHomework.Models;
using DataModel.Dtos;
using Newtonsoft.Json;
using NUnit.Framework;
using Request = DataModel.Dtos.Request;
using Row = DataModel.Dtos.Row;

namespace BlueCrestTests
{
    /// <summary>
    /// Test mainly used as a scratch pad for testing out theories not TDD,
    /// </summary>
    public class ScratchPad
    {
        private Request request;
        [SetUp]
        public void Setup()
        {
            using var stream = new StreamReader("testResult.json");
            string content = stream.ReadToEnd();
            request = JsonConvert.DeserializeObject<Request>(content);
        }

        [Test]
        public void ShouldDeserializeCorrectly()
        {
            Assert.IsNotNull(request);

            List<string> allDimensions = new List<string>(request.Dimensions.Rows.Select(x => x.DimensionId));
            double totalPnl = 0;

            foreach (string dimension in allDimensions)
            {
                Dictionary<string, int> columnKeys = new Dictionary<string, int>();

                List<string> cols = request.Dimensions.Columns.Where(x =>
                    x.Equals("fundreference") ||
                    x.Equals("desk") ||
                    x.Equals("strat")).ToList();
                cols.Add("pnl");

                for (int i = 0; i < request.Dimensions.Columns.Count(); i++)
                {
                    if (request.Dimensions.Columns[i] == "fundreference") columnKeys.Add("fundreference", i);
                    if (request.Dimensions.Columns[i] == "desk") columnKeys.Add("desk", i);
                    if (request.Dimensions.Columns[i] == "strat") columnKeys.Add("strat", i);
                }

                Assert.AreEqual(3, columnKeys.Count());
                Assert.AreEqual(4, cols.Count());

                List<string> colDataForDimension = new List<string>();
                var rows = request.Dimensions.Rows.Where(x => x.DimensionId == dimension);

                foreach (Row row in rows)
                {
                    foreach (KeyValuePair<string, int> keyValuePair in columnKeys)
                    {
                        colDataForDimension.Add(row.Dimensions[keyValuePair.Value]);
                    }
                }

                Assert.AreEqual(3, colDataForDimension.Count());

                var rowMeasures = request.Measures.Rows.Where(x => x.DimensionId == dimension);

                double cumulativePnl = 0;
                foreach (RowMeasure measure in rowMeasures)
                {
                    if (measure.Keys.Contains("pnl"))
                    {
                        cumulativePnl += measure.Measures.First();
                    }
                }

                totalPnl += cumulativePnl;

                Console.WriteLine($"{cols[0]}\t{cols[1]}\t{cols[2]}\t{cols[3]}");
                Console.WriteLine(
                    $"{colDataForDimension[0]}\t{colDataForDimension[1]}\t{colDataForDimension[2]}\t{cumulativePnl}");

                Console.WriteLine($"Total Pnl: {totalPnl}");

                RequestBinding binding = request.ToBindingObject();
                
                Assert.IsNotNull(binding);

            }
        }

        [Test]
        public void EnumerableRowMeasureItemToBindingObject()
        {
            var ret = request.ToEnumerableOfMeasures().ToBindingObject(request.RequestId);
            
            Assert.IsNotNull(ret);
        }

        [Test]
        public void NumberFormattingTests()
        {
            double d = 12345.678900;

            Console.WriteLine(String.Format(CultureInfo.CurrentCulture, "{0:N3}", d));
            
            Assert.Pass();
        }

        /// <summary>
        /// Just to demonstrate that I could wrestle with LINQ to get the data out in one statement.
        ///
        /// The key was adding the KeyIndices calculated property to the Dimensions class so that the index
        /// of the date for that column could be retrieved by string key when building the result set
        /// </summary>
        [Test]
        public void LinqTest()
        {
            var ret = request.Dimensions.Rows.Join(request.Measures.Rows, 
                row => row.DimensionId,
                rowMeasure => rowMeasure.DimensionId,
                (row, rowMeasure) => new RowMeasureItem
                {
                    DimensionId = rowMeasure.DimensionId,
                    Key = rowMeasure.Keys.First(),
                    Value = rowMeasure.Measures.First(),
                    Fund = request.Dimensions.Rows.Where
                        ((row1, i) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices["fundreference"]],
                    Strategy = request.Dimensions.Rows.Where
                            ((row1, i) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices["strat"]],
                    Desk = request.Dimensions.Rows.Where
                            ((row1, i) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices["desk"]]
                });
            
            Assert.IsNotNull(ret);
        }
    }
}