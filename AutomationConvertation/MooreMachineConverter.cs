using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutomationConvertation
{
    public class MooreMachineConverter: MachineConverter
    {
        public MooreMachineConverter(StreamReader inputStream, StreamWriter outputStream) : base(inputStream, outputStream)
        {
        }
        
        public void ReadMooreMachine()
        {
            List<string> outputStateSignals = _inputStream.ReadLine().Substring(2).Split(" ").ToList();
            List<string> statesOrders = new List<string>();
            int i = 0;
            List<string> states = _inputStream.ReadLine().Substring(2).Split(" ").ToList();
            foreach (string s in states)
            {
                int state = Convert.ToInt32(s);
                if (!_equalClassesStates.ContainsKey(Convert.ToInt32(outputStateSignals[i])))
                {
                    _equalClassesStates.Add(Convert.ToInt32(outputStateSignals[i]), new List<int>());
                }

                _equalClassesStates[Convert.ToInt32(outputStateSignals[i])].Add(state);
                statesOrders.Add(s);
                ++i;
            }

            string inputLine;
            while ((inputLine = _inputStream.ReadLine()) != null)
            {
                List<string> inputLineList = inputLine.Split(" ").ToList();
                int inputSignal = Convert.ToInt32(inputLineList.First());
                i = 1;
                foreach (string state in statesOrders)
                {
                    int newState = Convert.ToInt32(inputLineList[i]);
                    if (!_sourceStatesToTransitions.ContainsKey(Convert.ToInt32(state)))
                    {
                        _sourceStatesToTransitions.Add(Convert.ToInt32(state), new Dictionary<int, int>());
                    }
                    _sourceStatesToTransitions[Convert.ToInt32(state)].Add(inputSignal, newState);
                    if (!_inputAndOutputStatesSignals.ContainsKey(Convert.ToInt32(state)))
                    {
                        _inputAndOutputStatesSignals.Add(Convert.ToInt32(state), new Dictionary<int, int>());
                    }

                    _inputAndOutputStatesSignals[Convert.ToInt32(state)]
                        .Add(inputSignal, Convert.ToInt32(outputStateSignals[i - 1]));
                    ++i;
                }
            }
        }
        
        public void PrintMealyMapContainer()
        {
            _outputStream.Write("  ");
            foreach (int key in _sourceStatesToTransitions.Keys)
            {
                _outputStream.Write($"{key}   ");
            }
            _outputStream.WriteLine();
            int i = 1;
            int sourceState = 1;
            while (i <= _sourceStatesToTransitions[1].Count)
            {
                _outputStream.Write($"{i} ");
                foreach (Dictionary<int, int> states in _sourceStatesToTransitions.Values)
                {
                    int state = states[i];
                    int outputSignal = _inputAndOutputStatesSignals[sourceState][i];
                    _outputStream.Write($"{state}/{outputSignal} ");
                    sourceState++;
                }
                i++;
                sourceState = 1;
                _outputStream.WriteLine();
            }
            _outputStream.Close();
        }
    }
}