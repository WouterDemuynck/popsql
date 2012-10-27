using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql.Text
{
    internal class SqlWriterStateManager
    {
        private static readonly Dictionary<SqlWriterState, SqlWriterState[]> _transitions = new Dictionary<SqlWriterState, SqlWriterState[]>
            {
                { SqlWriterState.Start,       new[] { SqlWriterState.StartSelect, SqlWriterState.StartUpdate, SqlWriterState.StartInsert, SqlWriterState.StartDelete } },
                { SqlWriterState.StartSelect, new[] { SqlWriterState.Select } },
                { SqlWriterState.Select,      new[] { SqlWriterState.StartFrom } },
                { SqlWriterState.StartFrom,   new[] { SqlWriterState.From } },
                { SqlWriterState.StartDelete, new[] { SqlWriterState.StartFrom } },
                { SqlWriterState.StartInsert, new[] { SqlWriterState.Into } },
                { SqlWriterState.StartUpdate, new[] { SqlWriterState.Update } },
            };

        private readonly Stack<SqlWriterState> _states;

        public SqlWriterStateManager()
        {
            _states = new Stack<SqlWriterState>();
            _states.Push(SqlWriterState.Start);
        }

        public void RequestState(SqlWriterState state)
        {
            if (IsClosed) throw new InvalidOperationException("The writer has already been closed.");

            // Build in a safety net for missing state transitions.
            SqlWriterState[] validNextStates;
            if (!_transitions.TryGetValue(CurrentState, out validNextStates))
                throw new InvalidOperationException(
                    string.Format(
                        "The current state '{0}' does not allow any state transitions. " +
                        "This error should not occur, please report this to the authors of this library.", 
                        CurrentState));

            // Provide enough information to try and guess what's wrong.
            if (!validNextStates.Contains(state))
                throw new InvalidOperationException(
                    string.Format(
                        "The current state '{0}' is invalid for the current operation. Valid next states are '{1}'.",
                        string.Join("', '", validNextStates)));

            _states.Push(state);
        }

        public void Close()
        {
            _states.Clear();
            _states.Push(SqlWriterState.Closed);
        }

        public SqlWriterState CurrentState 
        {
            get
            {
                return _states.Peek();
            }
        }

        public bool IsClosed
        {
            get
            {
                return CurrentState == SqlWriterState.Closed;
            }
        }
    }
}
