using System;
using System.Collections;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;
using UnityEngine;

public class SwapGate : IGate {

    private int _qubit0;
    private int _qubit1;

    public SwapGate(int qubit0, int qubit1) {
        _qubit0 = Math.Min(qubit0, qubit1);
        _qubit1 = Math.Max(qubit0, qubit1);
    }
    /*
    public void ApplyTo(ref StateVector inStateVector, ref StateVector outStateVector) {
        int controlToBinary = 1 << _controlQubit;
        int currentToBinary = 1 << _qubit;

        
        for (int i = 0; i < inStateVector.length; i++) {
            
            if ((i & controlToBinary) > 0) {
                int swapPosition = i ^ currentToBinary;
                outStateVector[swapPosition] = inStateVector[i];
            } else {
                outStateVector[i] = inStateVector[i];
            }
        }
       
    }
    */

    public void ApplyTo(ref StateVector inStateVector, ref StateVector outStateVector) {
        int qubit0ToBinary = 1 << _qubit0;
        int qubit1ToBinary = 1 << _qubit1;
        int qubit0Mask = ~qubit0ToBinary;
        int qubit1Mask = ~qubit1ToBinary;

        StateVector auxStateVector = outStateVector;
        outStateVector = inStateVector;
        inStateVector = auxStateVector;

        int match = _qubit0 | _qubit1;
        int currentPosition = match;

        while (currentPosition < inStateVector.length) {
            int swapPosition0 = currentPosition & qubit0Mask;
            int swapPosition1 = currentPosition & qubit1Mask;

            Complex c = outStateVector[swapPosition0];
            outStateVector[swapPosition0] = outStateVector[swapPosition1];
            outStateVector[swapPosition1] = c;

            // next pos
            currentPosition += 1;
            currentPosition |= match;
        }
    }

    public void RawApplyTo(in StateVector inStateVector, ref StateVector outStateVector) => throw new System.NotImplementedException();
}
