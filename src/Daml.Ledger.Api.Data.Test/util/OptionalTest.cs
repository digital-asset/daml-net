// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Drawing;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Util.Test
{
    /// <summary>
    /// These 'tests' just demonstrate the examples at http://codinghelmet.com/articles/custom-implementation-of-the-option-maybe-type-in-cs
    /// </summary>
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

        [Fact]
        public void CanCreateSomeWithOf()
        {
            var maybePerson = Optional.Of(_youngPerson);
            maybePerson.Should().BeOfType<Some<Person>>();
            maybePerson.IsPresent.Should().BeTrue();
            bool set = false;
            maybePerson.IfPresent(p => set = true);
            set.Should().BeTrue();
        }

        [Fact]
        public void OfThrowsForNull()
        {
            Person nobody = null;
            Assert.Throws<NullReferenceException>(() => Optional.Of(nobody));
        }

        [Fact]
        public void OfNullableCreatesNoneForNull()
        {
            Person nobody = null;
            var maybePerson = Optional.OfNullable(nobody);
            maybePerson.Should().BeOfType<None<Person>>();
            maybePerson.IsPresent.Should().BeFalse();
            bool set = false;
            maybePerson.IfPresent(p => set = true);
            set.Should().BeFalse();
        }

        [Fact]
        public void CanMapNoneOptional()
        {
            Optional<Car> maybeCar = _youngPerson.TryGetCar(_now);
            maybeCar.Should().BeOfType<None<Car>>();
        }

        [Fact]
        public void CanMapSomeOptional()
        {
            Optional<Car> maybeCar = _olderPerson.TryGetCar(_now);
            maybeCar.Should().BeOfType<Some<Car>>();
        }

        [Fact]
        public void CanMapNoneOptionalFromOptional()
        {
            Optional<Color> maybeColor = _youngPerson.TryGetCar(_now).Map(c => c.Color);
            maybeColor.Should().BeOfType<None<Color>>();
        }

        [Fact]
        public void CanMapSomeOptionalFromOptional()
        {
            Optional<Color> maybeColor = _olderPerson.TryGetCar(_now).Map(c => c.Color);
            maybeColor.Should().BeOfType<Some<Color>>();
        }

        [Fact]
        public void NoneOptionalChainsThroughNoneOptional()
        {
            Optional<Person> somePerson = _youngPerson;
            Optional<Color> maybeColor = somePerson.Map(person => person.TryGetCar(_now)).Map(c => c.Color);
            maybeColor.Should().BeOfType<None<Color>>("as no car");
        }

        [Fact]
        public void SomeOptionalChainsThroughSomeOptional()
        {
            Optional<Person> somePerson = _olderPerson;
            Optional<Color> maybeColor = somePerson.Map(person => person.TryGetCar(_now)).Map(c => c.Color);
            maybeColor.Should().BeOfType<Some<Color>>("as some car");
        }

        [Fact]
        public void InitialNoneResultsInNoneOptional()
        {
            Optional<Person> somePerson = None.Value;
            Optional<Color> maybeColor = somePerson.Map(person => person.TryGetCar(_now)).Map(c => c.Color);
            maybeColor.Should().BeOfType<None<Color>>("as no person");
        }

        [Fact]
        public void CanRetrieveValueOfSomeOptional()
        {
            Color color = _olderPerson.TryGetCar(_now).Map(c => c.Color).Reduce(Color.Black);
            color.Should().Be(Color.Aqua, "as some car");
        }

        [Fact]
        public void CanRetrieveValueOfSomeOptionalWithLambda()
        {
            Color color = _olderPerson.TryGetCar(_now).Map(c => c.Color).Reduce(() => Color.Black);
            color.Should().Be(Color.Aqua, "as some car");
        }

        [Fact]
        public void CanRetrieveValueOfNoneOptional()
        {
            Color color = _youngPerson.TryGetCar(_now).Map(c => c.Color).Reduce(Color.Black);
            color.Should().Be(Color.Black);
        }

        [Fact]
        public void CanRetrieveValueOfNoneOptionalWithLambda()
        {
            Color color = _youngPerson.TryGetCar(_now).Map(c => c.Color).Reduce(() => Color.Black);
            color.Should().Be(Color.Black);
        }

        [Fact]
        public void CanCastOptionalToType()
        {
            Optional<Car> someCar = new Car("car", Color.Red);
            Optional<Car> noCar = None.Value;

            Optional<Vehicle> someVehicle = someCar.OfType<Vehicle>();
            someVehicle.Should().BeOfType<Some<Vehicle>>();

            Optional<Vehicle> noVehicle = noCar.OfType<Vehicle>();
            noVehicle.Should().BeOfType<None<Vehicle>>();

            Optional<Truck> noTruck = someCar.OfType<Truck>();
            noTruck.Should().BeOfType<None<Truck>>();
        }

        [Fact]
        public void CanTestForEquality()
        {
            Optional<Color> red = Color.Red;
            Optional<Color> otherRed = Color.Red;
            Optional<Color> blue = Color.Blue;

            Assert.True(red.GetHashCode() == otherRed.GetHashCode());

            Assert.True(red.Equals(otherRed));
            Assert.True(red == otherRed);
            Assert.False(red != otherRed);

            Assert.False(red.Equals(Color.Red));
            Assert.False(Color.Red.Equals(red));
            Assert.False(red.Equals(blue));
            Assert.False(red == blue);
            Assert.True(red != blue);

            Optional<Color> none = None.Value;
            Optional<Color> otherNone = None.Value;

            Assert.True(none.Equals(otherNone));
            Assert.True(none == otherNone);
            Assert.False(none != otherNone);
            Assert.True(none.Equals(None.Value));
            Assert.True(None.Value.Equals(none));
        }

        [Fact]
        public void CanFilterOptional()
        {
            var maybePerson = Optional.Of(_youngPerson);

            var maybeYoungPerson = maybePerson.Filter(p => p.BirthDate.Year == 2010);
            maybeYoungPerson.Should().BeOfType<Some<Person>>();

            var maybeOlderPerson = maybePerson.Filter(p => p.BirthDate.Year == 1990);
            maybeOlderPerson.Should().BeOfType<None<Person>>();
        }
    }
}
