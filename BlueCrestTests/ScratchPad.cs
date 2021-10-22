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
        private Request _request;
        [SetUp]
        public void Setup()
        {
            using var stream = new StreamReader("testResult.json");
            string content = stream.ReadToEnd();
            _request = JsonConvert.DeserializeObject<Request>(content);
        }

        [Test]
        public void ShouldDeserializeCorrectly()
        {
            Assert.IsNotNull(_request);

            List<string> allDimensions = new List<string>(_request.Dimensions.Rows.Select(x => x.DimensionId));
            double totalPnl = 0;

            foreach (string dimension in allDimensions)
            {
                Dictionary<string, int> columnKeys = new Dictionary<string, int>();

                List<string> cols = _request.Dimensions.Columns.Where(x =>
                    x.Equals(Constants.Fund) ||
                    x.Equals(Constants.Desk) ||
                    x.Equals(Constants.Strategy)).ToList();
                cols.Add("pnl");

                for (int i = 0; i < _request.Dimensions.Columns.Count(); i++)
                {
                    if (_request.Dimensions.Columns[i] == Constants.Fund) columnKeys.Add(Constants.Fund, i);
                    if (_request.Dimensions.Columns[i] == Constants.Desk) columnKeys.Add(Constants.Desk, i);
                    if (_request.Dimensions.Columns[i] == Constants.Strategy) columnKeys.Add(Constants.Strategy, i);
                }

                Assert.AreEqual(3, columnKeys.Count());
                Assert.AreEqual(4, cols.Count());

                List<string> colDataForDimension = new List<string>();
                var rows = _request.Dimensions.Rows.Where(x => x.DimensionId == dimension);

                foreach (Row row in rows)
                {
                    foreach (KeyValuePair<string, int> keyValuePair in columnKeys)
                    {
                        colDataForDimension.Add(row.Dimensions[keyValuePair.Value]);
                    }
                }

                Assert.AreEqual(3, colDataForDimension.Count());

                var rowMeasures = _request.Measures.Rows.Where(x => x.DimensionId == dimension);

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

                Assert.IsNotNull(_request);
            }
        }

        [Test]
        public void EnumerableRowMeasureItemToBindingObject()
        {
            var ret = _request.ToEnumerableOfMeasures().ToBindingObject(_request.RequestId);
            
            Assert.IsNotNull(ret);
        }

        [Test]
        public void NumberFormattingTests()
        {
            double d = 12345.678900;

            Console.WriteLine(String.Format(CultureInfo.CurrentCulture, "{0:N3}", d));
            
            Assert.Pass();
        }
        
        [Test]
        public void LinqTest()
        {
            var ret = _request.Dimensions.Rows.Join(_request.Measures.Rows, 
                row => row.DimensionId,
                rowMeasure => rowMeasure.DimensionId,
                (_, rowMeasure) => new RowMeasureItem
                {
                    DimensionId = rowMeasure.DimensionId,
                    Key = rowMeasure.Keys.First(),
                    Value = rowMeasure.Measures.First(),
                    Fund = _request.Dimensions.Rows.Where
                        ((row1, _) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[_request.Dimensions.KeyIndices[Constants.Fund]],
                    Strategy = _request.Dimensions.Rows.Where
                            ((row1, _) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[_request.Dimensions.KeyIndices[Constants.Strategy]],
                    Desk = _request.Dimensions.Rows.Where
                            ((row1, _) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[_request.Dimensions.KeyIndices[Constants.Desk]]
                });
            
            Assert.IsNotNull(ret);
        }
    }
}