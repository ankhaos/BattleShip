using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Морской_Бой
{
    internal class ComputerGame
    {
        int _location;
        List<List<int>> _coordinates;
        int _turn;
        int _x, _y;
        public ComputerGame(int location, List<List<int>> coordinates, int turn)
        {
            _location = location;
            _coordinates = coordinates;
            _turn = turn;
            _x = -1;
            _y = -1;
        }   
        public int Location { get { return _location; } set { _location = value; } }
        public List<List<int>> Coordinates { get { return _coordinates; } set { _coordinates = value; } }  
        public int Turn { get { return _turn; } set { _turn = value; } }    
        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } } 
        public void IsOk()
        {
            _location = 0;
            _coordinates.Clear();
            _turn = 0;
            _x = -1;
            _y = -1;
        }
    }
}
