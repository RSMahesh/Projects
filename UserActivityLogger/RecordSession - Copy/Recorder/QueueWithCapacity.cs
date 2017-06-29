using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder
{
    public class QueueWithCapacity
    {
        Queue<Char> charQueue = new Queue<char>();

        int _maxLimit;
        public QueueWithCapacity(int maxLimit)
        {
            _maxLimit = maxLimit;
        }

        public void Add(string text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (charQueue.Count >= _maxLimit)
                {
                    charQueue.Dequeue();
                }

                charQueue.Enqueue(text[i]);
            }
        }

        public string GetText()
        {
            return new string(charQueue.ToArray());
        }
    }
}
