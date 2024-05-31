using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    internal class DAO
    {
        private static DAO? instance;
        private static readonly object daoLock = new object();
        private ConcurrentQueue<string> queue;
        private readonly int maxBufferSize = 1000;
        private bool bufferOverflowed;
        
        public DAO()
        {
            queue = new ConcurrentQueue<string>();
            Write();
        }

        public static DAO CreateInstance()
        {
            lock(daoLock)
            {
                if(instance == null)
                {
                    instance = new DAO();
                }
                return instance;
            }
        }

        public void Add(Ball ball)
        {
            string time = DateTime.Now.ToString("h:mm:ss tt");
            string log = time + " Ball: " + ball.Id + " Position: " + vectorFormatToString(ball.Position) + " Velocity: " + vectorFormatToString(ball.Velocity);
            queue.Enqueue(log);
            if(queue.Count >= maxBufferSize)
            {
                bufferOverflowed = true;
            }
        }

        private string vectorFormatToString(Vector2 vector)
        {
            return "X: " + Math.Round(vector.X, 3) + " Y: " + Math.Round(vector.Y, 3);
        }

        private void Write()
        {
            Task.Run(async () =>
            {
                using StreamWriter streamWriter = new StreamWriter("logger_file.json");
                while (true)
                {
                    while (queue.TryDequeue(out string log))
                    {
                        string logger = JsonSerializer.Serialize(log);
                        await streamWriter.WriteLineAsync(logger);
                    }

                    if (bufferOverflowed)
                    {
                        await streamWriter.WriteLineAsync("Buffer overflow occurred!");
                        bufferOverflowed = false;
                    }

                    await streamWriter.FlushAsync();
                }
            });
        }
    }
}
