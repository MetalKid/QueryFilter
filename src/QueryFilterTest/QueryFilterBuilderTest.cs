#region << Usings >>

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryFilter;
using QueryFilter.Attributes;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilterTest
{
    [TestClass]
    public class QueryFilterBuilderTest
    {

        #region << StringFilter Tests >>

        private void StringFilterTest(StringFilter filter, bool isCaseSensitive, int expectedCount)
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
                new StringEntity {Name = "C"},
            };
            var input = entities.AsQueryable();

            // Act
            var results =
                QueryFilterBuilder<StringEntity, StringFilter>.New(isCaseSensitive).Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(expectedCount, results.Count, "Only " + expectedCount + " item(s) was/were expected.");
        }

        #region << EqualTo >>

        [TestMethod]
        public void StringFilter_Length_EqualTo()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
                new StringEntity {Name = "C"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.NameLength.EqualTo(1);

            // Act
            var results =
                QueryFilterBuilder<StringEntity, StringFilter>.New()
                    //.AddCustomMap(a => a.Name, filter.NameLength)
                    .Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(3, results.Count, "Only 3 item(s) was/were expected.");

        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_EqualTo_NotCaseSensitive_NotIgnoreCase_Null()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo(null, false);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_EqualTo_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo("a", false);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_EqualTo_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo("a", true);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_EqualTo_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo("a", false);
            StringFilterTest(filter, true, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_EqualTo_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo("a", true);
            StringFilterTest(filter, true, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_EqualTo_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo("A", false);
            StringFilterTest(filter, false, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_EqualTo_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo("A", true);
            StringFilterTest(filter, false, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_EqualTo_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo("A", false);
            StringFilterTest(filter, true, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_EqualTo_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EqualTo("A", true);
            StringFilterTest(filter, true, 1);
        }

        #endregion

        #region << NotEqualTo >>

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEqualTo_NotCaseSensitive_NotIgnoreCase_Null()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo(null, false);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEqualTo_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo("a", false);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEqualTo_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo("a", true);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEqualTo_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo("a", false);
            StringFilterTest(filter, true, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEqualTo_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo("a", true);
            StringFilterTest(filter, true, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotEqualTo_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo("A", false);
            StringFilterTest(filter, false, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotEqualTo_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo("A", true);
            StringFilterTest(filter, false, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotEqualTo_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo("A", false);
            StringFilterTest(filter, true, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotEqualTo_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEqualTo("A", true);
            StringFilterTest(filter, true, 2);
        }

        #endregion

        #region << Contains >>

        [TestMethod]
        public void StringFilter_LowerCaseValue_Contains_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.Contains("a", false);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_Contains_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.Contains("a", true);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_Contains_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.Contains("a", false);
            StringFilterTest(filter, true, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_Contains_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.Contains("a", true);
            StringFilterTest(filter, true, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_Contains_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.Contains("A", false);
            StringFilterTest(filter, false, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_Contains_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.Contains("A", true);
            StringFilterTest(filter, false, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_Contains_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.Contains("A", false);
            StringFilterTest(filter, true, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_Contains_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.Contains("A", true);
            StringFilterTest(filter, true, 1);
        }

        #endregion

        #region << NotContains >>

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotContains_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotContains("a", false);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotContains_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotContains("a", true);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotContains_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotContains("a", false);
            StringFilterTest(filter, true, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotContains_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotContains("a", true);
            StringFilterTest(filter, true, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotContains_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotContains("A", false);
            StringFilterTest(filter, false, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotContains_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotContains("A", true);
            StringFilterTest(filter, false, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotContains_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotContains("A", false);
            StringFilterTest(filter, true, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotContains_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotContains("A", true);
            StringFilterTest(filter, true, 2);
        }

        #endregion

        #region << StartsWith >>

        [TestMethod]
        public void StringFilter_LowerCaseValue_StartsWith_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.StartsWith("a", false);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_StartsWith_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.StartsWith("a", true);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_StartsWith_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.StartsWith("a", false);
            StringFilterTest(filter, true, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_StartsWith_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.StartsWith("a", true);
            StringFilterTest(filter, true, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_StartsWith_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.StartsWith("A", false);
            StringFilterTest(filter, false, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_StartsWith_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.StartsWith("A", true);
            StringFilterTest(filter, false, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_StartsWith_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.StartsWith("A", false);
            StringFilterTest(filter, true, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_StartsWith_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.StartsWith("A", true);
            StringFilterTest(filter, true, 1);
        }

        #endregion

        #region << NotStartsWith >>

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotStartsWith_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotStartsWith("a", false);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotStartsWith_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotStartsWith("a", true);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotStartsWith_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotStartsWith("a", false);
            StringFilterTest(filter, true, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotStartsWith_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotStartsWith("a", true);
            StringFilterTest(filter, true, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotStartsWith_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotStartsWith("A", false);
            StringFilterTest(filter, false, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotStartsWith_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotStartsWith("A", true);
            StringFilterTest(filter, false, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotStartsWith_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotStartsWith("A", false);
            StringFilterTest(filter, true, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotStartsWith_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotStartsWith("A", true);
            StringFilterTest(filter, true, 2);
        }

        #endregion

        #region << EndsWith >>

        [TestMethod]
        public void StringFilter_LowerCaseValue_EndsWith_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EndsWith("a", false);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_EndsWith_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EndsWith("a", true);
            StringFilterTest(filter, false, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_EndsWith_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EndsWith("a", false);
            StringFilterTest(filter, true, 0);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_EndsWith_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EndsWith("a", true);
            StringFilterTest(filter, true, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_EndsWith_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EndsWith("A", false);
            StringFilterTest(filter, false, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_EndsWith_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EndsWith("A", true);
            StringFilterTest(filter, false, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_EndsWith_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EndsWith("A", false);
            StringFilterTest(filter, true, 1);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_EndsWith_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.EndsWith("A", true);
            StringFilterTest(filter, true, 1);
        }

        #endregion

        #region << NotEndsWith >>

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEndsWith_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEndsWith("a", false);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEndsWith_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEndsWith("a", true);
            StringFilterTest(filter, false, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEndsWith_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEndsWith("a", false);
            StringFilterTest(filter, true, 3);
        }

        [TestMethod]
        public void StringFilter_LowerCaseValue_NotEndsWith_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEndsWith("a", true);
            StringFilterTest(filter, true, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotEndsWith_NotCaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEndsWith("A", false);
            StringFilterTest(filter, false, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotEndsWith_NotCaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEndsWith("A", true);
            StringFilterTest(filter, false, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotEndsWith_CaseSensitive_NotIgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEndsWith("A", false);
            StringFilterTest(filter, true, 2);
        }

        [TestMethod]
        public void StringFilter_UpperCaseValue_NotEndsWith_CaseSensitive_IgnoreCase()
        {
            var filter = new StringFilter();
            filter.Name.NotEndsWith("A", true);
            StringFilterTest(filter, true, 2);
        }

        #endregion

        #endregion

        #region << EquatableFilter Tests >>

        private void EquatableFilterTest(EquatableFilter filter, int expectedCount)
        {
            // Arrange
            IList<EquatableEntity> entities = new List<EquatableEntity>()
            {
                new EquatableEntity {IsActive = true},
                new EquatableEntity {IsActive = false},
                new EquatableEntity {IsActive = false},
            };
            var input = entities.AsQueryable();

            // Act
            var results =
                QueryFilterBuilder<EquatableEntity, EquatableFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(expectedCount, results.Count, "Only " + expectedCount + " item(s) was/were expected.");
        }

        #region << EqualTo >>

        [TestMethod]
        public void EquatableFilter_EqualTo_True()
        {
            var filter = new EquatableFilter();
            filter.Test.EqualTo(true);
            EquatableFilterTest(filter, 1);
        }

        [TestMethod]
        public void EquatableFilter_EqualTo_False()
        {
            var filter = new EquatableFilter();
            filter.Test.EqualTo(false);
            EquatableFilterTest(filter, 2);
        }

        #endregion

        #region << NotEqualTo >>

        [TestMethod]
        public void EquatableFilter_NotEqualTo_True()
        {
            var filter = new EquatableFilter();
            filter.Test.NotEqualTo(true);
            EquatableFilterTest(filter, 2);
        }

        [TestMethod]
        public void EquatableFilter_NotEqualTo_False()
        {
            var filter = new EquatableFilter();
            filter.Test.NotEqualTo(false);
            EquatableFilterTest(filter, 1);
        }

        #endregion

        #endregion

        #region << RangeFilter Tests >>

        private void RangeFilterTest(RangeFilter filter, int expectedCount)
        {
            // Arrange
            IList<RangeEntity> entities = new List<RangeEntity>()
            {
                new RangeEntity {Test = 1},
                new RangeEntity {Test = 2},
                new RangeEntity {Test = 3},
            };
            var input = entities.AsQueryable();

            // Act
            var results =
                QueryFilterBuilder<RangeEntity, RangeFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(expectedCount, results.Count, "Only " + expectedCount + " item(s) was/were expected.");
        }

        #region << EqualTo >>

        [TestMethod]
        public void RangeFilter_EqualTo_1()
        {
            var filter = new RangeFilter();
            filter.Test.EqualTo(1);
            RangeFilterTest(filter, 1);
        }

        [TestMethod]
        public void RangeFilter_EqualTo_0()
        {
            var filter = new RangeFilter();
            filter.Test.EqualTo(0);
            RangeFilterTest(filter, 0);
        }

        #endregion

        #region << NotEqualTo >>

        [TestMethod]
        public void RangeFilter_NotEqualTo_1()
        {
            var filter = new RangeFilter();
            filter.Test.NotEqualTo(1);
            RangeFilterTest(filter, 2);
        }

        [TestMethod]
        public void RangeFilter_NotEqualTo_0()
        {
            var filter = new RangeFilter();
            filter.Test.NotEqualTo(0);
            RangeFilterTest(filter, 3);
        }

        #endregion

        #region << GreaterThan >>

        [TestMethod]
        public void RangeFilter_GreaterThan_1()
        {
            var filter = new RangeFilter();
            filter.Test.GreaterThan(1);
            RangeFilterTest(filter, 2);
        }

        [TestMethod]
        public void RangeFilter_GreaterThan_0()
        {
            var filter = new RangeFilter();
            filter.Test.GreaterThan(0);
            RangeFilterTest(filter, 3);
        }

        #endregion

        #region << GreaterThanOrEqualTo >>

        [TestMethod]
        public void RangeFilter_GreaterThanOrEqualTo_1()
        {
            var filter = new RangeFilter();
            filter.Test.GreaterThanOrEqualTo(1);
            RangeFilterTest(filter, 3);
        }

        [TestMethod]
        public void RangeFilter_GreaterThanOrEqualTo_0()
        {
            var filter = new RangeFilter();
            filter.Test.GreaterThanOrEqualTo(0);
            RangeFilterTest(filter, 3);
        }

        #endregion

        #region << LessThan >>

        [TestMethod]
        public void RangeFilter_LessThan_3()
        {
            var filter = new RangeFilter();
            filter.Test.LessThan(3);
            RangeFilterTest(filter, 2);
        }

        [TestMethod]
        public void RangeFilter_LessThan_4()
        {
            var filter = new RangeFilter();
            filter.Test.LessThan(4);
            RangeFilterTest(filter, 3);
        }

        #endregion

        #region << LessThanOrEqualTo >>

        [TestMethod]
        public void RangeFilter_LessThanOrEqualTo_3()
        {
            var filter = new RangeFilter();
            filter.Test.LessThanOrEqualTo(3);
            RangeFilterTest(filter, 3);
        }

        [TestMethod]
        public void RangeFilter_LessThanOrEqualTo_4()
        {
            var filter = new RangeFilter();
            filter.Test.LessThanOrEqualTo(4);
            RangeFilterTest(filter, 3);
        }

        #endregion

        #endregion

        #region << GroupFilter Tests >>

        [TestMethod]
        public void GroupFilter_EqualTo_2()
        {
            // Arrange
            IList<GroupEntity> entities = new List<GroupEntity>()
            {
                new GroupEntity {Test = 1, Name = "abcdefg"},
                new GroupEntity {Test = 2, Name = "fghijkz"},
                new GroupEntity {Test = 3, Name = "jklmnop"},
            };
            var input = entities.AsQueryable();
            var filter = new GroupFilter();
            
            filter.AddGroups(
                FilterGroup.New(FilterGroupTypeEnum.Or, 
                filter.Name.Contains("abc"), 
                filter.Name.Contains("z")));

            // Act
            var results = QueryFilterBuilder<GroupEntity, GroupFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }
        #endregion

        #region << AddCustomMap Tests >>

        [TestMethod]
        public void AddCustomMap_EqualTo_1()
        {
            // Arrange
            IList<CustomMapEntity> entities = new List<CustomMapEntity>()
            {
                new CustomMapEntity {Test = "A"},
                new CustomMapEntity {Test = "B"},
                new CustomMapEntity {Test = "C"},
            };
            var input = entities.AsQueryable();
            var filter = new CustomMapFilter();
            filter.Test.EqualTo("A");

            // Act
            var results = QueryFilterBuilder<CustomMapEntity, CustomMapFilter>.New()
                .AddCustomMap(a => a.Test, filter.Test)
                .Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        #endregion

        #region << Complex Filter >>

        [TestMethod]
        public void ComplexFilter_EqualTo_1()
        {
            // Arrange
            IList<ComplexEntity> entities = new List<ComplexEntity>()
            {
                new ComplexEntity {Test = "A", Entity = new ComplexEntity2 { A = "1"}},
                new ComplexEntity {Test = "B", Entity = new ComplexEntity2 { A = "2"}},
                new ComplexEntity {Test = "C", Entity = new ComplexEntity2 { A = "3"}},
            };
            var input = entities.AsQueryable();
            var filter = new ComplexFilter();
            filter.Test.EqualTo("A");
            filter.A.EqualTo("1");

            // Act
            var results = QueryFilterBuilder<ComplexEntity, ComplexFilter>.New()
                .AddCustomMap(a => a.Entity.A, filter.A)
                .Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void ComplexFilter2_EqualTo_1()
        {
            // Arrange
            IList<ComplexEntity> entities = new List<ComplexEntity>()
            {
                new ComplexEntity {Test = "A", Entity = new ComplexEntity2 { A = "1"}},
                new ComplexEntity {Test = "B", Entity = new ComplexEntity2 { A = "2"}},
                new ComplexEntity {Test = "C", Entity = new ComplexEntity2 { A = "3"}},
            };
            var input = entities.AsQueryable();
            var filter = new ComplexFilter2();
            filter.Test.EqualTo("A");
            filter.A.EqualTo("1");

            // Act
            var results = QueryFilterBuilder<ComplexEntity, ComplexFilter2>.New()
                .Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void ComplexFilter2_Grouping()
        {
            // Arrange
            IList<ComplexEntity> entities = new List<ComplexEntity>()
            {
                new ComplexEntity {Test = "A", Entity = new ComplexEntity2 { A = "1"}},
                new ComplexEntity {Test = "B", Entity = new ComplexEntity2 { A = "2"}},
                new ComplexEntity {Test = "C", Entity = new ComplexEntity2 { A = "3"}},
            };
            var input = entities.AsQueryable();
            var filter = new ComplexFilter2();
            filter.AddGroup(
                FilterGroup.New(
                    FilterGroupTypeEnum.And, 
                    filter.Test.EqualTo("A"), 
                    filter.Test.NotEqualTo("C")),
                    FilterGroup.New(
                        FilterGroupTypeEnum.Or,
                        FilterGroup.New(
                            FilterGroupTypeEnum.And,
                            filter.Test.EqualTo("B")
                        ))
                );

            // Act
            var results = QueryFilterBuilder<ComplexEntity, ComplexFilter2>.New()
                .Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 item was expected.");
        }

        #endregion

        #region << Private Classes >>


        private class ComplexEntity
        {
            public string Test { get; set; }
            public ComplexEntity2 Entity { get; set; }
        }
        private class ComplexEntity2
        {
            public string A { get; set; }
        }
        private class ComplexFilter
        {
            [MapToProperty]
            public FilterString Test { get; set; }
            
            public FilterString A { get; set;}

            public ComplexFilter()
            {
                Test = new FilterString();
                A = new FilterString();
            }
        }
        private class ComplexFilter2 : IFilterGroup
        {
            [MapToProperty]
            public FilterString Test { get; set; }

            [MapToProperty("Entity.A")]
            public FilterString A { get; set; }

            public IList<FilterGroup> FilterGroups { get; set; }

            public ComplexFilter2()
            {
                Test = new FilterString();
                A = new FilterString();
                FilterGroups = new List<FilterGroup>();
            }

            public void AddGroup(params FilterGroup[] groups)
            {
                foreach (var item in groups)
                {
                    FilterGroups.Add(item);
                }
            }

        }
        private class CustomMapEntity
        {
            public string Test { get; set; }
        }
        private class CustomMapFilter
        {
            public FilterString Test { get; set; }

            public CustomMapFilter()
            {
                Test = new FilterString();
            }
        }

        private class GroupEntity
        {
            public int Test { get; set; }
            public string Name { get; set; }
        }
        private class GroupFilter : IFilterGroup
        {
            [MapToProperty]
            public FilterRange<int> Test { get; set; }

            [MapToProperty]
            public FilterString Name { get; set; }

            public IList<FilterGroup> FilterGroups { get; set; }

            public GroupFilter()
            {
                Test = new FilterRange<int>();
                Name = new FilterString();
                FilterGroups = new List<FilterGroup>();
            }

            public void AddGroups(params FilterGroup[] items)
            {
                if (items == null || items.Length == 0 || !items.Any())
                {
                    throw new InvalidOperationException("At least 1 statement is required.");
                }
                foreach (var item in items)
                {
                    FilterGroups.Add(item);
                }
            }
        }

        private class RangeEntity
        {
            public int Test { get; set; }
        }
        private class RangeFilter
        {
            [MapToProperty]
            public FilterRange<int> Test { get; set; }

            public RangeFilter()
            {
                Test = new FilterRange<int>();
            }
        }

        private class EquatableEntity
        {
            public bool IsActive { get; set; }
        }
        private class EquatableFilter
        {
            [MapToProperty("IsActive")]
            public FilterEquatable<bool> Test { get; set; }

            public EquatableFilter()
            {
                Test = new FilterEquatable<bool>();
            }
        }

        private class StringEntity
        {
            public string Name { get; set; }
        }
        private class StringFilter
        {
            [MapToProperty]
            public FilterString Name { get; set; }

            [MapToProperty("Name")]
            public FilterRange<int> NameLength { get; set; }

            public StringFilter()
            {
                Name = new FilterString();
                NameLength = new FilterRange<int>();
            }
        }

        #endregion

    }
}
