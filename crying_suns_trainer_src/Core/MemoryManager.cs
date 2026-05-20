using System;
using System.Diagnostics;
using Memory;

namespace CryingSunsTrainer.Core
{
    /// <summary>
    /// Manlow-level memory operations for the Crying Suns game process.
    /// Uses the Memory.dll library for safe read/write operations.
    /// </summary>
    public class MemoryManager : IDisposable
    {
        private readonly Mem _memoryInstance;
        private readonly Process _gameProcess;
        private bool _disposed;

        // Base addresses for common game values (example offsets, would be tuned per game version)
        private const int HullBaseOffset = 0x00A3F7C0;
        private const int EnergyBaseOffset = 0x00A3F8D0;
        private const int ScrapOffset = 0x00A3F9E0;
        private const int FuelOffset = 0x00A3FAF0;
        private const int FoodOffset = 0x00A3FC00;
        private const int TechUnlockBase = 0x00B2A100;

        public MemoryManager(Process process)
        {
            _gameProcess = process ?? throw new ArgumentNullException(nameof(process));
            _memoryInstance = new Mem();
            _memoryInstance.OpenProcess(process.Id);
        }

        /// <summary>
        /// Reads the current hull value from game memory.
        /// </summary>
        public int ReadHull()
        {
            return _memoryInstance.ReadInt($"0x{HullBaseOffset:X}");
        }

        /// <summary>
        /// Writes a new hull value to game memory.
        /// </summary>
        public void WriteHull(int value)
        {
            _memoryInstance.WriteMemory($"0x{HullBaseOffset:X}", "int", value.ToString());
        }

        /// <summary>
        /// Reads the current energy value.
        /// </summary>
        public int ReadEnergy()
        {
            return _memoryInstance.ReadInt($"0x{EnergyBaseOffset:X}");
        }

        /// <summary>
        /// Writes a new energy value.
        /// </summary>
        public void WriteEnergy(int value)
        {
            _memoryInstance.WriteMemory($"0x{EnergyBaseOffset:X}", "int", value.ToString());
        }

        /// <summary>
        /// Sets scrap, fuel, and food to maximum values.
        /// </summary>
        public void WriteMaxResources()
        {
            const int maxScrap = 9999;
            const int maxFuel = 999;
            const int maxFood = 999;

            _memoryInstance.WriteMemory($"0x{ScrapOffset:X}", "int", maxScrap.ToString());
            _memoryInstance.WriteMemory($"0x{FuelOffset:X}", "int", maxFuel.ToString());
            _memoryInstance.WriteMemory($"0x{FoodOffset:X}", "int", maxFood.ToString());
        }

        /// <summary>
        /// Unlocks all technology nodes by writing a 1 to each unlock flag.
        /// Assumes 100 tech slots starting at base offset.
        /// </summary>
        public void WriteUnlockAllTechs()
        {
            for (int i = 0; i < 100; i++)
            {
                int offset = TechUnlockBase + (i * 4); // each flag is 4 bytes (int)
                _memoryInstance.WriteMemory($"0x{offset:X}", "int", "1");
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _memoryInstance.CloseProcess();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
