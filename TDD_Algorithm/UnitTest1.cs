using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;
using System.Collections;
using TDD_Algorithm;

namespace TDD_Algorithm
{
    //Testiluokassa on yksikkötestejä sähkösopimusten hintojen vertailuun.
    public class Tests
    {
        private readonly ITestOutputHelper output;

        public Tests(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        //Yksikkötesti, joka vertailee sähkösopimusten hintoja. ///Fact-attribuuttia käytetään yksittäisen testin määrittelemiseen (xUnit)!\\\
        [Fact]
        public void ComparePrices_ShouldReturnCorrectDifferences()
        {
            //Olio, joka suorittaa hintojen vertailun ja JSON-deserialisoinnin.
            var comparer = new Comparer();

            //JSON-data pörssisähkön hinnoista.
            string jsonInput = @"
            {
                ""prices"": [
                    {
                        ""price"": 16.243,
                        ""startDate"": ""2022-11-14T22:00:00.000Z"",
                        ""endDate"": ""2022-11-14T23:00:00.000Z""
                    },
                    {
                        ""price"": 6.835,
                        ""startDate"": ""2022-11-14T21:00:00.000Z"",
                        ""endDate"": ""2022-11-14T22:00:00.000Z""
                    },
                    {
                        ""price"": 0.523,
                        ""startDate"": ""2022-11-14T20:00:00.000Z"",
                        ""endDate"": ""2022-11-14T21:00:00.000Z""
                    }
                ]
            }";

            List<ElectricityPrice> exchangePrices = comparer.DeserializeJson(jsonInput);

            //Kiinteän sähkön hinta.
            decimal fixedPrice = 12.0M;

            //Hintojen vertailu:
            List<PriceDifference> differences = comparer.ComparePrices(exchangePrices, fixedPrice);
            
            //Assert eli "Varmista". Tässä vaiheessa testin tulos tarkistetaan, että testattava koodi toimii odotetusti!
            Assert.Equal(3, differences.Count);

            Assert.Equal(new DateTime(2022, 11, 14, 22, 0, 0), differences[0].StartDate);
            Assert.Equal(new DateTime(2022, 11, 14, 23, 0, 0), differences[0].EndDate);
            Assert.Equal(4.243m, differences[0].PriceDifferenceValue, 3);
            Assert.Equal(ElectricityContractType.FixedPrice, differences[0].CheaperContract);

            Assert.Equal(new DateTime(2022, 11, 14, 21, 0, 0), differences[1].StartDate);
            Assert.Equal(new DateTime(2022, 11, 14, 22, 0, 0), differences[1].EndDate);
            Assert.Equal(5.165m, differences[1].PriceDifferenceValue, 3);
            Assert.Equal(ElectricityContractType.MarketPrice, differences[1].CheaperContract);

            Assert.Equal(new DateTime(2022, 11, 14, 20, 0, 0), differences[2].StartDate);
            Assert.Equal(new DateTime(2022, 11, 14, 21, 0, 0), differences[2].EndDate);
            Assert.Equal(11.477m, differences[2].PriceDifferenceValue, 3);
            Assert.Equal(ElectricityContractType.MarketPrice, differences[2].CheaperContract);

            LogResults(differences);
        }

        private void LogResults(List<PriceDifference> differences)
        {
            output.WriteLine("Price Differences:");

            foreach (var difference in differences)
            {
                output.WriteLine($"Start Date: {difference.StartDate}, End Date: {difference.EndDate}");
                output.WriteLine($"Price Difference Value: {difference.PriceDifferenceValue}");
                output.WriteLine($"Cheaper Contract: {difference.CheaperContract}");
            }
        }
    }
}