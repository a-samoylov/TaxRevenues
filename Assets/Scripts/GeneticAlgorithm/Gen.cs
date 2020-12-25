using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class Gen
    {
        private int _currentElementIndex = 0;
        private List<GenElement> _elements;

        public Gen(List<GenElement> elements)
        {
            _elements = elements;
        }

        public GenElement GetCurrentElement()
        {
            return _elements[_currentElementIndex];
        }

        public void Move(int steps)
        {
            //todo check this
            _currentElementIndex = (_currentElementIndex + steps) % Size();
        }

        public int Size()
        {
            return _elements.Count;
        }

        public override string ToString()
        {
            var result = "";

            var number = 1;
            foreach (var element in _elements)
            {
                result += $"{number}: {element.Name} Coefficient: {element.Coefficient}\n";
                number++;
            }

            return result;
        }
    }
}