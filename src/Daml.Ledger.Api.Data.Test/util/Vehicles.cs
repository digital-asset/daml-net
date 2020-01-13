// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Drawing;

namespace Com.Daml.Ledger.NetApi.Test.Util
{
    public abstract class Vehicle
    {
        public Vehicle(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; }
        public Color Color { get; }
    }

    public class Car : Vehicle
    {
        public Car(string name, Color color)
         :base(name, color)
        { 
        }
    }

    public class Truck : Vehicle
    {
        public Truck(string name, Color color)
            : base(name, color)
        {
        }
    }

}