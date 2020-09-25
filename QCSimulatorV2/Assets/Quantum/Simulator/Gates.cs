using System.Collections;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;
using UnityEngine;
using System;

public static class Gates {

    public static float ONE_SQRT2 = 1 / Mathf.Sqrt(2.0f);

    public static IGate H(int qubit) => new RotationGate(qubit, HMat);
    public static IGate T(int qubit) => new RotationGate(qubit, TMat);
    public static IGate X(int qubit) => new RotationGate(qubit, XMat);
    public static IGate Z(int qubit) => new RotationGate(qubit, ZMat);
    public static IGate S(int qubit) => new RotationGate(qubit, SMat);
    public static IGate U1(int qubit, float lambda) => U3(qubit, 0, 0, lambda);
    public static IGate U2(int qubit, float phi, float lambda) => U3(qubit, 0, phi, lambda);
    public static IGate U3(int qubit, float theta, float phi, float lambda) => new RotationGate(qubit, UMat(theta, phi, lambda));
    public static IGate Id(int qubit) => new RotationGate(qubit, IdMat);

    public static IGate CX(int qubit, int control) => new CXGate(qubit, control);
    public static IGate Swap(int qubit0, int qubit1) => new SwapGate(qubit0, qubit1);

    private static Complex[,] IdMat => new Complex[2, 2] {
                { Complex.One, Complex.Zero},
                { Complex.Zero, Complex.One}
            };

    private static Complex[,] HMat => new Complex[2, 2] {
                { Complex.One * ONE_SQRT2,  Complex.One * ONE_SQRT2},
                { Complex.One * ONE_SQRT2, -Complex.One * ONE_SQRT2}
            };
    private static Complex[,] TMat => new Complex[2, 2] {
                { Complex.One,  Complex.Zero},
                { Complex.Zero, EPowI(Mathf.PI / 4.0f)}
            };
    private static Complex[,] XMat => new Complex[2, 2] {
                { Complex.Zero, Complex.One},
                { Complex.One, Complex.Zero}
            };
    private static Complex[,] ZMat => new Complex[2, 2] {
                { Complex.One, Complex.Zero},
                { Complex.Zero, -Complex.One}
            };
    private static Complex[,] SMat => new Complex[2, 2] {
                { Complex.One, Complex.Zero},
                { Complex.Zero, Complex.ImaginaryOne}
            };
    private static Complex[,] UMat(float theta, float phi, float lambda) => new Complex[2, 2] {
                { Math.Cos(theta/2), -EPowI(lambda) * Math.Sin(theta/2)},
                { EPowI(phi) * Math.Sin(theta/2), EPowI(phi + lambda) * Math.Cos(theta/2)}
            };

    private static Complex EPowI(Complex x) => Complex.Pow(Complex.One * System.Math.E, Complex.ImaginaryOne * x);
}
