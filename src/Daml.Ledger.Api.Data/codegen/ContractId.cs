// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Com.Daml.Ledger.Api.Data.Codegen
{
    /**
     * This class is used as a super class for all concrete ContractIds generated
     * by the java codegen with the following properties:
     *
     *<pre>
     * Foo.ContractId fooCid = new Foo.ContractId("test");
     * Bar.ContractId barCid = new Bar.ContractId("test");
     * ContractId&lt;Foo&gt; genericFooCid = new ContractId&lt;&gt;("test");
     * ContractId&lt;Foo&gt; genericBarCid = new ContractId&lt;&gt;("test");
     *
     * fooCid.equals(genericFooCid) == true;
     * genericFooCid.equals(fooCid) == true;
     *
     * fooCid.equals(barCid) == false;
     * barCid.equals(fooCid) == false;
     *</pre>
     *
     * Due to erase, we cannot distinguish ContractId&lt;Foo&gt; from ContractId&lt;Bar&gt;, thus:
     *
     * <pre>
     * fooCid.equals(genericBarCid) == true
     * genericBarCid.equals(fooCid) == true
     *
     * genericFooCid.equals(genericBarCid) == true
     * genericBarCid.equals(genericFooCid) == true
     * </pre>
     *
     * @param <T> A template type
     */

    /// <summary>
    /// As above, but the complication here is type erasure in Java, which reduces every generic class with different type parameters to the same generic class
    /// with object types as parameters, which means that the compiler does the type safety and all generic classes with the same number of type parameters
    /// use the same instance.
    ///
    /// This is used in the java code to check equality using the contract id string member, not matter the type parameter used.
    ///
    /// To do this in C# this type must derive from a common base type in the same manner, so we use an abstract base to get the same functionality
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ContractIdBase
    {
        private readonly string _contractId;

        protected ContractIdBase(string contractId)
        {
            _contractId = contractId;
        }

        public Value ToValue() => new Com.Daml.Ledger.Api.Data.ContractId(_contractId);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (!(obj is ContractIdBase))
                return false;

            var that = (ContractIdBase) obj;

            return _contractId == that._contractId;
        }

        public override int GetHashCode() => _contractId.GetHashCode();

        public override string ToString() => $"ContractId({_contractId})";
    }

    public class ContractId<T> : ContractIdBase
    {
        public ContractId(string contractId)
            : base(contractId)
        { }
    }
}
