using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_Algorithm;

namespace TDD_Algorithm
{
    // Enum ElectricityContractType määrittelee sähkösopimuksen tyypit: Kiinteä hinta tai Pörssisähkö.
    public enum ElectricityContractType
    {
        FixedPrice,
        MarketPrice
    }

    // Luokka Comparer sisältää kaksi metodia, ComparePrices ja DeserializeJson, sähkön hintojen vertailuun ja JSON-deserialisointiin.
    public class Comparer
    {
        public List<PriceDifference> ComparePrices(List<ElectricityPrice> exchangePrices, decimal fixedPrice)
        {
            List<PriceDifference> differences = new List<PriceDifference>();

            foreach (var exchangePrice in exchangePrices)
            {
                decimal differenceValue = Math.Abs((decimal)exchangePrice.Price - fixedPrice);
                //Määritellään kumpi sähkösopimus on halvempi.
                ElectricityContractType cheaperContract = exchangePrice.Price < fixedPrice ? ElectricityContractType.MarketPrice : ElectricityContractType.FixedPrice;
                //Lisätään uusi olio "PriceDifference" listaan.
                differences.Add(new PriceDifference(differenceValue, exchangePrice.StartDate, exchangePrice.EndDate, cheaperContract));
            }

            return differences;
        }

        public List<ElectricityPrice> DeserializeJson(string jsonData)
        {
            var jsonInput = JsonConvert.DeserializeObject<Dictionary<string, List<ElectricityPrice>>>(jsonData);
            return jsonInput["prices"];
        }
    }
}
