using RentalData;
using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalServices
{
    public class GuitarService : IGuitar
    {
        private RentalContext _context;

        public GuitarService(RentalContext context)
        {
            _context = context;
        }

        public void Add(Guitar newGuitar)
        {
            _context.Add(newGuitar);
            _context.SaveChanges();
        }

        public Guitar Get(int id)
        {
            return _context.Guitars
                .FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Guitar> GetAllGuitars()
        {
            return _context.Guitars;
        }

        public IEnumerable<Guitar> GetByNumberStrings(int strings)
        {
            return GetAllGuitars()
                .Where(g => g.NumberOfStrings == strings);
        }

        public IEnumerable<Guitar> GetByNumberStrings(int low, int high)
        {
            return GetAllGuitars()
                .Where(g => g.NumberOfStrings >= low && g.NumberOfStrings <= high);
        }

        public IEnumerable<Guitar> GetByStyle(string style)
        {
            return GetAllGuitars()
                .Where(g => g.Style == style);
        }

        public IEnumerable<Guitar> GetByType(string type)
        {
            return GetAllGuitars()
                .Where(g => g.Type == type);
        }

        public int GetNumberStrings(int id)
        {
            return Get(id).NumberOfStrings;
        }

        public string GetStyle(int id)
        {
            return Get(id).Style;
        }

        public string GetType(int id)
        {
            return Get(id).Type;
        }
    }
}
