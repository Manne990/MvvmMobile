﻿using System;
using System.Collections.Generic;

namespace MvvmMobile.Core.ViewModel
{
    public sealed class Payloads : IPayloads
    {
        // Private Members
        private readonly Dictionary<Guid, IPayload> _payloads;


        // -----------------------------------------------------------------------------

        // Constructors
        public Payloads()
        {
            _payloads = new Dictionary<Guid, IPayload>();
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void Add(Guid id, IPayload payload)
        {
            lock(_payloads)
            {
                if (_payloads.ContainsKey(id))
                {
                    return;
                }

                _payloads.Add(id, payload);
            }
        }

        public void Remove(Guid id)
        {
            lock (_payloads)
            {
                if (_payloads.ContainsKey(id) == false)
                {
                    return;
                }

                _payloads.Remove(id);
            }
        }

        public T Get<T>(Guid id)
        {
            lock (_payloads)
            {
                if (_payloads.ContainsKey(id) == false)
                {
                    return default(T);
                }

                var payload = _payloads[id];

                if (!(payload is T))
                {
                    return default(T);
                }

                return (T)payload;
            }
        }
    }
}