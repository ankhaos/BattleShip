using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Морской_Бой
{
    internal class Ship
    {
        int _count;
        List<string> _shipxy;
        public Ship()
        {
            _count = 0;
            _shipxy = new List<string>();
        }
        public int Count { get { return _count; } set { _count = value; } }
        public List<string> Shipxy { get { return _shipxy; } set { _shipxy = value; } }
        public void Add(int x, int y)
        {
            _shipxy.Add(x.ToString() + ' ' + y.ToString());
        }
        public void CountAdd()
        {
            _count++;
        }
        public bool Include(int x, int y)
        {
            if (_shipxy.Contains(x.ToString() + ' ' + y.ToString())) return true;
            else return false;
        }
    }
}
