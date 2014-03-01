using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql.Text
{
    internal class SqlWriterStateManager
    {
        // This table holds the valid state transitions in the state machine. The key of the dictionary is the starting state and the value
        // of the dictionary represents the list of valid next states.
        private static readonly Dictionary<SqlWriterState, SqlWriterState[]> _transitions = new Dictionary<SqlWriterState, SqlWriterState[]>
            {
                { SqlWriterState.Start,               new[] { SqlWriterState.StartSelect, SqlWriterState.StartUpdate, SqlWriterState.StartInsert, SqlWriterState.StartDelete } },
                { SqlWriterState.StartSelect,         new[] { SqlWriterState.Select } },
                { SqlWriterState.Select,              new[] { SqlWriterState.StartFrom, SqlWriterState.EndSelect } },
                { SqlWriterState.StartFrom,           new[] { SqlWriterState.From } },
                { SqlWriterState.From,                new[] { SqlWriterState.StartJoin, SqlWriterState.StartWhere, SqlWriterState.StartOrderBy, SqlWriterState.EndSelect } },
                { SqlWriterState.StartJoin,           new[] { SqlWriterState.Join } },
                { SqlWriterState.Join,                new[] { SqlWriterState.StartOn, SqlWriterState.StartWhere, SqlWriterState.StartOrderBy, SqlWriterState.EndSelect } },
                { SqlWriterState.StartOn,             new[] { SqlWriterState.StartExpression } },
                { SqlWriterState.On,                  new[] { SqlWriterState.StartExpression, SqlWriterState.StartWhere, SqlWriterState.StartOrderBy, SqlWriterState.EndSelect } },
                { SqlWriterState.StartWhere,          new[] { SqlWriterState.StartExpression } },
                { SqlWriterState.StartExpression,     new[] { SqlWriterState.Expression } },
                { SqlWriterState.Expression,          new[] { SqlWriterState.On, SqlWriterState.Where, SqlWriterState.StartExpression, SqlWriterState.StartOrderBy } },
                { SqlWriterState.Where,               new[] { SqlWriterState.StartExpression, SqlWriterState.StartOrderBy, SqlWriterState.EndSelect } },
                { SqlWriterState.StartOrderBy,        new[] { SqlWriterState.OrderBy } },
                { SqlWriterState.OrderBy,             new[] { SqlWriterState.EndSelect } },
                { SqlWriterState.EndSelect,           new[] { SqlWriterState.Union, SqlWriterState.StartSelect, SqlWriterState.StartUpdate, SqlWriterState.StartInsert, SqlWriterState.StartDelete } },
                { SqlWriterState.Union,               new[] { SqlWriterState.StartSelect } },
                { SqlWriterState.StartDelete,         new[] { SqlWriterState.StartFrom } },
                { SqlWriterState.StartInsert,         new[] { SqlWriterState.StartInto } },
                { SqlWriterState.Into,                new[] { SqlWriterState.StartValues } },
                { SqlWriterState.StartInto,           new[] { SqlWriterState.Into, SqlWriterState.StartValues } },
                { SqlWriterState.StartValues,         new[] { SqlWriterState.Values } },
                { SqlWriterState.Values,              new[] { SqlWriterState.EndValues } },
                { SqlWriterState.EndValues,           new[] { SqlWriterState.StartValues } },
                { SqlWriterState.StartUpdate,         new[] { SqlWriterState.Update } },
                { SqlWriterState.Update,              new[] { SqlWriterState.StartSet } },
                { SqlWriterState.StartSet,            new[] { SqlWriterState.Set } },
                { SqlWriterState.Set,                 new[] { SqlWriterState.Set, SqlWriterState.StartWhere } },
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
                        "The current state '{0}' is invalid for the requested state '{1}'. Valid next states are '{2}'.",
                        CurrentState,
                        state,
                        string.Join("', '", validNextStates)));

            _states.Push(state);
        }

        public void RequestState(IDictionary<SqlWriterState, SqlWriterState> states)
        {
            var state = _states.First(_ => states.ContainsKey(_));
            RequestState(states[state]);
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
