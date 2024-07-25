using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Морской_Бой
{
    internal class Battleship
    {
        int[,] _gamefield = new int[10, 10]; //игровое поле
        int[,] _shiptype = new int[10, 10]; //тип корабля
        int _m1, _m2, _m3, _m4; //количество кораблей каждого типа
        Ship[] ship = new Ship[10]; //инфа о каждом корабле
        int _k;
        public Battleship() //конструктор
        {
            _m1 = 0;
            _m2 = 0;
            _m3 = 0;
            _m4 = 0;
            _k = 0;
            for (int i = 0; i < _gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < _gamefield.GetLength(1); j++)
                {
                    _gamefield[i, j] = 0;
                    _shiptype[i, j] = 0;
                }
                ship[i] = new Ship();
            }
        }
        public int[,] Gamefield { get { return _gamefield; } set { _gamefield = value; } }
        public int m1 { get { return _m1; } set { _m1 = value; } }
        public int m2 { get { return _m2; } set { _m2 = value; } }
        public int m3 { get { return _m3; } set { _m3 = value; } }
        public int m4 { get { return _m4; } set { _m4 = value; } }
        private void Changefield(int x, int y, int k)
        {
            for(int i = x-1; i <= x+1; i++)
                for(int j = y-1; j <= y+1; j++)
                {
                    if(i != -1 && j != -1 && i != 10 && j != 10 && _gamefield[i, j] != 3 && _gamefield[i, j] != 1)
                    {
                        _gamefield[i, j] = k;
                    }
                }
        }
        public int Addship(int x, int y, int mode, int location) //location 1 - горизонтально, 2 - вертикально
        {
            if (_gamefield[x, y] == 0)
            {
                switch (mode)
                {
                    case 1:
                        if (_m1 < 4)
                        {
                            _gamefield[x, y] = 1;
                            _shiptype[x, y] = 1;
                            Changefield(x, y, 4);
                            _m1++;
                            ship[_k].Add(x, y);
                            _k++;
                            return 1; //все ок
                        }
                        else return 3; //расставлено максимум кораблей данного типа
                    case 2:
                        if (_m2 < 3)
                        {
                            if (location == 1 && y + 1 != 10 && _gamefield[x, y + 1] != 4)
                            {       
                                for(int i = y; i <= y + 1; i++)
                                {
                                    _gamefield[x, i] = 1;
                                    _shiptype[x, i] = 2;
                                    Changefield(x, i, 4);
                                    ship[_k].Add(x, i);
                                }
                                _k++;
                            }
                            else 
                            if (location == 2 && x - 1 != -1 && _gamefield[x - 1, y] != 4)
                            {
                                for (int i = x - 1; i <= x; i++)
                                {
                                    _gamefield[i, y] = 1;
                                    _shiptype[i, y] = 2;
                                    Changefield(i, y, 4);
                                    ship[_k].Add(i, y);
                                }
                                _k++;
                            }
                            else return 4; //невозможно расставить
                            _m2++;
                            return 1; //все ок
                        }
                        else return 3; //расставлено максимум кораблей данного типа
                    case 3:
                        if (_m3 < 2)
                        {
                            if (location == 1 && y + 1 != 10 && y + 2 < 10 && _gamefield[x, y + 1] != 4 && _gamefield[x, y + 2] != 4)
                            {
                                for (int i = y; i <= y + 2; i++)
                                {
                                    _gamefield[x, i] = 1;
                                    _shiptype[x, i] = 3;
                                    Changefield(x, i, 4);
                                    ship[_k].Add(x, i);
                                }
                                _k++;
                            }
                            else
                            if (location == 2 && x - 1 != -1 && x - 2 > -1 && _gamefield[x - 1, y] != 4 && _gamefield[x - 2, y] != 4)
                            {
                                for (int i = x - 2; i <= x; i++)
                                {
                                    _gamefield[i, y] = 1;
                                    _shiptype[i, y] = 3;
                                    Changefield(i, y, 4);
                                    ship[_k].Add(i, y);
                                }
                                _k++;
                            }
                            else return 4; //невозможно расставить
                            _m3++;
                            return 1; //все ок
                        }
                        else return 3; //расставлено максимум кораблей данного типа
                    case 4:
                        if (_m4 < 1)
                        {
                            if (location == 1 && y + 1 != 10 && y + 2 < 10 && y + 3 < 10 && _gamefield[x, y + 1] != 4 && _gamefield[x, y + 2] != 4 && _gamefield[x, y + 3] != 4)
                            {
                                for (int i = y; i <= y + 3; i++)
                                {
                                    _gamefield[x, i] = 1;
                                    _shiptype[x, i] = 4;
                                    Changefield(x, i, 4);
                                    ship[_k].Add(x, i);
                                }
                                _k++;
                            }
                            else
                            if (location == 2 && x - 1 != -1 && x - 2 > -1 && x - 3 > -1 && _gamefield[x - 1, y] != 4 && _gamefield[x - 2, y] != 4 && _gamefield[x - 3, y] != 4)
                            {
                                for (int i = x - 3; i <= x; i++)
                                {
                                    _gamefield[i, y] = 1;
                                    _shiptype[i, y] = 4;
                                    Changefield(i, y, 4);
                                    ship[_k].Add(i, y);
                                }
                                _k++;
                            }
                            else return 4; //невозможно расставить
                            _m4++;
                            return 1; //все ок
                        }
                        else return 3; //расставлено максимум кораблей данного типа
                    default: return 4;
                }
            }
            else return 4;
        }
        public void NewGame()
        {
            _m1 = 0;
            _m2 = 0;
            _m3 = 0;
            _m4 = 0;
            _k = 0;
            for (int i = 0; i < _gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < _gamefield.GetLength(1); j++)
                {
                    _gamefield[i, j] = 0;
                    _shiptype[i, j] = 0;
                }
                ship[i] = new Ship();
            }
        }
        public bool Isdone()
        {
            if (_m1 == 4 && _m2 == 3 && _m3 == 2 && _m4 == 1) return true;
            else return false;
        }
        public bool IsEmpty(int x, int y)
        {
            int flag = 0;
            for(int i = 0; i < ship.Length; i++)
            {
                if (ship[i].Include(x, y)) ship[i].CountAdd();
            }
            for (int i = 0; i < ship.Length; i++)
            {
                if (_shiptype[x, y] == ship[i].Count && ship[i].Include(x, y)) flag = 1;
            }
            if (flag == 1) return true;
            else return false;
        }
        public void Empty(int x, int y)
        {
            Ship s = new Ship();
            for (int i = 0; i < ship.Length; i++)
            {
                if (ship[i].Include(x, y)) s = ship[i];
            }
            foreach (string t in s.Shipxy)
            {
                string[] xy = t.Split(' ');
                Changefield(Int32.Parse(xy[0]), Int32.Parse(xy[1]), 2);
            }
        }
        public bool GameOver()
        {
            int count = 0;
            for (int i = 0; i < _gamefield.GetLength(0); i++)
                for (int j = 0; j < _gamefield.GetLength(1); j++)
                    if (_gamefield[i, j] == 3) count++;
            if (count == 20) return true;
            else return false;
        }
    }
}
