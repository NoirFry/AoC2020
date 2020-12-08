namespace HgcEmulator
{
    using System;
    using System.Collections.Generic;

    public class HgcEmulator
    {
        private readonly List<HgcOpCode> _program = new();

        private readonly List<int> _args = new();

        private readonly HashSet<int> _visitedPositions = new();

        private int _position;

        private int _accumulator;

        public int Accumulator => _accumulator;

        public int Position => _position;

        private HgcOpCode CurentInstruction => _program[_position];

        private int CurentArg => _args[_position];

        private bool HasFinished => _position >= _program.Count || _position < 0;

        public HgcEmulator(List<HgcOpCode> program, List<int> args)
        {
            _program.AddRange(program);
            _args.AddRange(args);
        }

        /// <summary>
        /// Run emulator with specific starting parameters until its looped or finished the program.
        /// </summary>
        /// <returns><see cref="ReturnStatus.Finished"/><br/>
        /// <see cref="ReturnStatus.Looped"/></returns>
        public ReturnStatus RunAt(int position, int accumulator, bool clearVisited)
        {
            _position = position;
            _accumulator = accumulator;
            if (clearVisited)
                _visitedPositions.Clear();

            do
            {
                if (!_visitedPositions.Add(Position))
                    return ReturnStatus.Looped;
            } while (Execute() == ReturnStatus.Success);

            return ReturnStatus.Finished;
        }

        /// <summary>
        /// Reset and run emulator until its looped or finished the program.
        /// </summary>
        /// <returns><see cref="ReturnStatus.Finished"/><br/>
        /// <see cref="ReturnStatus.Looped"/></returns>
        public ReturnStatus Run()
        {
            return RunAt(0, 0, true);
        }

        public void InstructionToggleNopJmp(int position)
        {
            if (InstructionAt(position) == HgcOpCode.nop)
                ReplaceInstructionAt(HgcOpCode.jmp, position);
            else if (InstructionAt(position) == HgcOpCode.jmp)
                ReplaceInstructionAt(HgcOpCode.nop, position);
        }

        /// <summary>
        /// Executes one stop of the program.
        /// </summary>
        /// <returns><see cref="ReturnStatus.Finished"/><br/>
        /// <see cref="ReturnStatus.Success"/></returns>
        /// <exception cref="ArgumentException"></exception>
        private ReturnStatus Execute()
        {
            if (HasFinished)
                return ReturnStatus.Finished;

            switch (CurentInstruction)
            {
                case HgcOpCode.nop:
                    _position += 1;
                    break;
                case HgcOpCode.acc:
                    _accumulator += CurentArg;
                    _position += 1;
                    break;
                case HgcOpCode.jmp:
                    _position += CurentArg;
                    break;
                case HgcOpCode.None:
                default:
                    throw new ArgumentException("Wrong OpCode", nameof(CurentInstruction));
            }

            return ReturnStatus.Success;
        }

        private HgcOpCode InstructionAt(int position) => _program[position];

        private void ReplaceInstructionAt(HgcOpCode instruction, int position)
        {
            if (instruction == HgcOpCode.None)
                throw new ArgumentException("Wrong OpCode", nameof(instruction));

            if (position < 0 || position >= _program.Count)
                throw new ArgumentOutOfRangeException(nameof(position), "Out of range");

            _program[position] = instruction;
        }
    }
}

