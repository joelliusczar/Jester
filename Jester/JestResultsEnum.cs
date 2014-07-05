using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Jester
{
    public sealed class JestResultsEnum
    {
        public readonly string display;
        public readonly int value;
        public readonly Color color;

        public static readonly JestResultsEnum NOT_RUN = new JestResultsEnum(1, "Not yet Run",Color.Black);
        public static readonly JestResultsEnum FAILED = new JestResultsEnum(2, "Failed",Color.Red);
        public static readonly JestResultsEnum PASSED = new JestResultsEnum(4, "Pased",Color.Green);

        private JestResultsEnum(int value, string display,Color color)
        {
            this.value = value;
            this.display = display;
            this.color = color;
        }

        public override string ToString()
        {
            return this.display;
        }
    }
}
