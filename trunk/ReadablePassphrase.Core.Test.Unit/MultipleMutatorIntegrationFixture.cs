using System.Text;
using AutoFixture;
using MurrayGrant.ReadablePassphrase.Mutators;
using MurrayGrant.ReadablePassphrase.Random;
using MurrayGrant.ReadablePassphrase.TestHelpers;
using NUnit.Framework;

namespace MurrayGrant.ReadablePassphrase
{
    [TestFixture]
    public class MultipleMutatorIntegrationFixture
    {
        [Test]
        public void CombineMutators(
            [Values] UppercaseStyles upperStyle, [Values(1, 2, 3)] int upperCount,
            [Values] NumericStyles numericStyle, [Values(1, 2, 3)] int numberCount,
            [Values] ConstantStyles constantStyle)
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new TestPassphraseBuilder(WordList.Words));
            //  ARRANGE --------------------------------------------------------
            var mutChar = ".";
            //  ReadablePasswordGenerator delimits non-mutated words with spaces
            var inputSource = fixture.Create<TestPassphrase>();
            var input = inputSource.Phrase;
            // Before mutators are applied the phrase will have a trailing whitespace
            var sb = new StringBuilder(input + " ");

            var sut = new IMutator[] {
                new UppercaseMutator { When = upperStyle, NumberOfCharactersToCapitalise = upperCount },
                new NumericMutator { When = numericStyle, NumberOfNumbersToAdd = numberCount },
                new ConstantMutator { When = constantStyle, ValueToAdd = mutChar }
            };

            //  ACT ------------------------------------------------------------
            //  ASSERT ---------------------------------------------------------
            Assert.DoesNotThrow(() =>
            {
                foreach (var m in sut)
                    m.Mutate(sb, new CryptoRandomSource());
            });
        }
    }
}
