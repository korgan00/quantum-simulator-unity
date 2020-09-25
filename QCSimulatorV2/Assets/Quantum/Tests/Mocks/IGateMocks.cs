using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.Mocks {
    public class IGateMocks {

        public class GateStub : IGate {

            private Func<StateVector, StateVector> f;

            public GateStub(Func<StateVector, StateVector> applyTo) {
                f = applyTo;
            }

            public void ApplyTo(ref StateVector inStateVector, ref StateVector outStateVector) {
                outStateVector = f(inStateVector);
            }
        }
    }
}
