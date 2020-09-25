using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;

public class QuantumCircuitTests : MonoBehaviour {


    [ContextMenu("Test H0 Gate")]
    private void TestH0Gate() {
        QuantumCircuit qc = new QuantumCircuit(3);

        System.DateTime t = System.DateTime.Now;
        qc.Apply(Gates.H(0));
        Debug.Log($"H: {(System.DateTime.Now - t).TotalMilliseconds / 1000f}s");

        StateVector sv = qc.stateVector;

        string s = "";
        for (int i = 0; i < sv.length; i++) {
            s += $"[{sv[i].Real:N2} :: {sv[i].Imaginary:N2}i]\n";
        }
        Debug.Log(s);

    }
    [ContextMenu("Test H1 Gate")]
    private void TestH1Gate() {
        QuantumCircuit qc = new QuantumCircuit(3);

        System.DateTime t = System.DateTime.Now;
        qc.Apply(Gates.H(1));
        Debug.Log($"H: {(System.DateTime.Now - t).TotalMilliseconds / 1000f}s");

        StateVector sv = qc.stateVector;

        string s = "";
        for (int i = 0; i < sv.length; i++) {
            s += $"[{sv[i].Real:N2} :: {sv[i].Imaginary:N2}i]\n";
        }
        Debug.Log(s);

    }
    [ContextMenu("Test H2 Gate")]
    private void TestH2Gate() {
        QuantumCircuit qc = new QuantumCircuit(3);

        System.DateTime t = System.DateTime.Now;
        qc.Apply(Gates.H(2));
        Debug.Log($"H: {(System.DateTime.Now - t).TotalMilliseconds / 1000f}s");

        StateVector sv = qc.stateVector;

        string s = "";
        for (int i = 0; i < sv.length; i++) {
            s += $"[{sv[i].Real:N2} :: {sv[i].Imaginary:N2}i]\n";
        }
        Debug.Log(s);

    }

    [ContextMenu("Perf H Gate 28Q")]
    private void TestHGate28Q() {
        QuantumCircuit qc = new QuantumCircuit(28);
        qc.Apply(Gates.H(0));

        System.DateTime t = System.DateTime.Now;
        qc.Apply(Gates.H(0));
        Debug.Log($"H: {(System.DateTime.Now - t).TotalMilliseconds / 1000f}s");

        StateVector sv = qc.stateVector;
    }

    [ContextMenu("Perf H Gate 10Q")]
    private void TestHGate10Q() {
        QuantumCircuit qc = new QuantumCircuit(10);
        qc.Apply(Gates.H(0));

        const int it = 100;
        System.DateTime t = System.DateTime.Now;
        for (int i = 0; i < it; i++) {
            qc.Apply(Gates.H(0));
        }
        Debug.Log($"H: {(System.DateTime.Now - t).TotalMilliseconds / it}ms");

        StateVector sv = qc.stateVector;
    }

    [ContextMenu("Perf CX Gate 10Q")]
    private void TestCXGate10Q() {
        QuantumCircuit qc = new QuantumCircuit(10);
        qc.Apply(Gates.H(0));

        const int it = 100;
        System.DateTime t = System.DateTime.Now;
        for (int i = 0; i < it; i++) {
            qc.Apply(new CXGate(i % 10, 0));
        }
        Debug.Log($"H: {(System.DateTime.Now - t).TotalMilliseconds / it}ms");

        StateVector sv = qc.stateVector;
    }

    [ContextMenu("Perf 2 H Gate 28Q")]
    private void TestHGate29QPerf() {
        QuantumCircuit qc = new QuantumCircuit(28);
        qc.Apply(Gates.H(0));

        const int it = 10;
        System.DateTime t = System.DateTime.Now;
        for (int i = 0; i < it; i++) {
            qc.Apply(Gates.H(0));
        }
        Debug.Log($"H: {(System.DateTime.Now - t).TotalMilliseconds / (1000f * it)}s");

        StateVector sv = qc.stateVector;
    }



    [ContextMenu("Test Compare")]
    private void TestCompare() {
        QuantumCircuit qc = new QuantumCircuit(24);
        /*
        qc.Apply(RotationGate.H(10));
        Debug.Log(System.DateTime.Now - t);
        */
        System.DateTime t = System.DateTime.Now;
        /*
        //qc = new QuantumCircuit(24);
        t = System.DateTime.Now;
        for (int j = 0; j < 10; j++) {
            qc.Apply(RotationGate.H(j));
        }
        Debug.Log($"H: {(System.DateTime.Now - t).TotalMilliseconds / 10000f}s");
        */
        //qc = new QuantumCircuit(24);
        t = System.DateTime.Now;
        for (int j = 0; j < 10; j++) {
            qc.Apply(new CXGate(j, 0));
        }
        Debug.Log($"CX: {(System.DateTime.Now - t).TotalMilliseconds / 10000f}s");

        /*
        StateVector sv = qc.stateVector;

        QuantumCircuit qc1 = new QuantumCircuit(1);

        t = System.DateTime.Now;
        qc1.RawApply(RotationGate.H(0));
        Debug.Log(System.DateTime.Now - t);

        StateVector sv1 = qc1.stateVector;

        bool equals = true;
        for (int i = 0; i < sv1.length; i++) {
            equals &= sv[i] == sv1[i];
        }
        Debug.Log(equals);
        */
    }

    [ContextMenu("Test CX Gate")]
    private void TestCXGate() {

        QuantumCircuit qc = new QuantumCircuit(3);
        qc.Apply(Gates.H(0));
        qc.Apply(new CXGate(1, 0));
        qc.Apply(new CXGate(2, 0));

        StateVector sv = qc.stateVector;

        string s = "";
        for (int i = 0; i < sv.length; i++) {
            s += $"[{sv[i].Real:N2} :: {sv[i].Imaginary:N2}i]\n";
        }
        Debug.Log(s);
    }


    [ContextMenu("Chunk Test")]
    private void TestChunk() {

        for (int i = 8; i <= 16; i++) {
            const int it = 10;
            const int qubits = 28;
            StateVector.chunkSize = i;
            QuantumCircuit qc = new QuantumCircuit(qubits);
            qc.Apply(Gates.H(0));

            System.DateTime t0 = System.DateTime.Now;
            for (int j = 0; j < it; j++) {
                qc.Apply(Gates.H(j % qubits));
            }
            System.DateTime t1 = System.DateTime.Now;
            Debug.Log($"Chunk(2^{i}): {(t1 - t0).TotalMilliseconds / (1000f * it)}s");
        }
    }
}
