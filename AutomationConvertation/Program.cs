using System;
using System.IO;

namespace AutomationConvertation
{
    class Program
    {
        private const string InputFile = "../../../input.txt";
        private const string OutputFile = "../../../output.txt";
        
        static void Main(string[] args)
        {
            StreamReader inputStream = new StreamReader(InputFile);
            StreamWriter outputStream = new StreamWriter(OutputFile);
            string machineType = inputStream.ReadLine();
            if (machineType == MachineType.Mealy)
            {
                MealyMachineConverter mealyMachineConverter = new MealyMachineConverter(inputStream, outputStream);
                mealyMachineConverter.ReadMealyMachine();
                mealyMachineConverter.PrintMooreMapContainer();
            }
            else if (machineType == MachineType.Moore)
            {
                MooreMachineConverter mooreMachineConverter = new MooreMachineConverter(inputStream, outputStream);
                mooreMachineConverter.ReadMooreMachine();
                mooreMachineConverter.PrintMealyMapContainer();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Автомат не распознан");
            }
        }
    }
}
