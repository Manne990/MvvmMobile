using System;
namespace MvvmMobile.Sample.Core.Model
{
    public class Motorcycle : IMotorcycle
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{Brand} {Model} ({Year})";
        }
    }
}