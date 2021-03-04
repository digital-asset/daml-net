// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Util.Test
{
    /// <summary>
    /// These 'tests' just demonstrate the examples at http://codinghelmet.com/articles/custom-implementation-of-the-option-maybe-type-in-cs
    /// </summary>
    public class OptionalExtensionsTest
    {
        private class Person
        {
            private readonly int _age;
            private readonly Color _carColor;

            public Person(string name, int age, Color carColor)
            {
                Name = name;
                _age = age;
                _carColor = carColor;
            }

            public Optional<Car> TryGetCar()
            {
                if (_age >= 18)
                    return new Car("honda", _carColor);
                return None.Value;
            }

            public string Name { get; }
        }

        [Fact]
        public void WillSetSomeIfNotNull()
        {
            Car car = new Car("honda", Color.Aqua);

            Optional<Car> maybeCar = car.NoneIfNull();

            maybeCar.Should().BeOfType<Some<Car>>();
        }

        [Fact]
        public void WillSetNoneIfNull()
        {
            Car car = null;

            Optional<Car> maybeCar = car.NoneIfNull();

            maybeCar.Should().BeOfType<None<Car>>();
        }

        [Fact]
        public void WhenSetsSomeWhenContentMatched()
        {
            var color = Color.Cyan;
            Optional<Color> maybeColor1 = color.When(color == Color.Cyan);
            maybeColor1.Should().BeOfType<Some<Color>>();

            Optional<Color> maybeColor2 = color.When(c => c == Color.Cyan);
            maybeColor2.Should().BeOfType<Some<Color>>();
        }

        [Fact]
        public void WhenSetsNoneIfContentNotMatched()
        {
            var color = Color.Cyan;
            Optional<Color> maybeColor1 = color.When(color == Color.Red);
            maybeColor1.Should().BeOfType<None<Color>>();

            Optional<Color> maybeColor2 = color.When(c => c == Color.Red);
            maybeColor2.Should().BeOfType<None<Color>>();
        }

        [Fact]
        public void FirstOrNoneGetsNoneForEmptySequence()
        {
            IEnumerable<Color> colors = new Color[0];

            Optional<Color> maybeColor = colors.FirstOrNone();
            maybeColor.Should().BeOfType<None<Color>>();
        }

        [Fact]
        public void FirstOrNoneGetSomeForSequence()
        {
            IEnumerable<Color> colors = new[] { Color.Red, Color.Blue };

            Optional<Color> maybeColor = colors.FirstOrNone();
            maybeColor.Should().BeOfType<Some<Color>>();
            maybeColor.Reduce(Color.AliceBlue).Should().Be(Color.Red);
        }

        [Fact]
        public void FirstOrNoneGetNoneForSequenceIfPredicateNotMatched()
        {
            IEnumerable<Color> colors = new[] { Color.Red, Color.Blue };

            Optional<Color> maybeColor = colors.FirstOrNone(c => c == Color.Green);
            maybeColor.Should().BeOfType<None<Color>>();
        }

        [Fact]
        public void CanGetFilteredOptionalSequence()
        {
            IEnumerable<Person> people = new[]
            {
                new Person("Jack", 9, Color.Green), // No car
                new Person("Jill", 19, Color.Red),  // Has a red car
                new Person("Joe", 22, Color.Blue)  // Has a blue car
            };

            IEnumerable<Color> colors = people.SelectOptional(p => p.TryGetCar()).Select(c => c.Color);
            Assert.Equal(2, colors.Count());

            var temp = colors.ToArray();
            temp[0].Should().Be(Color.Red);
            temp[1].Should().Be(Color.Blue);
        }

        [Fact]
        public void CanGetOptionalFromDictionary()
        {
            IEnumerable<Person> people = new[]
            {
                new Person("Jack", 9, Color.Green), // No car
                new Person("Jill", 19, Color.Red),  // Has a red car
                new Person("Joe", 22, Color.Blue)   // Has a blue car
            };

            Dictionary<string, Car> dict = people.SelectOptional(p => p.TryGetCar().Map(car => (name: p.Name, car: car))).ToDictionary(tuple => tuple.name, tuple => tuple.car);

            Optional<Car> jillsCar = dict.TryGetValue("Jill");
            jillsCar.Should().BeOfType<Some<Car>>();
            jillsCar.Map(c => c.Color).Reduce(Color.Black).Should().Be(Color.Red);

            Optional<Car> tomsCar = dict.TryGetValue("Tom");
            tomsCar.Should().BeOfType<None<Car>>();
        }
    }
}
