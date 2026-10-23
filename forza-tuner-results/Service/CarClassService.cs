using ForzaTunerResults.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class CarClassService
    {

        public Collection<CarClass> GetCarClasses() => new()
        {
                new(1, "D", 400),
                new(2, "C", 500),
                new(3, "B", 600),
                new(4, "A", 700),
                new(5, "S1", 800),
                new(6, "S2", 900),
                new(7, "R", 998),
                new(8, "X", 999)
            };

    }
}
