using ForzaTunerResults.DataModel;
using System.Collections.ObjectModel;

namespace ForzaTunerResults.Service
{
    public sealed class CarsService
    {
        private readonly Collection<Car> cars = [
            new(1, 1995, "Chevrolet", "Corvette ZR-1"),
            new(2, 1995, "Chevrolet", "Corvette ZR-1"),
            new(3, 1995, "Chevrolet", "Corvette ZR-1"),
            new(4, 1995, "Chevrolet", "Corvette ZR-1"),
            new(5, 1995, "Chevrolet", "Corvette ZR-1"),
            new(6, 1995, "Chevrolet", "Corvette ZR-1"),
            new(7, 1995, "Chevrolet", "Corvette ZR-1"),
            new(8, 1995, "Chevrolet", "Corvette ZR-1"),
            new(9, 1995, "Chevrolet", "Corvette ZR-1"),
            new(10, 1995, "Chevrolet", "Corvette ZR-1"),
            new(11, 1995, "Chevrolet", "Corvette ZR-1"),
            new(12, 1995, "Chevrolet", "Corvette ZR-1"),
            new(13, 1995, "Chevrolet", "Corvette ZR-1"),
            new(14, 1995, "Chevrolet", "Corvette ZR-1"),
            new(15, 1995, "Chevrolet", "Corvette ZR-1"),
        ];

        public Collection<Car> GetCars() => cars;

        public void AddCar(Car car)
        {
            cars.Add(car);
        }

    }
}
