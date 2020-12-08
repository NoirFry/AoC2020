namespace HgcEmulator
{
    using System;
    using System.Collections.Generic;

    public class HgcEmulator
    {
        private readonly List<HgcOpCodes> program = new();

        private readonly List<int> args = new();

        private readonly HashSet<int> visitedPositions = new();

        private int position;

        private int accumulator;

        private HgcOpCodes CurentInstruction => program[position];

        private int CurentArg => args[position];

        public HgcEmulator(List<HgcOpCodes> program, List<int> args)
        {
            this.program.AddRange(program);
            this.args.AddRange(args);
        }

        /// <summary>
        /// Run emulator until its looped or exited the program.
        /// </summary>
        /// <param name="acc">Accumulator value before the last instruction.</param>
        /// <returns><see langword="true"/> if exited the program, otherwise <see langword="false"/>.</returns>
        public bool Run(out int acc)
        {
            visitedPositions.Add(position);

            bool visited;
            bool endOfProgram;
            int oldAcc;

            do
            {
                oldAcc = accumulator;
                endOfProgram = Execute();
                visited = !visitedPositions.Add(position);
            } while (!visited && !endOfProgram);

            acc = oldAcc;
            return endOfProgram;
        }

        /// <summary>
        /// Executes one stop of the program.
        /// </summary>
        /// <returns><see langword="true"/> if execution has reached the end of the program, otherwise <see langword="false"/>.</returns>
        public bool Execute()
        {
            switch (CurentInstruction)
            {
                case HgcOpCodes.nop:
                    JumpTo(1);
                    break;
                case HgcOpCodes.acc:
                    accumulator += CurentArg;
                    JumpTo(1);
                    break;
                case HgcOpCodes.jmp:
                    JumpTo(CurentArg);
                    break;
                case HgcOpCodes.None:
                default:
                    throw new ArgumentException("Wrong OpCode", nameof(CurentInstruction));
            }

            return position == program.Count;
        }

        public void InstructionToggleNopJmp(int position)
        {
            if (InstructionAt(position) == HgcOpCodes.nop)
                ReplaceInstructionAt(HgcOpCodes.jmp, position);
            else if (InstructionAt(position) == HgcOpCodes.jmp)
                ReplaceInstructionAt(HgcOpCodes.nop, position);
        }

        public HgcOpCodes InstructionAt(int position)
        {
            if (position < 0 || position >= program.Count)
                throw new ArgumentOutOfRangeException(nameof(position), "Out of range");

            return program[position];
        }

        public void ReplaceInstructionAt(HgcOpCodes instruction, int position)
        {
            if (instruction == HgcOpCodes.None)
                throw new ArgumentException("Wrong OpCode", nameof(instruction));

            if (position < 0 || position >= program.Count)
                throw new ArgumentOutOfRangeException(nameof(position), "Out of range");

            program[position] = instruction;
        }

        public void Reset()
        {
            position = 0;
            accumulator = 0;
            visitedPositions.Clear();
        }

        private void JumpTo(int jump)
        {
            if (position + jump < 0)
                throw new ArgumentOutOfRangeException(nameof(jump), "Out of range");
            position += jump;
        }
    }
}

