using System.Collections.Generic;
using System.IO;

namespace AutomationConvertation
{
    public class MachineConverter
    {
        protected readonly StreamReader _inputStream;
        protected readonly StreamWriter _outputStream;
        protected readonly Dictionary<int, Dictionary<int,int>> _sourceStatesToTransitions = new();
        protected readonly Dictionary<int, Dictionary<int, int>> _inputAndOutputStatesSignals = new();
        protected readonly Dictionary<int, List<int>> _equalClassesStates = new();

        public MachineConverter(StreamReader inputStream, StreamWriter outputStream)
        {
            _inputStream = inputStream;
            _outputStream = outputStream;
            _equalClassesStates = new Dictionary<int, List<int>>();
        }
    }
}