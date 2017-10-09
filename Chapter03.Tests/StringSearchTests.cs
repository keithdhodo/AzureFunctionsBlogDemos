using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsFunctions.Shared.Chapter03;

namespace Chapter03.Tests
{
    [TestClass]
    public class StringSearchTests
    {
        [TestMethod]
        public void StringSeach_CountMatches_Simple()
        {
            var searchString = "NopeNopeNope";
            var pattern = "Nope";

            Assert.AreEqual(3, new StringSearch().CountMatches(pattern, searchString));
        }

        [TestMethod]
        public void StringSeach_CountMatches_ParagraphAboutSeattle()
        {
            var searchString = "War work again brought local prosperity during World War II, this time centered on Boeing aircraft. The war dispersed the city's numerous Japanese-American businessmen due to the Japanese American internment. After the war, the local economy dipped. It rose again with Boeing's growing dominance in the commercial airliner market.[50] Seattle celebrated its restored prosperity and made a bid for world recognition with the Century 21 Exposition, the 1962 World's Fair.[51] Another major local economic downturn was in the late 1960s and early 1970s, at a time when Boeing was heavily affected by the oil crises, loss of Government contracts, and costs and delays associated with the Boeing 747. Many people left the area to look for work elsewhere, and two local real estate agents put up a billboard reading \"Will the last person leaving Seattle – Turn out the lights.\"";
            var pattern = "Seattle";

            Assert.AreEqual(2, new StringSearch().CountMatches(pattern, searchString));
        }

        [TestMethod]
        public void StringSeach_Squeeze_EmptyString()
        {
            Assert.AreEqual(string.Empty, new StringSearch().Squeeze(string.Empty));
        }

        [TestMethod]
        public void StringSeach_Squeeze_ShortStringWithLeadingWhitespace()
        {
            var squeezeString = "  random  whitespace  which  will  be squeezed.";

            Assert.AreEqual(" random whitespace which will be squeezed.", new StringSearch().Squeeze(squeezeString));
        }
    }
}
