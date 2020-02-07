// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    /**
     * A Timestamp value is represented as microseconds since the UNIX epoch.
     *
     * @see Com.DigitalAsset.Ledger.Api.V1.ValueOuterClass.Value#getTimestamp()
     *
     * Instead of the Java Instant class we are using a DateTimeOffset and we lose any precision below Ticks.
     */
    public class Timestamp : Value
    {
        private const long MicrosecondsInaMilliSecond = 1000;
        private const long TicksInaMicrosecond = 10;

        private static readonly DateTimeOffset _unixEpoch = DateTimeOffset.FromUnixTimeSeconds(0);
        
        private readonly long _ticksSinceUnixEpoch;

        /**
         * Constructs a {@link Timestamp} from milliseconds since UNIX epoch.
         *
         * @param millis milliseconds since UNIX epoch.
         */
        public static Timestamp FromMillis(long epochMilliseconds) => new Timestamp(epochMilliseconds * MicrosecondsInaMilliSecond);

        /**
         * Constructs a {@link Timestamp} value from an {@link Instant} up to microsecond precision.
         * This is a lossy conversion as nanoseconds are not preserved.
         */
        public static Timestamp FromDateTimeOffset(DateTimeOffset dateTimeOffset) => new Timestamp(dateTimeOffset);

        public Timestamp(DateTimeOffset dateTimeOffset)
        {
            _ticksSinceUnixEpoch = (dateTimeOffset - _unixEpoch).Ticks;
        }

        /**
         * Constructs a {@link Timestamp} from a microsecond value.
         *
         * @param value The number of microseconds since UNIX epoch.
         */
        public Timestamp(long epochMicroseconds)
        {
            _ticksSinceUnixEpoch = epochMicroseconds * TicksInaMicrosecond;
        }

        /**
         * This is an alias for {@link Timestamp#toInstant()}
         *
         * @return the microseconds stored in this timestamp
         */
        public long Microseconds => _ticksSinceUnixEpoch / TicksInaMicrosecond;

        /**
         * @return The point in time represented by this timestamp as {@link Instant}.
         */
        public DateTimeOffset ToDateTimeOffset() => _unixEpoch + TimeSpan.FromTicks(_ticksSinceUnixEpoch);

        public override Com.DigitalAsset.Ledger.Api.V1.Value ToProto() => new Com.DigitalAsset.Ledger.Api.V1.Value { Timestamp = Microseconds };

        public override string ToString() => $"Timestamp{{value={Microseconds}}}";

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Microseconds == rhs.Microseconds);
        public override int GetHashCode() => unchecked((int)Microseconds);

        public static bool operator ==(Timestamp lhs, Timestamp rhs) => lhs.Compare(rhs);
        public static bool operator !=(Timestamp lhs, Timestamp rhs) => !(lhs == rhs);
    }
} 
