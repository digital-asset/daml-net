// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Drawing;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Util.Test
{
    /// <summary>
    /// These 'tests' just demonstrate the examples at http://codinghelmet.com/articles/custom-implementation-of-the-option-maybe-type-in-cs
    /// </summary>
    [TestFixture]
    public class OptionalTest
    {
        private readonly Person _youngPerson = new Person(new DateTime(2010, 2, 20));
        private readonly Person _olderPerson = new Person(new DateTime(1990, 2, 20));
        private readonly DateTime _now = new DateTime(2019, 2, 20);

        private class Person
        {
            public DateTime BirthDate { get; }

            public Person(DateTime birthDate)
            {
                BirthDate = birthDate.Date;
            }

            public Optional<Car> TryGetCar(DateTime at)
            {
                if (BirthDate.AddYears(18) <= at)
                    return new Car("honda", Color.Aqua);
                return None.Value;
            }
        }

        [Test]
        public void CanCreateSomeWithOf()
        {
            var maybePerson = Optional.Of(_youngPerson);
            Assert.AreEqual(typeof(Some<Person>), maybePerson.GetType());
            Assert.IsTrue(maybePerson.IsPresent);
            bool set = false;
            maybePerson.IfPresent(p => set = true);
            Assert.IsTrue(set);
        }

        [Test]
        public void OfThrowsForNull()
        {
            Person nobody = null;
            Assert.Throws<NullReferenceException>(() => Optional.Of(nobody));
        }

        [Test]
        public void OfNullableCreatesNoneForNull()
        {
            Person nobody = null;
            var maybePerson = Optional.OfNullable(nobody);
            Assert.AreEqual(typeof(None<Person>), maybePerson.GetType());
            Assert.IsFalse(maybePerson.IsPresent);
            bool set = false;
            maybePerson.IfPresent(p => set = true);
            Assert.IsFalse(set);
        }

        [Test]
        public void CanMapNoneOptional()
        {
            Optional<Car> maybeCar = _youngPerson.TryGetCar(_now);

            Assert.AreEqual(typeof(None<Car>), maybeCar.GetType());
        }

        [Test]
        public void CanMapSomeOptional()
        {
            Optional<Car> maybeCar = _olderPerson.TryGetCar(_now);

            Assert.AreEqual(typeof(Some<Car>), maybeCar.GetType());
        }

        [Test]
        public void CanMapNoneOptionalFromOptional()
        {
            Optional<Color> maybeColor = _youngPerson.TryGetCar(_now).Map(c => c.Color);

            Assert.AreEqual(typeof(None<Color>), maybeColor.GetType());
        }

        [Test]
        public void CanMapSomeOptionalFromOptional()
        {
            Optional<Color> maybeColor = _olderPerson.TryGetCar(_now).Map(c => c.Color);

            Assert.AreEqual(typeof(Some<Color>), maybeColor.GetType());
        }

        [Test]
        public void NoneOptionalChainsThroughNoneOptional()
        {
            Optional<Person> somePerson = _youngPerson;

            Optional<Color> maybeColor = somePerson.Map(person => person.TryGetCar(_now)).Map(c => c.Color);

            Assert.AreEqual(typeof(None<Color>), maybeColor.GetType(), "Shouldn't have got color as no car");
        }

        [Test]
        public void SomeOptionalChainsThroughSomeOptional()
        {
            Optional<Person> somePerson = _olderPerson;

            Optional<Color> maybeColor = somePerson.Map(person => person.TryGetCar(_now)).Map(c => c.Color);

            Assert.AreEqual(typeof(Some<Color>), maybeColor.GetType(), "Should have got color as some car");
        }

        [Test]
        public void InitialNoneResultsInNoneOptional()
        {
            Optional<Person> somePerson = None.Value;

            Optional<Color> maybeColor = somePerson.Map(person => person.TryGetCar(_now)).Map(c => c.Color);

            Assert.AreEqual(typeof(None<Color>), maybeColor.GetType(), "Shouldn't have got color as no person");
        }

        [Test]
        public void CanRetrieveValueOfSomeOptional()
        {
            Color color = _olderPerson.TryGetCar(_now).Map(c => c.Color).Reduce(Color.Black);

            Assert.AreEqual(Color.Aqua, color, "Should have got color as some car");
        }

        [Test]
        public void CanRetrieveValueOfSomeOptionalWithLambda()
        {
            Color color = _olderPerson.TryGetCar(_now).Map(c => c.Color).Reduce(() => Color.Black);

            Assert.AreEqual(Color.Aqua, color, "Should have got color as some car");
        }

        [Test]
        public void CanRetrieveValueOfNoneOptional()
        {
            Color color = _youngPerson.TryGetCar(_now).Map(c => c.Color).Reduce(Color.Black);

            Assert.AreEqual(Color.Black, color);
        }

        [Test]
        public void CanRetrieveValueOfNoneOptionalWithLambda()
        {
            Color color = _youngPerson.TryGetCar(_now).Map(c => c.Color).Reduce(() => Color.Black);

            Assert.AreEqual(Color.Black, color);
        }

        [Test]
        public void CanCastOptionalToType()
        {
            Optional<Car> someCar = new Car("car", Color.Red);
            Optional<Car> noCar = None.Value;

            Optional<Vehicle> someVehicle = someCar.OfType<Vehicle>();
            Assert.AreEqual(typeof(Some<Vehicle>), someVehicle.GetType());

            Optional<Vehicle> noVehicle = noCar.OfType<Vehicle>();
            Assert.AreEqual(typeof(None<Vehicle>), noVehicle.GetType());

            Optional<Truck> noTruck = someCar.OfType<Truck>();
            Assert.AreEqual(typeof(None<Truck>), noTruck.GetType());
        }

        [Test]
        public void CanTestForEquality()
        {
            Optional<Color> red = Color.Red;
            Optional<Color> otherRed = Color.Red;
            Optional<Color> blue = Color.Blue;

            Assert.IsTrue(red.GetHashCode() == otherRed.GetHashCode());

            Assert.IsTrue(red.Equals(otherRed));
            Assert.IsTrue(red == otherRed);
            Assert.IsFalse(red != otherRed);

            Assert.IsFalse(red.Equals(Color.Red));
            Assert.IsFalse(Color.Red.Equals(red));
            Assert.IsFalse(red.Equals(blue));
            Assert.IsFalse(red == blue);
            Assert.IsTrue(red != blue);

            Optional<Color> none = None.Value;
            Optional<Color> otherNone = None.Value;

            Assert.IsTrue(none.Equals(otherNone));
            Assert.IsTrue(none == otherNone);
            Assert.IsFalse(none != otherNone);
            Assert.IsTrue(none.Equals(None.Value));
            Assert.IsTrue(None.Value.Equals(none));
        }

        [Test]
        public void CanFilterOptional()
        {
            var maybePerson = Optional.Of(_youngPerson);

            var maybeYoungPerson = maybePerson.Filter(p => p.BirthDate.Year == 2010);
            Assert.AreEqual(typeof(Some<Person>), maybeYoungPerson.GetType());

            var maybeOlderPerson = maybePerson.Filter(p => p.BirthDate.Year == 1990);
            Assert.AreEqual(typeof(None<Person>), maybeOlderPerson.GetType());
        }
    }
}
