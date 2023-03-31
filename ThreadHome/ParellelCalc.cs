using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadState = System.Threading.ThreadState;

namespace ThreadHome
{
    internal class ParellelCalc
    {
        public long Min { get; set; }
        public long Max { get; set; }
        public int Count { get; set; }
        private long parSum = 0;
        Stopwatch stopwatch = new Stopwatch();
        List<Thread> allThreads = new List<Thread>();
        public ParellelCalc(long min, long max, int count)
        {
            Min = min;
            Max = max;
            Count = count;
        }

        public long Run(out long elapsed)
        {

            CreateThreads();
            stopwatch.Start();
            StartAllThreads();
            
            while (CheckTreadsState()) ;
            stopwatch.Stop();
            elapsed = stopwatch.ElapsedMilliseconds;
            return parSum;


        }
        private void CalcResult(long start, long end)
        {
            long sum = 0;
            for (long i = start; i < end; i++)
            {
                sum += i;
            }
            parSum += sum;
        }
        private bool CheckTreadsState()
        {
            int count = 0;
            for (int i = 0; i < allThreads.Count; i++)
            {
                if (allThreads[i].ThreadState == ThreadState.Running)
                    count++;
            }
            if (count == 0)
            {
                return false;
            }
            else
                return true;
        }

        private void CreateThreads()
        {
            for (int i = 0; i < Count; i++)
            {
                int index = i;
                allThreads.Add(new Thread(() => CalcResult(Max / Count * index, Max / Count * (index + 1))));
            }
        }
        private void StartAllThreads()
        {
            for (int i = 0; i < Count; i++)
            {
                allThreads[i].Start();
            }
        }
    }
}
