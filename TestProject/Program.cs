using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessStuff.Models;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new MaleModel();
            model.UOFM = UnitOfMeasureEnum.Imperial;
            model.Weight = 330;
            model.Height = 72;
            model.AgeInYears = 31;
            model.WaistLength = 56;
            

            


            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
