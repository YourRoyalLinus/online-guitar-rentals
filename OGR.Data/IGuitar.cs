using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalData
{
    public interface IGuitar
    {
        IEnumerable<Guitar> GetAllGuitars();
        IEnumerable<Guitar> GetByType(string type);

        IEnumerable<Guitar> GetByStyle(string style);

        IEnumerable<Guitar> GetByNumberStrings(int strings);

        IEnumerable<Guitar> GetByNumberStrings(int low, int high);

        Guitar Get(int id);

        string GetType(int id);

        string GetStyle(int id);

        int GetNumberStrings(int id);
        void Add(Guitar newGuitar);
    }
}
