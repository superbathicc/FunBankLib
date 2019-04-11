using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunBankLib {
    class IdleCheck {
        int waiting;
        int step;
        Task runner;
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        /// <summary>
        /// Idle will occur step * waiting after the Start() function has been called.
        /// </summary>
        /// <param name="waiting"></param>
        /// <param name="step"></param>
        public IdleCheck(int waiting, int step) {
            this.waiting = waiting;
            this.step = step;
        }

        public void Start() {
            runner = new Task(async () => {
                int counter = 0;
                while(!tokenSource.IsCancellationRequested) {
                    if (counter++ < waiting) {
                        await Task.Delay(step);
                    } else {
                        OnIdled(new EventArgs());
                    }
                }
            }, tokenSource.Token);
        }

        public void Refresh() {
            tokenSource.Cancel();
            new Task(async () => {
                while (!(runner.IsCanceled || runner.IsCompleted)) {
                    await Task.Delay(step);
                }
                Start();
            });
        }

        public event EventHandler Idled;
        protected virtual void OnIdled(EventArgs e) {
            Idled?.Invoke(this, e);
        }
    }
}
