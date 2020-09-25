using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using Tests.Mocks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {

    public class QuantumCircuitTests {

        [Test]
        public void NewQCStateVectorLengthTest() {

            // Arrange
            QuantumCircuit qc;
            int expectLenght = 4;

            // Act
            qc = new QuantumCircuit(2);
            int actualLenght = qc.stateVector.length;

            // Assert
            Assert.IsTrue(expectLenght == actualLenght,
                            $"Initial QuantumCircuit with 2 qubit has a StateVector length, " +
                            $"expected {expectLenght}" +
                            $"but found {actualLenght}.");
        }


        [Test]
        public void ApplyOneGateTest() {

            // Arrange
            QuantumCircuit qc = new QuantumCircuit(2);
            StateVector sv = new StateVector(2);
            sv[0] = Complex.Zero;
            sv[1] = Complex.Zero;
            sv[2] = Complex.Zero;
            sv[3] = Complex.One;
            IGate gate = new IGateMocks.GateStub(_ => sv);
            Complex expect0IdxValue = Complex.Zero;
            Complex expect1IdxValue = Complex.Zero;
            Complex expect2IdxValue = Complex.Zero;
            Complex expect3IdxValue = Complex.One;

            // Act
            qc.Apply(gate);
            Complex actual0IdxValue = qc.stateVector[0];
            Complex actual1IdxValue = qc.stateVector[1];
            Complex actual2IdxValue = qc.stateVector[2];
            Complex actual3IdxValue = qc.stateVector[3];

            // Assert
            Assert.IsTrue(expect0IdxValue == actual0IdxValue &&
                    expect1IdxValue == actual1IdxValue &&
                    expect2IdxValue == actual2IdxValue &&
                    expect3IdxValue == actual3IdxValue,
                        $"Apply IGate should modify StateVector, " +
                        $"expected [ {expect0IdxValue}, {expect1IdxValue}, {expect2IdxValue}, {expect3IdxValue} ]" +
                        $"but found [ {actual0IdxValue}, {actual1IdxValue}, {actual2IdxValue}, {actual3IdxValue} ].");

        }

        [Test]
        public void ApplyOneGateProvideStateVectorToGate() {

            // Arrange
            QuantumCircuit qc = new QuantumCircuit(2);
            qc.stateVector[0] = Complex.Zero;
            qc.stateVector[1] = Complex.Zero;
            qc.stateVector[2] = Complex.One;
            qc.stateVector[3] = Complex.Zero;
            StateVector sv = new StateVector(2);
            IGate gate = new IGateMocks.GateStub(s => sv = s);
            Complex expect0IdxValue = Complex.Zero;
            Complex expect1IdxValue = Complex.Zero;
            Complex expect2IdxValue = Complex.One;
            Complex expect3IdxValue = Complex.Zero;

            // Act
            qc.Apply(gate);
            Complex actual0IdxValue = sv[0];
            Complex actual1IdxValue = sv[1];
            Complex actual2IdxValue = sv[2];
            Complex actual3IdxValue = sv[3];

            // Assert
            Assert.IsTrue(expect0IdxValue == actual0IdxValue &&
                    expect1IdxValue == actual1IdxValue &&
                    expect2IdxValue == actual2IdxValue &&
                    expect3IdxValue == actual3IdxValue,
                        $"QuantumCircuit should pass the current StateVector to the gate, " +
                        $"expected [ {expect0IdxValue}, {expect1IdxValue}, {expect2IdxValue}, {expect3IdxValue} ]" +
                        $"but found [ {actual0IdxValue}, {actual1IdxValue}, {actual2IdxValue}, {actual3IdxValue} ].");

        }

    }

}
