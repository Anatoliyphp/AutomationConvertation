using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutomationConvertation
{
    public class MealyMachineConverter: MachineConverter
    {
        public MealyMachineConverter(StreamReader inputStream, StreamWriter outputStream) : base(inputStream, outputStream)
        {
        }
        
        public void ReadMealyMachine()
        {
            string statesString = _inputStream.ReadLine().Substring(3);
            //statesString.Insert(0, " ");
            List<string> states = statesString.Split("   ").ToList();
            string inputLine;
            while ((inputLine = _inputStream.ReadLine()) != null)
            {
                List<string> inputLineList = inputLine.Split(" ").ToList();
                int inputSignal = Convert.ToInt32(inputLineList.First());
                int i = 1;
                foreach (string state in states)
                {
                    int slashPosition = inputLineList[i].IndexOf('/');
                    int transitionState = Convert.ToInt32(inputLineList[i].Substring(0, slashPosition));
                    int outputSignal = Convert.ToInt32(inputLineList[i].Substring(slashPosition + 1, inputLineList[i].Length - slashPosition - 1));
                    if (!_sourceStatesToTransitions.ContainsKey(Convert.ToInt32(state)))
                    {
                        _sourceStatesToTransitions.Add(Convert.ToInt32(state), new Dictionary<int, int>());
                    }
                    _sourceStatesToTransitions[Convert.ToInt32(state)][inputSignal] = transitionState;
                    if (!_inputAndOutputStatesSignals.ContainsKey(Convert.ToInt32(state)))
                    {
                        _inputAndOutputStatesSignals.Add(Convert.ToInt32(state), new Dictionary<int, int>());
                    }
                    _inputAndOutputStatesSignals[Convert.ToInt32(state)][inputSignal] = outputSignal;
                    i++;
                }
            }
            foreach (KeyValuePair<int, Dictionary<int,int>> outputSignals in _inputAndOutputStatesSignals)
            {
                if (!_equalClassesStates.ContainsKey(outputSignals.Key))
                {
                    _equalClassesStates.Add(outputSignals.Key, new List<int>());
                }
                _equalClassesStates[outputSignals.Key].Add(outputSignals.Key);
            }
        }
        
        public void PrintMooreMapContainer()
        {
            _outputStream.Write("  ");
            foreach (List<int> states in _equalClassesStates.Values)
            {
                int state = states.First();
                _outputStream.Write($"{_inputAndOutputStatesSignals[state][1]} ");
            }
            _outputStream.WriteLine();
            _outputStream.Write("  ");
            foreach (int key in _equalClassesStates.Keys)
            {
                _outputStream.Write($"{key} ");
            }
            _outputStream.WriteLine();
            int i = 1;
            while (i <= _sourceStatesToTransitions[1].Count)
            {
                _outputStream.Write($"{i} ");
                foreach (List<int> states in _equalClassesStates.Values)
                {
                    int state = states.First();
                    int transitionState = _sourceStatesToTransitions[state][i];
                    int transitionStateToEqualClass =
                        _equalClassesStates.FirstOrDefault(e => e.Value.Contains(transitionState)).Key;
                    _outputStream.Write($"{transitionStateToEqualClass} ");
                }
                i++;
                _outputStream.WriteLine();
            }
            _outputStream.Close();
        }
    }
}