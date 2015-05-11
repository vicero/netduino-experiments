using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace RelayDriver
{
    public class Relay : IDisposable
    {
        private bool _disposed;
        private bool _state;

        private readonly OutputPort _outputPin;
        private readonly int _runTime;


        public Relay(Cpu.Pin pin, int runTime)
        {
            _outputPin = new OutputPort(pin, true);
            _runTime = runTime;
        }

        /// <summary>
        /// Deletes an instance of the <see cref="Relay"/> class.
        /// </summary>
        ~Relay()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases resources used by this <see cref="Relay"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the resources associated with the <see cref="Relay"/> object.
        /// </summary>
        /// <param name="disposing">
        /// <b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.
        /// </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                try
                {
                    _outputPin.Dispose();
                }
                finally
                {
                    _disposed = true;
                }
            }
        }

        /// <summary>
        /// Runs the relay for the length of time set in the <see cref="Relay"/> constructor
        /// </summary>
        public void Run() {
            //if (_state)
            //{
            //    return;
            //}

            _state = false;
            _outputPin.Write(_state);
            new Timer(new TimerCallback(TurnRelayOff), null, _runTime, -1);
        }

        private void TurnRelayOff(object sender)
        {
            _state = true;
            _outputPin.Write(_state);
        }

        /// <summary>
        /// Current state of the relay.  [true] if on, [false] if off
        /// </summary>
        public bool State
        {
            get { return _state; }
        }
    }
}
