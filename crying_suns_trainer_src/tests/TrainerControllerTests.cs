using System;
using CryingSunsTrainer.Core;
using Moq;
using Xunit;

namespace CryingSunsTrainer.Tests
{
    /// <summary>
    /// Unit tests for TrainerController to verify cheat toggling and actions.
    /// Uses Moq to mock MemoryManager dependencies.
    /// </summary>
    public class TrainerControllerTests
    {
        [Fact]
        public void ToggleInfiniteHull_ShouldActivateAndWriteHull()
        {
            // Arrange
            var mockMemory = new Mock<MemoryManager>(It.IsAny<System.Diagnostics.Process>());
            mockMemory.Setup(m => m.WriteHull(It.IsAny<int>()));
            var controller = new TrainerController(mockMemory.Object);

            // Act
            controller.ToggleInfiniteHull();

            // Assert
            Assert.True(controller.IsInfiniteHullActive);
            mockMemory.Verify(m => m.WriteHull(9999), Times.Once);
        }

        [Fact]
        public void ToggleInfiniteHull_Twice_ShouldDeactivate()
        {
            // Arrange
            var mockMemory = new Mock<MemoryManager>(It.IsAny<System.Diagnostics.Process>());
            mockMemory.Setup(m => m.WriteHull(It.IsAny<int>()));
            var controller = new TrainerController(mockMemory.Object);

            // Act
            controller.ToggleInfiniteHull();
            controller.ToggleInfiniteHull();

            // Assert
            Assert.False(controller.IsInfiniteHullActive);
            mockMemory.Verify(m => m.WriteHull(9999), Times.Once);
        }

        [Fact]
        public void ToggleInfiniteEnergy_ShouldActivateAndWriteEnergy()
        {
            // Arrange
            var mockMemory = new Mock<MemoryManager>(It.IsAny<System.Diagnostics.Process>());
            mockMemory.Setup(m => m.WriteEnergy(It.IsAny<int>()));
            var controller = new TrainerController(mockMemory.Object);

            // Act
            controller.ToggleInfiniteEnergy();

            // Assert
            Assert.True(controller.IsInfiniteEnergyActive);
            mockMemory.Verify(m => m.WriteEnergy(500), Times.Once);
        }

        [Fact]
        public void SetMaxResources_ShouldCallWriteMaxResources()
        {
            // Arrange
            var mockMemory = new Mock<MemoryManager>(It.IsAny<System.Diagnostics.Process>());
            mockMemory.Setup(m => m.WriteMaxResources());
            var controller = new TrainerController(mockMemory.Object);

            // Act
            controller.SetMaxResources();

            // Assert
            mockMemory.Verify(m => m.WriteMaxResources(), Times.Once);
        }

        [Fact]
        public void UnlockAllTechs_ShouldCallWriteUnlockAllTechs()
        {
            // Arrange
            var mockMemory = new Mock<MemoryManager>(It.IsAny<System.Diagnostics.Process>());
            mockMemory.Setup(m => m.WriteUnlockAllTechs());
            var controller = new TrainerController(mockMemory.Object);

            // Act
            controller.UnlockAllTechs();

            // Assert
            mockMemory.Verify(m => m.WriteUnlockAllTechs(), Times.Once);
        }
    }
}
