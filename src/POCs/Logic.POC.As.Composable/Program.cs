using Logic.POC.As.Composable.Components;
using System;

namespace Logic.POC.As.Composable
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            UseComposableLogic();

            Console.WriteLine("Done! with the composable logic");

            UseStandardTailoredLogic();

            Console.WriteLine("Done! with the standard logic");

            Console.ReadLine();
        }

        private static void UseComposableLogic()
        {
            var fac = new UoWFac();
            ComposableOperatorLogic logic = new ComposableOperatorLogic(fac);

            var getComp = new GetComponent(logic);
            var updComp = new UpdateComponent(logic);

            var oper = getComp.Execute(16);

            var rnd = new Random().Next(800, 1000);
            oper.FirstName = $"jp cito {rnd}";
            oper.Document = rnd.ToString();

            updComp.Execute(oper);
        }

        private static void UseStandardTailoredLogic()
        {
            var fac = new UoWFac();
            var logic = new StandardOperatorLogic(fac);

            var oper = logic.GetById(19);

            var rnd = new Random().Next(800, 1000);
            oper.FirstName = $"jp zote {rnd}";
            oper.Document = rnd.ToString();

            logic.Update(oper);
        }
    }
}