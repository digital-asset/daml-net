// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public abstract class LedgerOffset
    {
        public sealed class LedgerBegin : LedgerOffset
        {
            private LedgerBegin() {}
            public static LedgerBegin Instance { get; } = new LedgerBegin();
            public override string Offset => "LedgerOffset.Begin";
            public override string ToString() => Offset;
        }

        public sealed class LedgerEnd : LedgerOffset
        {
            private LedgerEnd() { }
            public static LedgerEnd Instance { get; } = new LedgerEnd();
            public override string Offset => "LedgerOffset.End";
            public override string ToString() => Offset;
        }

        public sealed class Absolute : LedgerOffset
        {
            public Absolute(string offset)
            {
                if (string.IsNullOrEmpty(offset))
                    throw new ArgumentNullException(offset);
                Offset = offset;
            }

            public override string Offset { get; }
            public override string ToString() => $"LedgerOffset.Absolute({Offset})";
        }

        public abstract string Offset { get; }

        public static LedgerOffset FromProto(Com.DigitalAsset.Ledger.Api.V1.LedgerOffset ledgerOffset)
        {
            switch (ledgerOffset.ValueCase)
            {
                case Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.ValueOneofCase.Absolute:
                    return new Absolute(ledgerOffset.Absolute);
                case Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.ValueOneofCase.Boundary:
                    switch (ledgerOffset.Boundary)
                    {
                        case Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.Types.LedgerBoundary.LedgerBegin:
                            return LedgerBegin.Instance;
                        case Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.Types.LedgerBoundary.LedgerEnd:
                            return LedgerEnd.Instance;
                        default:
                            throw new LedgerBoundaryUnrecognized(ledgerOffset.Boundary);
                    }
                case Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.ValueOneofCase.None:
                default:
                    throw new LedgerBoundaryUnset(ledgerOffset);
            }
        }

        public Com.DigitalAsset.Ledger.Api.V1.LedgerOffset ToProto()
        {
            if (this is LedgerBegin)
                return new Com.DigitalAsset.Ledger.Api.V1.LedgerOffset { Boundary = Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.Types.LedgerBoundary.LedgerBegin };

            if (this is LedgerEnd) 
                return new Com.DigitalAsset.Ledger.Api.V1.LedgerOffset { Boundary = Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.Types.LedgerBoundary.LedgerEnd };

            if (this is Absolute)
            {
                var thisAbsolute = (Absolute) this;

                if (thisAbsolute.Offset == LedgerBegin.Instance.Offset)
                    return new Com.DigitalAsset.Ledger.Api.V1.LedgerOffset { Boundary = Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.Types.LedgerBoundary.LedgerBegin };

                if (thisAbsolute.Offset == LedgerEnd.Instance.Offset)
                    return new Com.DigitalAsset.Ledger.Api.V1.LedgerOffset { Boundary = Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.Types.LedgerBoundary.LedgerEnd };

                return new Com.DigitalAsset.Ledger.Api.V1.LedgerOffset { Absolute = ((Absolute)this).Offset };
            }

            throw new LedgerOffsetUnknown(this);
        }

        public override bool Equals(object obj) => this.Compare(obj, rhs => Offset == rhs.Offset);

        public override int GetHashCode() => Offset.GetHashCode();

        public static bool operator ==(LedgerOffset lhs, LedgerOffset rhs) => lhs.Compare(rhs);
        public static bool operator !=(LedgerOffset lhs, LedgerOffset rhs) => !(lhs == rhs);
    }

    class LedgerBoundaryUnrecognized : Exception
    {
        public LedgerBoundaryUnrecognized(Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.Types.LedgerBoundary boundary)
            : base($"Ledger Boundary unknown {boundary}")
        { 
        }
    }

    class LedgerBoundaryUnset : Exception
    {
        public LedgerBoundaryUnset(Com.DigitalAsset.Ledger.Api.V1.LedgerOffset offset)
         : base($"Ledger Offset unset {offset}")
        {
        }
    }

    class LedgerOffsetUnknown : Exception
    {
        public LedgerOffsetUnknown(LedgerOffset offset)
        : base($"Ledger offset unknown {offset}")
        { 
        }
    }
} 
