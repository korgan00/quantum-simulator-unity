using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Tests {

    public class StateVectorTests {

        [Test]
        public void New2QubitStateVectorLengthTest() {

            // Arrange
            StateVector sv;
            int expectLenght = 4;

            // Act
            sv = new StateVector(2);
            int actualLenght = sv.length;

            // Assert
            Assert.IsTrue(expectLenght == actualLenght,
                            $"Initial StateVector length with 2 qubit, " +
                            $"expected {expectLenght}" +
                            $"but found {actualLenght}.");
        }

        [Test]
        public void New10QubitStateVectorLengthTest() {

            // Arrange
            StateVector sv;
            int expectLenght = 1024;

            // Act
            sv = new StateVector(10);
            int actualLenght = sv.length;

            // Assert
            Assert.IsTrue(expectLenght == actualLenght,
                            $"Initial StateVector length with 10 qubit, " +
                            $"expected {expectLenght}" +
                            $"but found {actualLenght}.");
        }

        [Test]
        public void New2QubitStateVectorInitialStateTest() {

            // Arrange
            StateVector sv;
            Complex expect0IdxValue = Complex.One;
            Complex expect1IdxValue = Complex.Zero;
            Complex expect2IdxValue = Complex.Zero;
            Complex expect3IdxValue = Complex.Zero;

            // Act
            sv = new StateVector(2);
            Complex actual0IdxValue = sv[0];
            Complex actual1IdxValue = sv[1];
            Complex actual2IdxValue = sv[2];
            Complex actual3IdxValue = sv[3];

            // Assert
            Assert.IsTrue(expect0IdxValue == actual0IdxValue &&
                        expect1IdxValue == actual1IdxValue &&
                        expect2IdxValue == actual2IdxValue &&
                        expect3IdxValue == actual3IdxValue,
                            $"Initial StateVector state with 2 qubit, " +
                            $"expected [ {expect0IdxValue}, {expect1IdxValue}, {expect2IdxValue}, {expect3IdxValue} ]" +
                            $"but found [ {actual0IdxValue}, {actual1IdxValue}, {actual2IdxValue}, {actual3IdxValue} ].");
        }

        [Test]
        public void ModifyOneValueTest() {

            // Arrange
            StateVector sv = new StateVector(10);
            Complex expectValue = Complex.One;

            // Act
            sv[753] = Complex.One;
            Complex actualValue = sv[753];

            // Assert
            Assert.IsTrue(expectValue == actualValue,
                            $"Modify value in position 753 and then check its value, " +
                            $"expected {expectValue}" +
                            $"but found {actualValue}.");
        }

    }
}
