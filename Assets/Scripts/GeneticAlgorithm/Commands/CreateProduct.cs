namespace GeneticAlgorithm.Commands
{
    internal enum CommandResult
    {
        ProductCantBeCreated = 1,
        ProductCantBeSold = 2,
        ProductCreated = 3
    }
    
    internal enum CommandCoefficient
    {
        PayTaxes = 1,
        PayFines = 2,
        PayBribes = 3
    }

    public class CreateProduct : AbstractCommand
    {
        private TaxOffice _taxOffice = new TaxOffice();

        public override string GetName()
        {
            return "create_product";
        }

        public override int GetMinCoefficient()
        {
            return (int) CommandCoefficient.PayTaxes;
        }

        public override int GetMaxCoefficient()
        {
            return (int) CommandCoefficient.PayBribes;
        }

        public override int Process(Manufacture manufacture, GenElement genElement)
        {
            if (!manufacture.IsPossibleCreateProduct())
            {
                return (int) CommandResult.ProductCantBeCreated;
            }
            
            if (!Exchange.IsPossibleSell())
            {
                return (int) CommandResult.ProductCantBeSold;
            }

            var product = manufacture.CreateProduct();

            if (genElement.Coefficient == (int) CommandCoefficient.PayTaxes)
            {
                manufacture.PayTaxes(product);

                return (int) CommandResult.ProductCreated;
            }

            if (_taxOffice.IsNeedPayFines(manufacture, product))
            {
                if (genElement.Coefficient == (int) CommandCoefficient.PayFines)
                {
                    manufacture.PayFines(product);

                    return (int) CommandResult.ProductCreated;
                }

                if (_taxOffice.IsCouldPayBribe(manufacture, product))
                {
                    manufacture.PayBribe(product);
                }
                else
                {
                    manufacture.PayFines(product);
                }
            }

            return (int) CommandResult.ProductCreated;
        }
    }
}