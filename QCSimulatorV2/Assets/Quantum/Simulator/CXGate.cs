using System.Collections;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;
using UnityEngine;

public class CXGate : IGate {

    private int _qubit;
    private int _controlQubit;

    public CXGate(int qubit, int controlQubit) {
        _qubit = qubit;
        _controlQubit = controlQubit;
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
        int controlToBinary = 1 << _controlQubit;
        int currentToBinary = 1 << _qubit;

        StateVector auxStateVector = outStateVector;
        outStateVector = inStateVector;
        inStateVector = auxStateVector;

        int match = controlToBinary | currentToBinary;
        int currentPosition = match;

        while (currentPosition < inStateVector.length) {
            int swapPosition = currentPosition ^ currentToBinary;

            Complex c = outStateVector[swapPosition];
            outStateVector[swapPosition] = outStateVector[currentPosition];
            outStateVector[currentPosition] = c;

            // next pos
            currentPosition += 1;
            currentPosition |= match;
        }
    }

}
