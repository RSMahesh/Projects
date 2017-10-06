using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{

    public interface ICommand<T>
    {
        void Do(T input);
        void Undo(T input);
    }

    public class SetCellDataCommand : ICommand<CellData>
    {
        private CellData _Value;

        DataGridView _dataGridView;
        public SetCellDataCommand(DataGridView dataGridView)
        {
            _dataGridView = dataGridView;

        }
        public CellData Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private void Set(CellData cellData)
        {
            //_dataGridView.CurrentCell = _dataGridView.Rows[cellData.Location.Y].Cells[cellData.Location.X];
            _dataGridView.EndEdit();
            _dataGridView.Rows[cellData.Location.Y].Cells[cellData.Location.X].Value = cellData.Value;
        }

        public void Do(CellData cellData)
        {
            Set(cellData);
        }
        public void Undo(CellData cellData)
        {
            Set(cellData);
        }

    }

    public class UndoRedoStack<T>
    {
        private Stack<T> _Undo;
        private Stack<T> _Redo;

        private ICommand<T> _command;
        public UndoRedoStack(ICommand<T> command)
        {
            _command = command;
            Reset();
        }

        public int UndoCount
        {
            get
            {
                return _Undo.Count;
            }
        }
        public int RedoCount
        {
            get
            {
                return _Redo.Count;
            }
        }

        public void Reset()
        {
            _Undo = new Stack<T>();
            _Redo = new Stack<T>();
        }
        public void Do(T input)
        {
            //cmd.Do(input);
            _Undo.Push(input);
            _Redo.Clear(); // Once we issue a new command, the redo stack clears
        }
        public void Undo()
        {
            if (_Undo.Count > 0)
            {
                T input = _Undo.Pop();
                _command.Undo(input);
                _Redo.Push(input);
            }
        }
        public void Redo()
        {
            if (_Redo.Count > 0)
            {
                T input = _Redo.Pop();
                _command.Do(input);
                _Undo.Push(input);
            }
        }
    }
}
