﻿using TMPro;
using UnityEngine;

namespace Common
{
    public class Info : MonoBehaviour
    {
        public WorldDateTime worldDateTime;
        public ManufacturesManager manufacturesManager;

        public TextMeshProUGUI worldDateTimeText;
        public TextMeshProUGUI exchangeText;
        public TextMeshProUGUI taxOfficeText;
        public TextMeshProUGUI manufactureManagerText;

        private void Start()
        {
            worldDateTime.NewDay += WorldDateTimeNewDayHandler;
        }

        private void Update()
        {
            exchangeText.text = $"Sold Products: {Exchange.SoldProducts}\n" +
                                $"Sold Products Today: {Exchange.SoldProductsToday}\n" +
                                $"Vat:  {Exchange.Vat}\n" +
                                $"Vat Today:  {Exchange.VatToday}";

            taxOfficeText.text = $"Taxes: \n" +
                                 $"B: {TaxOffice.Bribes}, F: {TaxOffice.Fines}, T: {TaxOffice.Taxes}\n" +
                                 $"CB: {TaxOffice.CurrentDayBribes}, CF: {TaxOffice.CurrentDayFines}, CT: {TaxOffice.CurrentDayTaxes}\n";

            manufactureManagerText.text = $"Count reproduction dnk: {manufacturesManager.GetReproductionDnkCount()} ({manufacturesManager.ReproductionCount})";
        }

        private void WorldDateTimeNewDayHandler(object sender, Event.WorldDateTimeEventArgs e)
        {
            worldDateTimeText.text = $"Current Day: {e.Day}";
        }
    }
}