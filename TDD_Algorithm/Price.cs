using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_Algorithm;

namespace TDD_Algorithm
{
    //Luokka ElectricityPrice, jossa on määritelty hinta, alkamispvm ja loppumispvm.
    public class ElectricityPrice
    {
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    //Luokka, jossa on kahden (pörssisähkö ja kiinteä sähkö) välinen ero tietyllä ajanjaksolla.
    public class PriceDifference
    {
        public decimal PriceDifferenceValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ElectricityContractType CheaperContract { get; set; }

        // Konstruktori PriceDifference-luokalle ottaa vastaan hintaeron, ajanjakson alkamis- ja päättymispäivämäärät, sekä edullisemman sähkösopimuksen tyypin.
        public PriceDifference(decimal priceDifference, DateTime startDate, DateTime endDate, ElectricityContractType cheaperContract)
        {
            PriceDifferenceValue = priceDifference;
            StartDate = startDate;
            EndDate = endDate;
            CheaperContract = cheaperContract;
        }
    }
}
