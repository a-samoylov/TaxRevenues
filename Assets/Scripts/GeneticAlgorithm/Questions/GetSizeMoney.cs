namespace GeneticAlgorithm.Questions
{
    public class GetSizeMoney : AbstractQuestion
    {
        private enum Result
        {
            Small = 1,
            Medium = 2,
            Big = 3
        }

        private enum Coefficient
        {
            // small: 0-2000,  medium: 2000-10000,   big: >10000
            SmallScale = 0,

            // small: 0-10000,  medium: 10000-50000,  big: >50000
            MediumScale = 1,

            // small: 0-50000, medium: 50000-500000, big: >500000
            BigScale = 2
        }

        public override string GetName()
        {
            return "get_size_money";
        }

        public override int GetMinCoefficient()
        {
            return (int) Coefficient.SmallScale;
        }

        public override int GetMaxCoefficient()
        {
            return (int) Coefficient.BigScale;
        }

        public override int Process(Manufacture manufacture, GenElement genElement)
        {
            var money = manufacture.Money;
            
            switch (genElement.Coefficient)
            {
                case (int) Coefficient.SmallScale:
                {
                    if (money <= 2000)
                    {
                        return (int) Result.Small;
                    }
                    else if (money > 2000 && money <= 10000)
                    {
                        return (int) Result.Medium;
                    }
                    
                    break;
                }
                case (int) Coefficient.MediumScale:
                {
                    if (money <= 10000)
                    {
                        return (int) Result.Small;
                    }
                    else if (money > 10000 && money <= 50000)
                    {
                        return (int) Result.Medium;
                    }
                    
                    break;
                }
                case (int) Coefficient.BigScale:
                {
                    if (money <= 50000)
                    {
                        return (int) Result.Small;
                    }
                    else if (money > 50000 && money <= 500000)
                    {
                        return (int) Result.Medium;
                    }
                    
                    break;
                }
            }

            return (int) Result.Big;
        }
    }
}