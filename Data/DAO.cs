using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data
{
    internal class DAO
    {
        private static DAO? instance;
        private static readonly object daoLock = new object();
        private static readonly object bufferLock = new object();
        private BlockingCollection<LoggerBall> queue;
        private readonly int maxBufferSize = 1000;
        private bool bufferOverflowed;
        
        private DAO()
        {
            queue = new BlockingCollection<LoggerBall>(new ConcurrentQueue<LoggerBall>(), maxBufferSize);
            Task.Run(Write);
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

        public void Add(Ball ball, DateTime date )
        {
            //string time = DateTime.Now.ToString("h:mm:ss tt");
            //string log = time + " Ball: " + ball.Id + " Position: " + vectorFormatToString(ball.Position) + " Velocity: " + vectorFormatToString(ball.Velocity);
            
            Task.Run(() =>
            {
                if (queue.TryAdd(new LoggerBall(date, ball.Id, ball.Position, ball.Velocity)))
                {
                    return;
                }
                else
                {
                    lock (bufferLock)
                    {
                        bufferOverflowed = true;
                    }
                }

            });
        }


        private async void Write()
        {
            await using StreamWriter streamWriter = new StreamWriter("logger_file.json", false,  Encoding.UTF8);
            
            
           while (!queue.IsCompleted)
           {
                bool bufferOverflowedTrue = false;
                lock (bufferLock)
                {
                    if (bufferOverflowed)
                    {
                        bufferOverflowedTrue = true;
                        bufferOverflowed = false;
                    }
                }
                        
                if (bufferOverflowedTrue)
                    await streamWriter.WriteLineAsync("Buffer overflow occurred!");
                LoggerBall ball_taken = queue.Take();


                await streamWriter.WriteLineAsync(JsonConvert.SerializeObject(ball_taken));
                await streamWriter.FlushAsync();

            }
        }
    }
}
