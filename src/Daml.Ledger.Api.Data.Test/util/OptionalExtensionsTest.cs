// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Util.Test
{
    /// <summary>
    /// These 'tests' just demonstrate the examples at http://codinghelmet.com/articles/custom-implementation-of-the-option-maybe-type-in-cs
    /// </summary>
    [TestFixture]
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

        [Test]
        public void WillSetSomeIfNotNull()
        {
            Car car = new Car("honda", Color.Aqua);

            Optional<Car> maybeCar = car.NoneIfNull();

            Assert.AreEqual(typeof(Some<Car>), maybeCar.GetType());
        }

        [Test]
        public void WillSetNoneIfNull()
        {
            Car car = null;

            Optional<Car> maybeCar = car.NoneIfNull();

            Assert.AreEqual(typeof(None<Car>), maybeCar.GetType());
        }

        [Test]
        public void WhenSetsSomeWhenContentMatched()
        {
            var color = Color.Cyan;
            Optional<Color> maybeColor1 = color.When(color == Color.Cyan);
            Assert.AreEqual(typeof(Some<Color>), maybeColor1.GetType());

            Optional<Color> maybeColor2 = color.When(c => c == Color.Cyan);
            Assert.AreEqual(typeof(Some<Color>), maybeColor2.GetType());
        }

        [Test]
        public void WhenSetsNoneIfContentNotMatched()
        {
            var color = Color.Cyan;
            Optional<Color> maybeColor1 = color.When(color == Color.Red);
            Assert.AreEqual(typeof(None<Color>), maybeColor1.GetType());

            Optional<Color> maybeColor2 = color.When(c => c == Color.Red);
            Assert.AreEqual(typeof(None<Color>), maybeColor2.GetType());
        }

        [Test]
        public void FirstOrNoneGetsNoneForEmptySequence()
        {
            IEnumerable<Color> colors = new Color[0];

            Optional<Color> maybeColor = colors.FirstOrNone();
            Assert.AreEqual(typeof(None<Color>), maybeColor.GetType());
        }

        [Test]
        public void FirstOrNoneGetSomeForSequence()
        {
            IEnumerable<Color> colors = new[] { Color.Red, Color.Blue};

            Optional<Color> maybeColor = colors.FirstOrNone();
            Assert.AreEqual(typeof(Some<Color>), maybeColor.GetType());
            Assert.AreEqual(Color.Red, maybeColor.Reduce(Color.AliceBlue));
        }

        [Test]
        public void FirstOrNoneGetNoneForSequenceIfPredicateNotMatched()
        {
            IEnumerable<Color> colors = new[] { Color.Red, Color.Blue };

            Optional<Color> maybeColor = colors.FirstOrNone(c => c == Color.Green);
            Assert.AreEqual(typeof(None<Color>), maybeColor.GetType());
        }

        [Test]
        public void CanGetFilteredOptionalSequence()
        {
            IEnumerable<Person> people = new[]
            {
                new Person("Jack", 9, Color.Green), // No car
                new Person("Jill", 19, Color.Red),  // Has a red car
                new Person("Joe", 22, Color.Blue)  // Has a blue car
            };

            IEnumerable<Color> colors = people.SelectOptional(p => p.TryGetCar()).Select(c => c.Color);
            Assert.AreEqual(2, colors.Count());

            var temp = colors.ToArray();
            Assert.AreEqual(Color.Red, temp[0]);
            Assert.AreEqual(Color.Blue, temp[1]);
        }

        [Test]
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
            Assert.AreEqual(typeof(Some<Car>), jillsCar.GetType());
            Assert.AreEqual(Color.Red, jillsCar.Map(c => c.Color).Reduce(Color.Black));

            Optional<Car> tomsCar = dict.TryGetValue("Tom");
            Assert.AreEqual(typeof(None<Car>), tomsCar.GetType());
        }
    }
}
