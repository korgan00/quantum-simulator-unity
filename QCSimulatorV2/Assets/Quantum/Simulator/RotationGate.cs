using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;

public class RotationGate : IGate {



    public Complex[,] _matrix;
    public int _qubit;

    private RotationGate() { }

    public RotationGate(int qubit, Complex[,] matrix) {
        if (matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2) {
            throw new ArgumentException("Matrix dimension should be 2x2");
        }
        _matrix = matrix;
        _qubit = qubit;
    }

    public Complex this[int x, int y] {
        get => _matrix[x, y];
    }

    public void ApplyTo(ref StateVector inStateVector, ref StateVector outStateVector) {
        int qubitToBinary = 1 << _qubit;
        int qubitBinaryMask = ~qubitToBinary;
        int matrixRow, a, b;

        for (int i = 0; i < inStateVector.length; i++) {
            matrixRow = (i & qubitToBinary) >> _qubit;
            a = i & qubitBinaryMask;
            b = i | qubitToBinary;

            outStateVector[i] = inStateVector[a] * _matrix[0, matrixRow] +
                                inStateVector[b] * _matrix[1, matrixRow];
        }
    }

    public static Complex[,] KroneckerProduct(Complex[][,] mats) {
        int dimX = 1, dimY = 1;
        for (int i = 0; i < mats.Length; i++) {
            dimX *= mats[i].GetLength(0);
            dimY *= mats[i].GetLength(1);
        }

        Complex[,] gRes = new Complex[dimX, dimY];

        for (int i = 0; i < dimX; i++) {
            for (int j = 0; j < dimY; j++) {
                gRes[i, j] = 1;
                int dimXDivisor = 1;
                int dimYDivisor = 1;
                for (int k = 0; k < mats.Length; k++) {
                    Complex[,] currMat = mats[k];

                    gRes[i, j] *= currMat[(i / dimXDivisor) % currMat.GetLength(0), (j / dimYDivisor) % currMat.GetLength(1)];

                    dimXDivisor *= currMat.GetLength(0);
                    dimYDivisor *= currMat.GetLength(1);
                }
            }
        }

        return gRes;
    }


}
