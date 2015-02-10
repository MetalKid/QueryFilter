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

        [TestMethod]
        public void StringFilter_EqualTo_1of3Returned()
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
            filter.Name.EqualTo("A");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void StringFilter_EqualTo_2of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
                new StringEntity {Name = "B"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.EqualTo("B");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        [TestMethod]
        public void StringFilter_NotEqualTo_1of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "A"},
                new StringEntity {Name = "C"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.NotEqualTo("A");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void StringFilter_NotEqualTo_2of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.NotEqualTo("B");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        [TestMethod]
        public void StringFilter_Contains_1of3Returned()
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
            filter.Name.Contains("A");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void StringFilter_Contains_2of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
                new StringEntity {Name = "B"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.Contains("B");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        [TestMethod]
        public void StringFilter_NotContains_1of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "A"},
                new StringEntity {Name = "C"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.NotContains("A");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void StringFilter_NotContains_2of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.NotContains("B");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        [TestMethod]
        public void StringFilter_StartsWith_1of3Returned()
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
            filter.Name.StartsWith("A");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void StringFilter_StartsWith_2of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
                new StringEntity {Name = "B"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.StartsWith("B");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        [TestMethod]
        public void StringFilter_NotStartsWith_1of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "A"},
                new StringEntity {Name = "C"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.NotStartsWith("A");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void StringFilter_NotStartsWith_2of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.NotStartsWith("B");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        [TestMethod]
        public void StringFilter_EndsWith_1of3Returned()
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
            filter.Name.EndsWith("A");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void StringFilter_EndsWith_2of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
                new StringEntity {Name = "B"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.EndsWith("B");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        [TestMethod]
        public void StringFilter_NotEndsWith_1of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "A"},
                new StringEntity {Name = "C"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.NotEndsWith("A");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void StringFilter_NotEndsWith_2of3Returned()
        {
            // Arrange
            IList<StringEntity> entities = new List<StringEntity>()
            {
                new StringEntity {Name = "A"},
                new StringEntity {Name = "A"},
                new StringEntity {Name = "B"},
            };
            var input = entities.AsQueryable();
            var filter = new StringFilter();
            filter.Name.NotEndsWith("B");

            // Act
            var results = QueryFilterBuilder<StringEntity, StringFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        #endregion

        #region << EquatableFilter Tests >>

        [TestMethod]
        public void EquatableFilter_EqualTo_1of3Returned()
        {
            // Arrange
            IList<EquatableEntity> entities = new List<EquatableEntity>()
            {
                new EquatableEntity {IsActive = true},
                new EquatableEntity {IsActive = false},
                new EquatableEntity {IsActive = false},
            };
            var input = entities.AsQueryable();
            var filter = new EquatableFilter();
            filter.Test.EqualTo(true);

            // Act
            var results = QueryFilterBuilder<EquatableEntity, EquatableFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void EquatableFilter_EqualTo_2of3Returned()
        {
            // Arrange
            IList<EquatableEntity> entities = new List<EquatableEntity>()
            {
             new EquatableEntity {IsActive = true},
                new EquatableEntity {IsActive = false},
                new EquatableEntity {IsActive = false},
            };
            var input = entities.AsQueryable();
            var filter = new EquatableFilter();
            filter.Test.EqualTo(false);

            // Act
            var results = QueryFilterBuilder<EquatableEntity, EquatableFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        [TestMethod]
        public void EquatableFilter_NotEqualTo_1of3Returned()
        {
            // Arrange
            IList<EquatableEntity> entities = new List<EquatableEntity>()
            {
                new EquatableEntity {IsActive = true},
                new EquatableEntity {IsActive = true},
                new EquatableEntity {IsActive = false},
            };
            var input = entities.AsQueryable();
            var filter = new EquatableFilter();
            filter.Test.NotEqualTo(true);

            // Act
            var results = QueryFilterBuilder<EquatableEntity, EquatableFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        [TestMethod]
        public void EquatableFilter_NotEqualTo_2of3Returned()
        {
            // Arrange
            IList<EquatableEntity> entities = new List<EquatableEntity>()
            {
                new EquatableEntity {IsActive = true},
                new EquatableEntity {IsActive = true},
                new EquatableEntity {IsActive = false},
            };
            var input = entities.AsQueryable();
            var filter = new EquatableFilter();
            filter.Test.NotEqualTo(false);

            // Act
            var results = QueryFilterBuilder<EquatableEntity, EquatableFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(2, results.Count, "Only 2 items were expected.");
        }

        #endregion

        #region << RangeFilter Tests >>

        [TestMethod]
        public void RangeFilter_EqualTo_1of3Returned()
        {
            // Arrange
            IList<RangeEntity> entities = new List<RangeEntity>()
            {
                new RangeEntity {Test = 1},
                new RangeEntity {Test = 2},
                new RangeEntity {Test = 3},
            };
            var input = entities.AsQueryable();
            var filter = new RangeFilter();
            filter.Test.EqualTo(1);

            // Act
            var results = QueryFilterBuilder<RangeEntity, RangeFilter>.New().Build(input, filter).ToList();

            // Assert
            Assert.AreEqual(1, results.Count, "Only 1 item was expected.");
        }

        #endregion

        #region << GroupFilter Tests >>


        [TestMethod]
        public void GroupFilter_EqualTo_2of3Returned()
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
        public void AddCustomMap_EqualTo_1of3Returned()
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
        public void ComplexFilter_EqualTo_1of3Returned()
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

            public StringFilter()
            {
                Name = new FilterString();
            }
        }

        #endregion
    }
}
