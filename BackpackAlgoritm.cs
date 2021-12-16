using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackpackWPF
{
    public class BackpackAlgoritm
    {
        public BackpackAlgoritm(List<BackpackInfo> backpacks, int maxCapacity)
        {
            int size = backpacks.Count();

            _weight = new int[size] ;
            _values = new int[size] ;
            _listOfID = new List<int>();

            foreach (var item in backpacks)
            {
                _weight[item.ID] = item.Weight;
                _values[item.ID] = item.Price;
            }

            _maxCapcity = maxCapacity;
            
        }

        public int[] _weight;
        public int[] _values;
        private int _maxCapcity;
        private int[,] _arr;
        private List<int> _listOfID;

        public int GetMaxPrices()
        {
            var arr = new int[_weight.Length + 1, _maxCapcity + 1];

            for (int i = 0; i <= _weight.Length; i++)
            {
                for (int j = 0; j <= _maxCapcity; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        arr[i, j] = 0;
                    }
                    else
                    {
                        if (_weight[i - 1] > j)
                        {
                            arr[i, j] = arr[i - 1, j];
                        }
                        else
                        {
                            var prev = arr[i - 1, j];
                            var f = _values[i - 1] + arr[i - 1, j - _weight[i - 1]];
                            arr[i, j] = Math.Max(prev, f);
                        }

                    }
                }
            }
            _arr = arr;

            // КНП элемент
            return arr[_weight.Length, _maxCapcity];
        }

        private void Print(int i, int j)
        {
            if (_arr[i, j] == 0) return;

            if (_arr[i - 1, j] == _arr[i, j])
            {               
                //Print(i - 1, j);
            }
            else
            {
                Print(i - 1, j - _weight[i - 1]);
                //Console.Write(i + " ");
                _listOfID.Add(i);
            }
        }

        public List<int> GetListOfID()
        {
            Print(_values.Count(), _maxCapcity);

            return _listOfID;
        }
    }
}
