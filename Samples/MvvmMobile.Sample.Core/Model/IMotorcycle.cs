using System;
namespace MvvmMobile.Sample.Core.Model
{
    public interface IMotorcycle
    {
        Guid Id { get; set; }
        string Brand { get; set; }
        string Model { get; set; }
        int Year { get; set; }
    }
}