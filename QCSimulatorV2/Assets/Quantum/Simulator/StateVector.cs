using System;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;



public class StateVector {

    public int numQubits { get; private set; }
    public int length { get; private set; }

    public static int chunkSize = 16;
    private int _chunkElementsSize;

    private Complex[][] _values;

    public StateVector(int numQubits) {
        this.numQubits = numQubits;
        length = 1 << numQubits;
        _chunkElementsSize = 1 << chunkSize;
        int chunkCount = length / _chunkElementsSize;
        if (length % _chunkElementsSize != 0) {
            chunkCount++;
        }

        _values = new Complex[chunkCount][];
        for (int i = 0; i < chunkCount; i++) {
            _values[i] = new Complex[_chunkElementsSize];
        }

        _values[0][0] = Complex.One;
    }

    public static StateVector OneQubit => new StateVector(1);

    public Complex this[int x] {
        get {
            int chunk = x >> chunkSize;
            int idx = x % _chunkElementsSize;
            return _values[chunk][idx];
        }
        set {
            int chunk = x >> chunkSize;
            int idx = x % _chunkElementsSize;
            _values[chunk][idx] = value;
        }
    }

}

/*
public class StateVector {

    //private Complex[] _values;
    //public Complex[] values => _values;

    public int numQubits { get; private set; }
    public int length { get; private set; }

    public static int chunkSize = 14;
    private int _chunkElementsSize;

    private List<Complex[]> _values;

    public StateVector(int numQubits) {
        this.numQubits = numQubits;
        length = 1 << numQubits;
        _values = new List<Complex[]>();
        _chunkElementsSize = 1 << chunkSize;
        int rest = length;
        while (rest > 0) {
            _values.Add(new Complex[Math.Min(_chunkElementsSize, rest)]);
            rest -= _chunkElementsSize;
        }
        _values[0][0] = Complex.One;
    }

    public static StateVector OneQubit => new StateVector(1);

    public Complex this[int x] {
        get {
            int chunk = x >> chunkSize;
            int idx = x % _chunkElementsSize;
            return _values[chunk][idx];
        }
        set {
            int chunk = x >> chunkSize;
            int idx = x % _chunkElementsSize;
            _values[chunk][idx] = value;
        }
    }

}
*/

/*
public class StateVector {

    private Complex[] _values;
    //public Complex[] values => _values;

    public int numQubits { get; private set; }
    public int length { get; private set; }

    public StateVector(int numQubits) {
        this.numQubits = numQubits;
        length = 1 << numQubits;
        _values = new Complex[length];
        _values[0] = Complex.One;
    }

    public static StateVector OneQubit => new StateVector(1);

    public Complex this[int x] {
        get => _values[x];
        set => _values[x] = value;
    }

}
//*/