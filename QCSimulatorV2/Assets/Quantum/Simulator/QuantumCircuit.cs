using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumCircuit {

    public StateVector stateVector => _states[_currentStateIdx];
    private ref StateVector backState => ref _states[_currentStateIdx ^ 1];

    private int _currentStateIdx;
    private StateVector[] _states;

    public QuantumCircuit(int numQubits) {
        _states = new StateVector[2];
        _states[0] = new StateVector(numQubits);
        _states[1] = new StateVector(numQubits);
    }

    private void swapStates() => _currentStateIdx ^= 1;

    public void Apply(IGate gate) {
        gate.ApplyTo(ref _states[_currentStateIdx], ref backState);
        swapStates();
    }
}
